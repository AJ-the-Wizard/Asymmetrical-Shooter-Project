using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Rigidbody))]

public class ConstantVelocity : MonoBehaviour
{
	Rigidbody rigidBody;

	public Vector3 Velocity;

	// Use this for initialization
	void Start ()
	{
		rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		rigidBody.velocity = Velocity;
	}
}
