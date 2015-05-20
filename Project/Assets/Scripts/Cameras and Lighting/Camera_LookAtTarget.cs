using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Transform))]

public class Camera_LookAtTarget : MonoBehaviour
{
	Transform transform;

	//Quaternion DefaultAngle;

	public GameObject Owner;
	public GameObject Target;

	public float FollowTrackSpeed = 100f;
	public float FollowTrackDistance = 2f;
	public Vector3 FollowTrackOffset = new Vector3(0f, 1f, 0f);

	public float EnemyTrackSpeed = 100f;
	public float EnemyTrackDistance = 2f;
	public Vector3 EnemyTrackOffset = new Vector3(0f, 1f, 0f);

	// Use this for initialization
	void Start ()
	{
		transform = GetComponent<Transform>();

		//DefaultAngle = transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if((Owner) == false)
		{
			return;
		}

		Transform ownerTransform = Owner.GetComponent<Transform>();
       
		if(Target == null)
		{
			transform.rotation = Quaternion.RotateTowards(transform.rotation,
			                                              ownerTransform.rotation,
			                                              Time.fixedDeltaTime * FollowTrackSpeed);

			Vector3 newPosition = ownerTransform.position;
			newPosition += ownerTransform.right * FollowTrackOffset.x;
			newPosition += ownerTransform.up * FollowTrackOffset.y;
			newPosition += ownerTransform.forward * FollowTrackOffset.z;
			transform.position = newPosition;
		}
		else
		{
			Transform targetTransform = Target.GetComponent<Transform>();
			
			Vector3 relativePosition = new Vector3();
			relativePosition.x = targetTransform.position.x - transform.position.x;
			relativePosition.y = targetTransform.position.y - transform.position.y;
			relativePosition.z = targetTransform.position.z - transform.position.z;
			
			Quaternion targetAngle = Quaternion.LookRotation(relativePosition);
			
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetAngle, Time.fixedDeltaTime * EnemyTrackSpeed);

			Vector3 newPosition = Owner.GetComponent<Transform>().position;
			relativePosition.Normalize();
			newPosition.x -= relativePosition.x * EnemyTrackDistance;
			newPosition.y -= relativePosition.y * EnemyTrackDistance;
			newPosition.z -= relativePosition.z * EnemyTrackDistance;
			newPosition += EnemyTrackOffset;
			
			transform.position = newPosition;
			
			//transform.position = GetComponentInParent<Transform>().position + (-relativePosition);
		}	
	}
}