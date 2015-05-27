﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathdindingGrid : MonoBehaviour 
{
	//Note: commented part is only in case the object is in the center of the map
	public LayerMask nonWalkableMask;
	public float nodeRadius;
    private Vector2 gridWorldSize;
	private Node[,] grid;
	private float nodeDiameter;
	private int gridSizeX,gridSizeY;

    public void Setup(int x ,int y)
    {
        Initialize(x, y);
        CreateGrid();
    }

	private void Initialize(int x,int y)
	{
		nodeDiameter = nodeRadius * 2;
		gridWorldSize = new Vector2 (x, y);
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
	}

	private void CreateGrid()
	{
		grid = new Node[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = transform.position;
       
        for (int x =0; x<gridSizeX; x++) 
		{
			for (int y =0; y<gridSizeY; y++) 
			{
                if(grid[x,y]==null)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter) + Vector3.forward * (y * nodeDiameter);
                    bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, nonWalkableMask));
                    grid[x, y] = new Node(walkable, worldPoint, x, y);
                }

                List<Node> neighbours = new List<Node>();

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0)
                        {
                            continue;
                        }

                        int checkX = x + i;
                        int checkY = y + j;

                        if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                        {

                            if (grid[checkX, checkY] == null)
                            {
                                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + i) + Vector3.forward * (y * nodeDiameter + j);
                                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, nonWalkableMask));
                                Node n = new Node(walkable, worldPoint, x+i, y+j);
                                grid[checkX, checkY] = n;
                            }
                            else
                            {
                                neighbours.Add(grid[checkX, checkY]);
                            }

                        }
                    }
                }
                grid[x, y].setNeighbours(neighbours);
			}
		}
	}

	public Node NodeFromWorldPoint(Vector3 worldPosition)
	{
		float percentX = (worldPosition.x /*+ gridWorldSize.x / 2*/) / gridWorldSize.x;
		float percentY = (worldPosition.z /*+ gridWorldSize.y / 2*/) / gridWorldSize.y;
		percentX=Mathf.Clamp01(percentX);
		percentY=Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX) * percentX);
		int y = Mathf.RoundToInt((gridSizeY) * percentY);

		return grid [x, y];
	}

	public List<Node>GetNeighbours(Node node)
	{
		List<Node> neighbours = new List<Node> ();

		for (int x =-1; x<= 1; x++) {
			for (int y =-1; y<= 1; y++) 
			{
				if(x==0 && y ==0)
				{
					continue;
				}

				int checkX = node.getGridX()+x;
				int checkY = node.getGridY()+y;

				if (checkX>=0 && checkX < gridSizeX && checkY>=0 && checkY < gridSizeY)
				{
					neighbours.Add(grid[checkX,checkY]);
				}
			}
		}

		return neighbours;
	}

	public void UpdateGrid(Vector3 position)
	{
		Node node = NodeFromWorldPoint (position);
        
		bool walkable = !(Physics.CheckSphere(node.getPosition(),nodeRadius,nonWalkableMask));
        node.setWalkable(walkable);

		List<Node> neighbours = node.getNeighbours();
		foreach (Node n in neighbours) {
			walkable = !(Physics.CheckSphere(n.getPosition(),nodeRadius,nonWalkableMask));
            n.setWalkable(walkable);
		}
	}

    //Decomment to see the grid on the editor
	/*void OnDrawGizmos() 
	{

		if (grid != null) {
			foreach (Node n in grid) {
				Gizmos.color = (n.isWalkable())?Color.white:Color.red;

				Gizmos.DrawCube(n.getPosition(), Vector3.one * (nodeDiameter-.1f));
			}
		}
	}*/

}
