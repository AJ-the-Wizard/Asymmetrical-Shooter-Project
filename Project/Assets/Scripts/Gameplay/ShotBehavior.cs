using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Transform))]
[RequireComponent (typeof (Rigidbody))]

public class ShotBehavior : MonoBehaviour
{
	Transform transform;
	Rigidbody rigidBody;

	public float Speed = 25f;

	// Use this for initialization
	void Awake ()
	{
		transform = GetComponent<Transform>();
		rigidBody = GetComponent<Rigidbody>();

		rigidBody.velocity += transform.forward * Speed;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	}
}
