using UnityEngine;
using System.Collections;

public class MagicController_WizardInput : MonoBehaviour
{
	protected MagicController magicController;

	public Controller.Xbox360Button Cast = Controller.Xbox360Button.RightTriggerAxis;
	public Controller.Xbox360Button PrevSpell = Controller.Xbox360Button.LeftBumper;
	public Controller.Xbox360Button NextSpell = Controller.Xbox360Button.RightBumper;

	void Update()
	{
		if(Controller.GetButtonDown(Cast))
		{
			magicController.CastSpell();
		}
		else if(Controller.GetButtonUp(Cast))
		{
			magicController.EndSpell();
		}
		
		if(Controller.GetButtonDown(PrevSpell))
		{
			magicController.SelectCurrentSpellIndex(magicController.GetCurrentSpellIndex() - 1);
		}
		if(Controller.GetButtonDown(NextSpell))
		{
			magicController.SelectCurrentSpellIndex(magicController.GetCurrentSpellIndex() + 1);
		}
	}
}
