using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Rigidbody))]

public class RandomSpin : MonoBehaviour
{
	public Vector3 MinAngularVelocity;
	public Vector3 MaxAngularVelocity;

	// Use this for initialization
	void Start ()
	{
		Vector3 rotation = new Vector3(Random.Range(MinAngularVelocity.x, MaxAngularVelocity.x),
		                               Random.Range(MinAngularVelocity.y, MaxAngularVelocity.y),
		                               Random.Range(MinAngularVelocity.z, MaxAngularVelocity.z));

		GetComponent<Rigidbody>().angularVelocity = rotation;
	}
}
