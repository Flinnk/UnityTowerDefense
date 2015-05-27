using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour {

	public int rows = 8;
	public int columns = 8;

	public Position[] crystalsPositions;
	public Position[] spawnPositions;
	
	public GameObject wall;
	public GameObject tile;
	public GameObject crystal;
	public GameObject spawner;

	private Transform gridHolder;
    private Transform crystalHolder;
    private Transform spawnHolder;

	void GridSetup()
	{
		gridHolder = new GameObject ("Grid").transform;

		for (int x =0; x<columns+1; x++) 
		{
			for (int y =0 ;y<rows+1;y++)
			{
				GameObject instance;
				if(x == 0|| x == columns || y == 0 || y == rows)
				{
					instance=Instantiate (wall, new Vector3 (x, 0f, y), Quaternion.identity) as GameObject;
				}
				else
				{
					instance=Instantiate (tile, new Vector3 (x, 0f, y), Quaternion.identity) as GameObject;

				}
				instance.transform.localRotation = Quaternion.Euler(90f,0f,0f);//Because the initial rotate state of a plane
				instance.transform.SetParent (gridHolder); 
			}
		}
	}

	public void InstantiateCrystals()
	{
        crystalHolder = new GameObject("Crystals").transform;
        GameController gameController = GameController.Instance;
		for (int i =0; i<crystalsPositions.Length; i++) 
		{
			Position position = crystalsPositions[i];
            GameObject crystalObject = Instantiate(crystal, new Vector3(position.column, 0, position.row), Quaternion.identity) as GameObject;
            gameController.crystals.Add(crystalObject);
            //Put GameObject above the floor acording to its collider size
            Collider collider = crystalObject.GetComponent<Collider>();
            if (collider != null)
            {
                crystalObject.transform.position = crystalObject.transform.position + crystalObject.transform.up * collider.bounds.size.y / 2;
            }
            crystalObject.transform.SetParent(crystalHolder); 
		}
	}

	public void InstantiateSpawners()
	{
        spawnHolder = new GameObject("Spawners").transform;
        GameController gameController = GameController.Instance;
		for (int i =0; i<spawnPositions.Length; i++) 
		{
			Position position = spawnPositions[i];
			GameObject spawnObject = Instantiate (spawner, new Vector3(position.column,0,position.row), Quaternion.identity) as GameObject;
            gameController.enemySpawners.Add(spawnObject);
            spawnObject.transform.SetParent(spawnHolder); 
		}
	}

	public void Setup()
	{
		GridSetup ();
		InstantiateCrystals ();
		InstantiateSpawners ();
	}

}
