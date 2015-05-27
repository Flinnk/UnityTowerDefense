using UnityEngine;
using System.Collections;

public class Cannon : Proyectile 
{
    private GameController gameController;

    void Start()
    {
        gameController = GameController.Instance;

    }

	void Update () 
	{
        if(gameController.getState() == GameController.STATES.PLAY)
        {
		    if (target != null ) 
		    {
			    this.transform.position += (target.position - this.transform.position) * Time.deltaTime * velocity;
		    } 
		    else 
		    {
			    Destroy(gameObject);
		    }
        }
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals ("Enemy")) 
        {
			other.GetComponent<Enemy>().ReceiveDamage(damage);
            Destroy(gameObject);
		}
	}
}
