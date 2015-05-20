using UnityEngine;
using System.Collections;
[RequireComponent (typeof(ObjectEvents))]

public class Switch : MonoBehaviour
{
	protected ObjectEvents objectEvents;

	public bool ActiveByDefault = false;
	public bool OnlyOneToggle = false;
	public bool Toggleable = true;

	protected int ToggleCount;
	protected bool Active;

	public void Awake()
	{
		objectEvents = GetComponent<ObjectEvents>();
		Active = ActiveByDefault;
	}
	
	public void SetSwitch(bool active)
	{
		if(active != Active)
		{
			ToggleSwitch();
		}
	}

	public virtual void ToggleSwitch()
	{
		if(CanBeToggled())
		{
			Active = !Active;
			ToggleCount += 1;
		}
	}

	public bool CanBeToggled()
	{
		if(OnlyOneToggle && (ToggleCount > 0))
		{
			return false;
		}

		return Toggleable;
	}

	public int GetToggleCount()
	{
		return ToggleCount;
	}

	public bool GetActive()
	{
		return Active;
	}
}
