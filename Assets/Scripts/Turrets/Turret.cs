using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Turret : MonoBehaviour 
{
	public float fireRate=0.5f;
	public float fireRadius=5f;
	public GameObject proyectile;

    protected float nextShot;
    protected List<Collider> enemies = new List<Collider>();

	private BoxCollider boxCollider;

	protected void Initialize () 
	{
		boxCollider = GetComponentInChildren<BoxCollider> ();
		boxCollider.size = new Vector3 (fireRadius, 0, fireRadius);
		nextShot=0f;
	}
	
}
