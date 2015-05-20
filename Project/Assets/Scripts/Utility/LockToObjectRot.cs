using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Transform))]

public class LockToObjectRot : MonoBehaviour
{
	Transform transform;
	
	public GameObject Target;
	public bool IgnoreXAxis = false;
	public bool IgnoreYAxis = false;
	public bool IgnoreZAxis = false;
	
	public Vector3 MinRotation = new Vector3(0f, 0f, 0f);
	public Vector3 MaxRotation = new Vector3(360f, 360f, 360f);
	
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
			Quaternion targetRotation = Target.GetComponent<Transform>().rotation;

			Vector3 newRotation = targetRotation.eulerAngles;

			if(IgnoreXAxis)
			{
				newRotation.x = transform.rotation.eulerAngles.x;
			}
			if(IgnoreYAxis)
			{
				newRotation.y = transform.rotation.eulerAngles.y;
			}
			if(IgnoreZAxis)
			{
				newRotation.z = transform.rotation.eulerAngles.z;
			}
			
			newRotation.x = Mathf.Clamp(newRotation.x, MinRotation.x, MaxRotation.x);
			newRotation.y = Mathf.Clamp(newRotation.y, MinRotation.y, MaxRotation.y);
			newRotation.z = Mathf.Clamp(newRotation.z, MinRotation.z, MaxRotation.z);

			transform.rotation = Quaternion.Euler(newRotation);
		}
	}
}
