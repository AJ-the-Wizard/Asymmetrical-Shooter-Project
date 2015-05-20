using UnityEngine;
using System.Collections;

public class Region_TargetEntered : MonoBehaviour 
{
	public GameObject TargetObject;

	bool Triggered = false;

	void OnTriggerEnter (Collider other)
	{
		if(TargetObject != null)
		{
			if(other.gameObject == TargetObject)
			{
				Triggered = true;
			}
		}
	}

	void OnTriggerExit (Collider other)
	{
		if(TargetObject != null)
		{
			if(other.gameObject == TargetObject)
			{
				Triggered = false;
			}
		}
	}

	public bool GetTriggered()
	{
		return Triggered;
	}
}
