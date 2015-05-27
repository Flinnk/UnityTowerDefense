using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public float movementSpeed = 8f;
	[Header ("Zoom Settings")]
	public float zoomSpeed= 4f;
    public float zoomScale = 100f;
	public float maximumZoom = 60f;
	public float minimumZoom = 15f;
	[Header ("Rotating Settings")]
	public float rotationSpeed= 40f;

	private enum RotationDirection {NONE,RIGHT,LEFT};
	private RotationDirection rotationDirection;
	private Camera camera;

	void Start () 
    {
		camera = Camera.main;
        rotationDirection = RotationDirection.NONE;
	}
	
	void Update () 
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		float zoom = Input.GetAxis ("Mouse ScrollWheel");

		if (Input.GetKey (KeyCode.Q)) 
        {
			rotationDirection=RotationDirection.LEFT;
		} 
        else if (Input.GetKey (KeyCode.E)) 
        {
			rotationDirection=RotationDirection.RIGHT;
		} 
        else 
        {
			rotationDirection=RotationDirection.NONE;
		}

		Rotate (rotationDirection);
		Zoom (zoom);
		Move (horizontal,vertical);
	}

    void Move(float horizontal, float vertical)
	{
        Vector3 movementVector = (this.transform.right * horizontal + this.transform.up * vertical) * Time.deltaTime * movementSpeed;
		movementVector.y = 0;
		this.transform.position += movementVector;
	}

	void Zoom(float zoom)
	{
		camera.fieldOfView = Mathf.Clamp (camera.fieldOfView + -zoom*Time.deltaTime*zoomSpeed*zoomScale, minimumZoom, maximumZoom);
	}

	void Rotate(RotationDirection direction)
	{
		Vector3 localRotation = this.transform.localRotation.eulerAngles;

		switch (direction) 
        {
			case RotationDirection.LEFT:
			{
			    this.transform.localRotation = Quaternion.Euler (localRotation.x,localRotation.y+-1*rotationSpeed*Time.deltaTime,localRotation.z);
				break;
			}
			case RotationDirection.RIGHT:
			{
			    this.transform.localRotation = Quaternion.Euler (localRotation.x,localRotation.y+1*rotationSpeed*Time.deltaTime,localRotation.z);
				break;
			}
			case RotationDirection.NONE:
			{
				break;
			}
		}
	}
}
