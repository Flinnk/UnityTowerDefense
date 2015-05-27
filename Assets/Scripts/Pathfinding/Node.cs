using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node 
{
	private bool walkable;
    private Vector3 position;
    private int gridX;
    private int gridY;
    private Node parent;
    private List<Node> neighbours;

	public Node(bool walkable, Vector3 position,int gridX,int gridY)
	{
        this.walkable = walkable;
		this.position = position;
		this.gridX = gridX;
		this.gridY = gridY;
        neighbours= new List<Node>();
	}

    public bool isWalkable()
    {
        return this.walkable;
    }

    public void setWalkable(bool walkable)
    {
        this.walkable = walkable;
    }

    public Vector3 getPosition()
    {
        return this.position;
    }

    public void setPosition(Vector3 position)
    {
        this.position = position;
    }

    public int getGridX()
    {
        return this.gridX;
    }

    public void setGridX(int gridX)
    {
        this.gridX = gridX;
    }

    public int getGridY()
    {
        return this.gridY;
    }

    public void setGridY(int gridY)
    {
        this.gridY = gridY;
    }

    public Node getParent()
    {
        return this.parent;
    }

    public void setParent(Node parent)
    {
        this.parent = parent;
    }

    public List<Node> getNeighbours()
    {
        return this.neighbours;
    }

    public void setNeighbours(List<Node> neighbours)
    {
        this.neighbours = neighbours;
    }
}
