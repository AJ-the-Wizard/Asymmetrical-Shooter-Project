using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Deco_ShiftIntoPosition))]

public class Switch_ShiftIntoPosition : Switch
{
	protected Deco_ShiftIntoPosition Deco;
	
	void Awake()
	{
		base.Awake();

		Deco = GetComponent<Deco_ShiftIntoPosition>();
		Active = Deco.StartActive;
	}

	public override void ToggleSwitch()
	{
		if(CanBeToggled())
		{
			base.ToggleSwitch();

			Deco.SetTriggered(Active);
		}
	}
}
