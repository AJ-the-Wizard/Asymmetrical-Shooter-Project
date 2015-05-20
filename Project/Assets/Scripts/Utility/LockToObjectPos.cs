using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Transform))]

public class LockToObjectPos : MonoBehaviour
{
	Transform transform;

	public GameObject Target;

	// Use this for initialization
	void Start ()
	{
		transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(Target != null)
		{
			transform.position = Target.GetComponent<Transform>().position;
		}
	}
}
