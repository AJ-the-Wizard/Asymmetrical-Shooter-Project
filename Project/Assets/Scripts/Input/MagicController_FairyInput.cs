using UnityEngine;
using System.Collections;

public class MagicController_FairyInput : MonoBehaviour
{
	public MagicController LeftHand;
	public MagicController RightHand;
	
	public Controller.Xbox360Button CastLeftHand = Controller.Xbox360Button.LeftTriggerAxis;
	public Controller.Xbox360Button CastRightHand = Controller.Xbox360Button.RightTriggerAxis;

	void Update()
	{
		if(Controller.GetButtonDown(CastLeftHand))
		{
			LeftHand.CastSpell();
		}
		else if(Controller.GetButtonUp(CastLeftHand))
		{
			LeftHand.EndSpell();
		}

		if(Controller.GetButtonDown(CastRightHand))
		{
			RightHand.CastSpell();
		}
		else if(Controller.GetButtonUp(CastRightHand))
		{
			RightHand.EndSpell();
		}
	}
}
