using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Rigidbody))]

public class ConstantSpin : MonoBehaviour
{
	Rigidbody rigidBody;

	public Vector3 AngularVelocity;

	// Use this for initialization
	void Start ()
	{
		rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		rigidBody.angularVelocity = AngularVelocity;
	}
}
