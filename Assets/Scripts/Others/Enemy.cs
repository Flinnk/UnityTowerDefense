using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	public float movementSpeed = 5f;
	public float health = 100f;
    [Header("HealthBar")]
    public Texture2D healthBar;
    public float healthBarOffsetX;
    public float healthBarOffsetY;

    private float currentHealth;
	private Pathfinding pathfinding;
	private Transform target;
	private List<Node> path;
	private int pathIndex;
	private GameController gameController;
    private Rect healthBarPosition;

	void Start () 
	{
        currentHealth = health;
		gameController = GameController.Instance;
		pathfinding = GetComponent<Pathfinding> ();
		FindNearestCrystal ();
        ReEvaluatePath();
	}
	
	void Update () 
	{
        if (path != null)//Path not blocked
        {
            if (pathIndex < path.Count && gameController.getState() == GameController.STATES.PLAY)
            {
                Vector3 targetDirection = (path[pathIndex].getPosition() - transform.position);
                targetDirection.y = 0;
                if (targetDirection.magnitude > 0.05f)
                {
                    Move(targetDirection.normalized);
                }
                else
                {
                    pathIndex++;
                }
            }
        }
	}

	void Move(Vector3 targetDirection)
	{
		this.transform.position += targetDirection* Time.deltaTime * movementSpeed;
	}

	void FindNearestCrystal()
	{
        List<GameObject> crystals = gameController.crystals;
		for (int i =0; i<crystals.Count; i++) 
		{
			if (i==0)
			{
				target=crystals[0].transform;
			}
			else if((crystals[i].transform.position-transform.position).magnitude<(target.position-transform.position).magnitude)
			{
				target=crystals[i].transform;
			}
		}
	}

	public void ReEvaluatePath()
	{
		pathfinding.FindPath (transform.position,target.position);
		path=pathfinding.RetracePath (transform.position,target.position);
		pathIndex = 0;
	}

	public void ReceiveDamage(float damage)
	{
		currentHealth -= damage;
		if(currentHealth<=0)
		{
			Destroy(gameObject);
		}
	}

    public void OnGUI()
    {
        Vector3 position = Camera.main.WorldToScreenPoint(transform.position);
        healthBarPosition.x = position.x - healthBarOffsetX;
        healthBarPosition.y = Screen.height - position.y - healthBarOffsetY;
        healthBarPosition.width = (currentHealth / health)*25;
        healthBarPosition.height = 10;
        GUI.DrawTexture(healthBarPosition,healthBar);
    }

     //Decomment to see in the editor the path trazed
     /* void OnDrawGizmos()
    {
        if (path != null)
        {
            foreach (Node n in path)
            {
                Gizmos.color = (n.isWalkable) ? Color.black : Color.red;

                Gizmos.DrawCube(n.position, Vector3.one * (1 - .1f));
            }
        }
    }*/
	
}
