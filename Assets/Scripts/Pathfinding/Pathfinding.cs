using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour {

	private PathdindingGrid grid;

	void Awake()
	{
		grid = GameController.Instance.GetComponent<PathdindingGrid> ();

	}

	public void FindPath(Vector3 startPos, Vector3 targetPos) {
		Node startNode = grid.NodeFromWorldPoint(startPos);
		Node targetNode = grid.NodeFromWorldPoint(targetPos);

		List<Node> openSet = new List<Node>();
		HashSet<Node> closedSet= new HashSet<Node>();
		openSet.Add(startNode);
        closedSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            openSet.Remove(currentNode);

             if (currentNode == targetNode)//Early exit
             {
                 return;
             }

            foreach (Node neighbour in currentNode.getNeighbours())
            {
                if (neighbour.isWalkable() && !closedSet.Contains(neighbour))
                {
                    neighbour.setParent(currentNode);
                    closedSet.Add(neighbour);
                    if(!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
	}

	public List<Node> RetracePath(Vector3 start, Vector3 end) {
		Node startNode = grid.NodeFromWorldPoint (start);
		Node endNode = grid.NodeFromWorldPoint (end);

		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			path.Add(currentNode);
            currentNode = currentNode.getParent();
            if (!currentNode.isWalkable()||currentNode==null)//Control cases of way completely blocked
            {
                return null;
            }
		}
		path.Reverse();
		
		return path;
		
	}
}