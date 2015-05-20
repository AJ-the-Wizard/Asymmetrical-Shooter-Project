using UnityEngine;
using System.Collections;
[RequireComponent (typeof(ObjectEvents))]

public class Trigger : MonoBehaviour
{
	protected ObjectEvents objectEvents;

	public Switch[] Switches = new Switch[1];
	public bool StartActive = false;
	private bool Active;
	
	public void Awake()
	{
		objectEvents = GetComponent<ObjectEvents>();

		Active = StartActive;
	}

	public void SetSwitches(bool active)
	{
		if(active != Active)
		{
			ToggleSwitches();
		}
	}
	
	public virtual void ToggleSwitches()
	{
		Active = !Active;

		foreach(Switch target in Switches)
		{
			target.SetSwitch(Active);
		}
	}

	public bool GetActive()
	{
		return Active;
	}
}
