using UnityEngine;
using System.Collections;

public class PlayerCamMovement : MonoBehaviour {

	Rigidbody rb;
	Vector3 movement;

	public float speed;
	public float turnment;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
	}


	void FixedUpdate()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		Turning (h);
	}


	void Turning(float h)
	{
		// Rotate player about y axis according to keyboard input (for testing)
		transform.Rotate (0f, h * turnment, 0f);
	}


}
