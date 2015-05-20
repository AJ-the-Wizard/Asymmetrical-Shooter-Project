using UnityEngine;
using System.Collections;

public class Trigger_OnSwitches : Trigger
{
	public enum SwitchType {All, Any, Different, Same};

	public Switch[] TriggeringSwitches = new Switch[1];
	public SwitchType TriggerCondition = SwitchType.All;

	void Awake()
	{
		base.Awake();
	}
	
	void FixedUpdate()
	{
		bool beingTriggered = true;

		if(TriggerCondition == SwitchType.All)
		{
			foreach(Switch target in TriggeringSwitches)
			{
				if(target.GetActive() == false)
				{
					beingTriggered = false;
					break;
				}
			}
		}
		else if(TriggerCondition == SwitchType.Any)
		{
			beingTriggered = false;

			foreach(Switch target in TriggeringSwitches)
			{
				if(target.GetActive())
				{
					beingTriggered = true;
					break;
				}
			}
		}
		else if(TriggerCondition == SwitchType.Different)
		{
			bool setting = TriggeringSwitches[0].GetActive();
			beingTriggered = false;

			for(int i = 1; i < TriggeringSwitches.Length; ++i)
			{
				if(TriggeringSwitches[i].GetActive() != setting)
				{
					beingTriggered = true;
					break;
				}
			}
		}
		else if(TriggerCondition == SwitchType.Same)
		{
			bool setting = TriggeringSwitches[0].GetActive();
			beingTriggered = false;

			for(int i = 1; i < TriggeringSwitches.Length; ++i)
			{
				if(TriggeringSwitches[i].GetActive() == setting)
				{
					beingTriggered = true;
					break;
				}
			}
		}

		base.SetSwitches(beingTriggered);
	}
}
