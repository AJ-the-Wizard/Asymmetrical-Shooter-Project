using UnityEngine;
using System.Collections;

public class AI_Transport : MonoBehaviour
{
	public GameObject[] Cargo;

	// Use this for initialization
	void Start ()
	{
		GetComponent<ObjectEvents>().OnDeathListeners += OnDeath;

		foreach(GameObject ship in Cargo)
		{
			ship.GetComponent<Health>().Invincible = true;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnDeath(ObjectEvents.DeathType deathType, GameObject attacker)
	{
		for(int i = 0; i < Cargo.Length; ++i)
		{
			Cargo[i].GetComponent<Health>().Invincible = false;
			Cargo[i].GetComponent<Health>().TakeDamage(9999, attacker);
		}
	}
}
