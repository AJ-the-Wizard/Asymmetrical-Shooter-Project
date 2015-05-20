using UnityEngine;
using System.Collections;

public class Trigger_OnShifted : Trigger
{
	public Deco_ShiftIntoPosition Deco;
	protected bool ActiveState;

	void Awake()
	{
		base.Awake();
	}

	void Start()
	{
		ActiveState = !Deco.StartActive;
	}
	
	void FixedUpdate()
	{
		if((Deco.GetTriggered() == ActiveState)
		   && (Deco.GetIsMoving() == false))
		{
			base.SetSwitches(true);
		}
		else
		{
			base.SetSwitches(false);
		}
	}
}
