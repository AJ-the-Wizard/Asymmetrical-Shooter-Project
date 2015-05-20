using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Health))]

public class Trigger_OnDamage : Trigger
{
	public int MinimumDamage = 0;

	void Awake()
	{
		base.Awake();

		objectEvents.OnDamagedListeners += OnDamaged;
	}

	void FixedUpdate()
	{
	
	}

	void OnDamaged(int damageTaken, GameObject attacker)
	{
		if(damageTaken >= MinimumDamage)
		{
			base.ToggleSwitches();
		}
	}
}
