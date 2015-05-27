using UnityEngine;
using System.Collections;

public class Proyectile : MonoBehaviour 
{
	public float velocity = 20f;
	public int damage = 15;

    protected Transform target;

    public void setTarget(Transform target)
    {
        this.target = target;
    }

    public Transform getTarget()
    {
        return this.target;
    }
}
