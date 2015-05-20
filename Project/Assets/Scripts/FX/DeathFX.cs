using UnityEngine;
using System.Collections;
[RequireComponent (typeof (ObjectEvents))]

public class DeathFX : MonoBehaviour
{
	ObjectEvents objectEvents;

	public GameObject OnDeathFX;
	public bool PlayOnScriptDeath = false;

	// Use this for initialization
	void Start ()
	{
		objectEvents = GetComponent<ObjectEvents>();

		objectEvents.OnDeathListeners += OnDeath;
	}
	
	// Update is called once per frame
	void OnDeath (ObjectEvents.DeathType deathType, GameObject attacker)
	{
		if(PlayOnScriptDeath
		   || (deathType != ObjectEvents.DeathType.Script))
		{
			if(OnDeathFX != null)
			{
				GameObject fx = Instantiate(OnDeathFX,
				                            GetComponent<Transform>().position,
				                            GetComponent<Transform>().rotation) as GameObject;

				if(fx.GetComponent<ObjectEvents>() != null)
				{
					fx.GetComponent<ObjectEvents>().Owner = objectEvents.Owner;
					fx.GetComponent<ObjectEvents>().Team = objectEvents.Team;
				}
			}
		}
	}
}
