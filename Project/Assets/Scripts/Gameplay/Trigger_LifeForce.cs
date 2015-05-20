using UnityEngine;
using System.Collections;

public class Trigger_LifeForce : Trigger
{
	public GameObject[] LoadBearingObjects;
	public bool ActiveWhenAlive = false;
	
	void Awake()
	{
		base.Awake();
	}
	
	void FixedUpdate()
	{
		bool alive = false;

		foreach(GameObject target in LoadBearingObjects)
		{
			if(target != null)
			{
				alive = true;
				break;
			}
		}

		base.SetSwitches(alive == ActiveWhenAlive);
	}
}
