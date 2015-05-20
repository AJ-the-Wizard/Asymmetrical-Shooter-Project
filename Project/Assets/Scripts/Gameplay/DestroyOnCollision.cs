using UnityEngine;
using System.Collections;
[RequireComponent (typeof(ObjectEvents))]

public class DestroyOnCollision : MonoBehaviour
{
	private ObjectEvents objectEvents;

	void Awake()
	{
		objectEvents = GetComponent<ObjectEvents>();
	}

	void OnTriggerEnter(Collider other)
	{
		CheckForOwner(other.gameObject);
	}
	
	void OnCollision(Collision collision)
	{
		CheckForOwner(collision.gameObject);
	}

	void CheckForOwner(GameObject other)
	{
		Transform[] transforms = other.GetComponentsInParent<Transform>();
		
		foreach(Transform transform in transforms)
		{
			if(transform.gameObject == objectEvents.Owner)
			{
				return;
			}
		}
		
		objectEvents.Death(ObjectEvents.DeathType.Script, other.gameObject);
		DestroyObject(this.gameObject);
	}
}
