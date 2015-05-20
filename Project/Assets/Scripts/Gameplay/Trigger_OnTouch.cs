using UnityEngine;
using System.Collections;

public class Trigger_OnTouch : Trigger
{
	public GameObject[] Targets = new GameObject[1];
	public string[] TargetTags = new string[1];
	public bool ToggleOnEnter = true;
	public bool ToggleOnExit = true;
	public bool EnabledWhenEntered = true;

	void Awake()
	{
		base.Awake();

		if(EnabledWhenEntered == false)
		{
			ToggleSwitches();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		OnEnter(other.gameObject);
	}
	
	void OnCollisionEnter(Collision other)
	{
		OnEnter(other.gameObject);
	}
	
	void OnTriggerExit(Collider other)
	{
		OnExit(other.gameObject);
	}
	
	void OnCollisionExit(Collision other)
    {
		OnExit(other.gameObject);
    }

	protected virtual void OnEnter(GameObject other)
	{
		if(EnabledWhenEntered != GetActive())
		{
			if(IsATarget(other.gameObject))
			{
				ToggleSwitches();
			}
        }
	}
	
	protected virtual void OnExit(GameObject other)
	{
		if(EnabledWhenEntered == GetActive())
		{
			if(IsATarget(other.gameObject))
			{
				ToggleSwitches();
			}
        }
    }

	protected bool IsATarget(GameObject other)
	{
		foreach(GameObject target in Targets)
		{
			if(other.gameObject == target)
			{
				return true;
			}
		}
		
		foreach(string tag in TargetTags)
		{
            if(other.gameObject.tag == tag)
            {
                return true;
            }
        }

		return false;
    }
}
