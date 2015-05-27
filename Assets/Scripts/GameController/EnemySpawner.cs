using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour 
{
	public float spawnRate =2f;
	
    private GameObject enemy;
	private float nextSpawn;
    private GameController gameController;

    void Start()
    {
        nextSpawn = spawnRate;
        gameController = GameController.Instance;
    }

	void Update () 
	{
        if (gameController.getState()==GameController.STATES.PLAY)
        {
            Spawn();
        }
	}

	void Spawn()
	{
		if (Time.time > nextSpawn&&enemy!=null) 
		{
			GameObject instance = Instantiate(enemy,transform.position,Quaternion.identity) as GameObject;
            Collider collider = instance.GetComponent<Collider>();
            if (collider != null)
            {
                instance.transform.position = instance.transform.position + instance.transform.up * collider.bounds.size.y / 2;
            }
            instance.transform.SetParent(transform);
            nextSpawn=Time.time+spawnRate;
		}
	}

    public void ChangeSpawnParameters(GameObject enemy,float spawnRate)
    {
        this.enemy = enemy;
        this.spawnRate = spawnRate;
        nextSpawn = Time.time + spawnRate;
    }

    public void ChangeSpawnParameters(GameObject enemy)
    {
        ChangeSpawnParameters(enemy, this.spawnRate);
    }
}
