using UnityEngine;
using System.Collections;

public class ObjectEvents : MonoBehaviour
{
	public GameObject Owner;
	public string Team;
	
	public delegate void OnDamaged(int damageTaken, GameObject attacker);
	public event OnDamaged OnDamagedListeners;

	public enum DeathType {Script, Damage};
	public delegate void OnDeath(DeathType deathType, GameObject attacker);
	public event OnDeath OnDeathListeners;

	// Use this for initialization
	void Start ()
	{
		if(Owner == null)
		{
			Owner = this.gameObject;
		}

		if(Team == "")
		{
			if(Owner.GetComponent<ObjectEvents>() != null)
			{
				Team = Owner.GetComponent<ObjectEvents>().Team;
			}
		}
	}

	public void PreventParentCollision()
	{
		Collider[] ownerColliders = Owner.GetComponentsInChildren<Collider>();
		Collider[] myColliders = GetComponentsInChildren<Collider>();

		for(int i = 0; i < ownerColliders.Length; ++i)
		{
			for(int j = 0; j < myColliders.Length; ++j)
			{
				Physics.IgnoreCollision(ownerColliders[i], myColliders[j]);
			}
		}
	}

	public void Damaged(int damageTaken, GameObject attacker)
	{
		if(OnDamagedListeners != null)
		{
			OnDamagedListeners(damageTaken, attacker);
		}
	}

	public void Death(DeathType deathType, GameObject attacker)
	{
		if(OnDeathListeners != null)
		{
			OnDeathListeners(deathType, attacker);
		}

		Destroy(this.gameObject);
	}
}
