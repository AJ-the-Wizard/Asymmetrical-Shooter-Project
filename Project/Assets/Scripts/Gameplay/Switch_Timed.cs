using UnityEngine;
using System.Collections;

public class Switch_Timed : Switch
{
	public float TimerLength = 1f;
	protected float TimerCount;
	
	void Awake()
	{
		base.Awake();
	}
	
	void FixedUpdate()
	{
		if(Active != ActiveByDefault)
		{
			TimerCount += Time.fixedDeltaTime;

			if(TimerCount > TimerLength)
			{
				base.ToggleSwitch();
			}
		}
		else
		{
			TimerCount = 0f;
		}
	}

	public override void ToggleSwitch()
	{
		if(Active == ActiveByDefault)
		{
			base.ToggleSwitch();
		}
	}
}
