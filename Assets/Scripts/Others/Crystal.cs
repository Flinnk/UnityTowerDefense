using UnityEngine;
using System.Collections;

public class Crystal : MonoBehaviour 
{
	public int health=1;
    private int currentHealth;
    private GameController gameController;

    void Start()
    {
        currentHealth = health;
        gameController = GameController.Instance;
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals ("Enemy")) 
        {
			Destroy(other.gameObject);
            currentHealth--;
            if (currentHealth <= 0)
			{
                gameController.onGameOver();
				Debug.Log("Gameover");
			}
		}
	}
}
