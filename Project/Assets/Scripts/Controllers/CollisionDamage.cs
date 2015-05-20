using UnityEngine;
using System.Collections;
[RequireComponent (typeof (ObjectEvents))]

public class CollisionDamage : MonoBehaviour
{
	//Rigidbody rigidBody;
	ObjectEvents objectEvents;

	public int Damage = 1;
	public bool DestroyOnHit = true;
	public bool DestroyOnTriggers = false;
	private Vector3 LastVelocity;

	public GameObject OnHitFX;

	// Use this for initialization
	void Awake ()
	{
		objectEvents = GetComponent<ObjectEvents>();
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject != null)
		{
			DealDamage(other.gameObject);
			Collided(other.gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject != null)
		{
			DealDamage(other.gameObject);
			Collided(other.gameObject);
		}
	}

	void DealDamage(GameObject otherObject)
	{
		if(otherObject != null)
		{
			if(otherObject.GetComponent<ProxyHitbox>())
			{
				otherObject = otherObject.GetComponent<ObjectEvents>().Owner;
			}

			if(otherObject.GetComponent<Health>() != null)
			{
				otherObject.GetComponent<Health>().TakeDamage(Damage, this.gameObject);
			}
		}
	}

	void Collided(GameObject otherObject)
	{
		if(otherObject.GetComponent<Collider>().isTrigger
		    && (DestroyOnTriggers == false))
		{
			return;
		}

		if(OnHitFX != null)
		{
			GameObject fx = Instantiate(OnHitFX,
			                            GetComponent<Transform>().position,
			                            GetComponent<Transform>().rotation) as GameObject;
			
			if(fx.GetComponent<ObjectEvents>() != null)
			{
				fx.GetComponent<ObjectEvents>().Owner = objectEvents.Owner;
				fx.GetComponent<ObjectEvents>().Team = objectEvents.Team;
				fx.GetComponent<ObjectEvents>().PreventParentCollision();
			}
		}

		if(DestroyOnHit)
		{
			GetComponent<Collider>().enabled = false;
			objectEvents.Death(ObjectEvents.DeathType.Script, otherObject);
			DestroyObject(this.gameObject);
			this.enabled = false;
		}
	}
}
