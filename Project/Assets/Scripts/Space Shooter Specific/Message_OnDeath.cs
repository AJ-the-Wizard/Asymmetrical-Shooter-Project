using UnityEngine;
using System.Collections;
[RequireComponent (typeof(ObjectEvents))]

public class Message_OnDeath : MonoBehaviour
{
	public string Message;

	// Use this for initialization
	void Start ()
	{
		GetComponent<ObjectEvents>().OnDeathListeners += OnDeath;
	}
	
	void OnDeath(ObjectEvents.DeathType deathType, GameObject attacker)
	{
		if(Message != "")
		{
			GameObject.FindWithTag("GameController").GetComponent<LevelController>().WritePlayerWarning(Message);
		}
	}
}
