using UnityEngine;
using System.Collections;
[RequireComponent (typeof (ObjectEvents))]

public class OnDeathKnockback : MonoBehaviour
{
	ObjectEvents objectEvents;

	public float Knockback;

	// Use this for initialization
	void Start ()
	{
		objectEvents = GetComponent<ObjectEvents>();

		if(GetComponent<ObjectEvents>() != null)
		{
			GetComponent<ObjectEvents>().OnDeathListeners += OnDeath;
		}
	}
	
	// Update is called once per frame
	void OnDeath (ObjectEvents.DeathType deathType, GameObject attacker)
	{
		if(objectEvents.Owner == null)
		{
			return;
		}
		else if((GetComponent<Rigidbody>() == null)
		   || (objectEvents.Owner.GetComponent<Rigidbody>() == null))
		{
			return;
		}

		Vector3 newVelocity = GetComponent<Rigidbody>().velocity;
		newVelocity.Normalize();
		newVelocity *= Knockback;

		if(objectEvents.Owner.GetComponent<SpaceShipController>() != null)
		{
			objectEvents.Owner.GetComponent<SpaceShipController>().ApplyVelocity(newVelocity);
		}
		else
		{
			objectEvents.Owner.GetComponent<Rigidbody>().velocity += newVelocity;
		}
	}
}
