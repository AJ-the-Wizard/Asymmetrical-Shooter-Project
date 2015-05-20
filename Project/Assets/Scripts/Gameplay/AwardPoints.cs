using UnityEngine;
using System.Collections;
[RequireComponent (typeof (ObjectEvents))]

public class AwardPoints : MonoBehaviour
{
	ObjectEvents objectEvents;

	public int PointValue;
	public bool AwardOnScriptDeath = false;
	public bool AwardOnEnemyKill = false;
	public bool AwardOnAnyDeath = false;

	bool PointsAwarded = false;

	// Use this for initialization
	void Start ()
	{
		objectEvents = GetComponent<ObjectEvents>();

		objectEvents.OnDeathListeners += OnDeath;
	}
	
	void OnDeath(ObjectEvents.DeathType deathType, GameObject attacker)
	{
		if((PointValue == 0)
		   || PointsAwarded)
		{
			return;
		}
		
		if(((AwardOnScriptDeath == false) && (deathType == ObjectEvents.DeathType.Script))
		    || ((AwardOnEnemyKill == false) && (attacker.GetComponent<ObjectEvents>().Team != "Player")))
		{
			return;
		}

		GameObject controller = GameObject.FindWithTag("GameController");
		if(controller != null)
		{
			controller.GetComponent<LevelController>().AddScore(PointValue);
		}
	}

	void OnDestroy()
	{
		if(AwardOnAnyDeath
		   && (PointsAwarded == false))
		{
			GameObject controller = GameObject.FindWithTag("GameController");
			if(controller != null)
			{
				controller.GetComponent<LevelController>().AddScore(PointValue);
			}
		}
	}
}
