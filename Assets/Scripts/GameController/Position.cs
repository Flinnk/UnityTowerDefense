using UnityEngine;
using System.Collections;

[System.Serializable]
public class Position 
{
	[SerializeField]public int row;
	[SerializeField]public int column;

	public Position(int row, int column)
	{
		this.row = row;
		this.column = column;
	}
}
