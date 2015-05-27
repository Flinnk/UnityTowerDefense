using UnityEngine;
using System.Collections;

public class LaserTurret : Turret 
{
	private Laser laser;

    private GameController gameController;


	void Start()
	{
        base.Initialize();
        gameController = GameController.Instance;
		GameObject laserObject= Instantiate(proyectile,this.transform.position,Quaternion.identity) as GameObject;
		laser = laserObject.GetComponent<Laser> ();
	}

	private void Shoot()
	{
        if (Time.time > nextShot && gameController.getState() == GameController.STATES.PLAY) 
        {
			if(enemies[0]!=null)
			{
				laser.setTarget(enemies[0].gameObject.transform);
				nextShot=Time.time+fireRate;
			}
			else
			{
				laser.Return();
				enemies.Remove(enemies[0]);
			}
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag.Equals("Enemy")) 
        {
			enemies.Add(collider);
		}
	}

	void OnTriggerStay(Collider collider)
	{
		if (collider.tag.Equals("Enemy")) 
        {
			if(enemies.Count>0)
			{
				Shoot ();
			}
		}
	}
	
	void OnTriggerExit(Collider collider)
	{
		if (collider.tag.Equals("Enemy")) 
        {
			enemies.Remove(collider);
		}
	}
}
