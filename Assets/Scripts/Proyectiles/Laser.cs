using UnityEngine;
using System.Collections;

public class Laser : Proyectile 
{
	private LineRenderer laserRenderer;
	private BoxCollider beamCollider;
    private GameController gameController;
	
	void Start()
	{
        gameController = GameController.Instance;
		beamCollider = GetComponent<BoxCollider> ();
		laserRenderer = GetComponent<LineRenderer>();
		laserRenderer.SetPosition(0,transform.position);
		Return ();
	}
	
	void Update () 
	{
        if (gameController.getState() == GameController.STATES.PLAY)
        {
            if (target != null)
            {
                beamCollider.enabled = true;
                transform.LookAt(target);
                beamCollider.center = new Vector3(0, 1, (target.position - transform.position).magnitude / 2);
                beamCollider.size = new Vector3(1, 1, (target.position - transform.position).magnitude);
                laserRenderer.SetPosition(1, target.position);
            }
            else
            {
                Return();
            }
        }
        else if (gameController.getState() == GameController.STATES.GAME_OVER)
        {
            Return();
        }
	}

	public void Return()
	{
		laserRenderer.SetPosition(1,transform.position);
		beamCollider.enabled = false;
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals ("Enemy"))
        {
			Enemy enemy = other.gameObject.GetComponent<Enemy>();
			enemy.ReceiveDamage(damage*Time.deltaTime);

		}
	}
}
