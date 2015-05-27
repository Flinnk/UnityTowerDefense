using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : Singleton<GameController> 
{
    [Header("UI")]
    public Button playPauseButton;
    public Text controls;
    public Text gameOver;
    [HideInInspector]public enum STATES { PLAY, STOP, GAME_OVER, RECREATING_GRID }
    [Header("GameControlling")]
	public GameObject cannonTurret;
	public GameObject laserTurret;
	public LayerMask walkable;
	public LayerMask nonConstructable;
    public int nonWalkableLayerIndex;

    [HideInInspector]
    public List<GameObject> crystals;
    //[HideInInspector]
    public List<GameObject> enemySpawners;
	
	private GridManager gridManager;
	private PathdindingGrid pathfindingGrid;

    private STATES state;

	void Awake () 
	{
        crystals = new List<GameObject>();
        enemySpawners = new List<GameObject>();
		state = STATES.RECREATING_GRID;
		gridManager = GetComponent<GridManager> ();
		pathfindingGrid = GetComponent<PathdindingGrid> ();
		gridManager.Setup ();
		pathfindingGrid.Setup (gridManager.rows+1,gridManager.columns+1);//+1 Because i generate a external wall
        state = STATES.STOP;
    }
	
	void Update () 
	{
        if (this.state == STATES.PLAY)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InstantiateTurret(cannonTurret);
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                InstantiateTurret(laserTurret);
            }
        }
	}

    void InstantiateTurret(GameObject turret)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, walkable))
        {
            if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, nonConstructable))
            {
                Vector3 position = hit.collider.gameObject.transform.position;
                position.y = 0;
                hit.collider.gameObject.layer = nonWalkableLayerIndex;
                GameObject instance = Instantiate(turret, position, Quaternion.identity)as GameObject;
                Collider collider = instance.GetComponent<Collider>();
                if (collider != null)
                {
                    instance.transform.position = instance.transform.position + instance.transform.up * collider.bounds.size.y / 2;
                }
                state = STATES.RECREATING_GRID;
                pathfindingGrid.UpdateGrid(position);
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<Enemy>().ReEvaluatePath();
                }
                state = STATES.PLAY;
            }
        }
    }

    public void PauseOrPlay()
    {
        if (state == STATES.STOP)
        {
            if(playPauseButton!=null)
                playPauseButton.GetComponentInChildren<Text>().text="Pause";
            if (controls != null)
                controls.enabled = false;
            state = STATES.PLAY;
        }
        else if (state == STATES.PLAY)
        {
            if (playPauseButton != null)
                playPauseButton.GetComponentInChildren<Text>().text = "Play";
            if (controls != null)
                controls.enabled = true;
            state = STATES.STOP;
        }
        else if (state == STATES.GAME_OVER)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    public void onGameOver()
    {
       state=GameController.STATES.GAME_OVER;
       if (gameOver != null)
           gameOver.enabled = true;
       if (playPauseButton != null)
           playPauseButton.GetComponentInChildren<Text>().text = "Retry";
    }

    public STATES getState()
    {
        return this.state;
    }

    public void setState(STATES state)
    {
        this.state = state;
    }
}
