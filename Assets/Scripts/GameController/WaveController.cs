using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveController : MonoBehaviour 
{
    public GameObject normalEnemy;
    public GameObject swiftEnemy; 
    public GameObject armouredEnemy;
    public enum TYPE_ENEMY {NORMAL, SWIFT, ARMOURED};

    private GameController gameController;
    private List<EnemySpawner> enemySpawners;

	void Start () 
    {
        gameController = GameController.Instance;
        enemySpawners = new List<EnemySpawner>();
        for (int i = 0; i < gameController.enemySpawners.Count;i++ )
        {
            enemySpawners.Add(gameController.enemySpawners[i].GetComponent<EnemySpawner>());
            enemySpawners[i].ChangeSpawnParameters(normalEnemy);
        }
	}

    public void ChangeSpawner(int index, TYPE_ENEMY enemyType, float spawnRate)
    {
        if (enemySpawners[index] != null)
        {
            GameObject enemy;
            switch(enemyType)
            {
                case TYPE_ENEMY.NORMAL:
                {
                    enemy = normalEnemy;
                    break;
                }
                case TYPE_ENEMY.SWIFT:
                {
                    enemy = swiftEnemy;
                    break;
                }
                case TYPE_ENEMY.ARMOURED:
                {
                    enemy = armouredEnemy;
                    break;
                }
                default:
                {
                    enemy = null;
                    break;
                }
            }
            
            if(enemy!=null)
            {
                enemySpawners[index].ChangeSpawnParameters(enemy,spawnRate);
            }
            
        }
    }
}
