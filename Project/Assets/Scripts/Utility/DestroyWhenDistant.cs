using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Transform))]
[RequireComponent (typeof (ObjectEvents))]

public class DestroyWhenDistant : MonoBehaviour
{
	Transform transform;

	public GameObject Target;
	Transform TargetTransform;
	GameObject OldTarget;
	public float MaxDistance = 10f;

	// Use this for initialization
	void Start ()
	{
		transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Target != OldTarget)
		{
			TargetTransform = Target.GetComponent<Transform>();
		}
		OldTarget = Target;

		if(Target != null)
		{
			float distance = Vector3.Distance(transform.position, TargetTransform.position);

			if(distance > MaxDistance)
			{
				if(GetComponent<ObjectEvents>() != null)
				{
					GetComponent<ObjectEvents>().Death(ObjectEvents.DeathType.Script, null);
				}

				DestroyObject(this.gameObject);
			}
		}
	}
}
