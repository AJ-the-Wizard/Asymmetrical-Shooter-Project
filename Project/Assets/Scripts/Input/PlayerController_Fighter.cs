using UnityEngine;
using System.Collections;
[RequireComponent (typeof(FighterController))]
[RequireComponent (typeof(MagicController))]

public class PlayerController_Fighter : MonoBehaviour
{
	protected FighterController movementController;
	protected MagicController magicController;

	public Controller.Xbox360Button MoveForward = Controller.Xbox360Button.LeftStickUpAxis;
	public Controller.Xbox360Button MoveLeft = Controller.Xbox360Button.LeftStickLeftAxis;
	public Controller.Xbox360Button MoveBack = Controller.Xbox360Button.LeftStickDownAxis;
	public Controller.Xbox360Button MoveRight = Controller.Xbox360Button.LeftStickRightAxis;
	
	public Controller.Xbox360Button LookLeft = Controller.Xbox360Button.RightStickLeftAxis;
	public Controller.Xbox360Button LookRight = Controller.Xbox360Button.RightStickRightAxis;
	public Controller.Xbox360Button LookUp = Controller.Xbox360Button.RightStickDownAxis;
	public Controller.Xbox360Button LookDown = Controller.Xbox360Button.RightStickUpAxis;

	public Controller.Xbox360Button Jump = Controller.Xbox360Button.LeftTriggerAxis;
	public Controller.Xbox360Button Cast = Controller.Xbox360Button.RightTriggerAxis;
	public Controller.Xbox360Button PrevSpell = Controller.Xbox360Button.LeftBumper;
	public Controller.Xbox360Button NextSpell = Controller.Xbox360Button.RightBumper;

	// Use this for initialization
	void Start ()
	{
		movementController = GetComponent<FighterController>();
		magicController = GetComponent<MagicController>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		Vector2 movement = new Vector2();
		
		movement.y += Controller.GetAxis(MoveForward);
		movement.x -= Controller.GetAxis(MoveLeft);
		movement.y -= Controller.GetAxis(MoveBack);
		movement.x += Controller.GetAxis(MoveRight);

		Vector2 turning = new Vector2();
		
		turning.x += Input.GetAxis(Controller.RightStickXAxis);
		turning.y += Input.GetAxis(Controller.RightStickYAxis);

		if(Controller.GetButtonDown(Cast))
		{
			magicController.CastSpell();
		}
		else if(Controller.GetButtonUp(Cast))
		{
			magicController.EndSpell();
		}

		if(Controller.GetButtonDown(Jump))
		{
			movementController.Jump();
		}

		if(Controller.GetButtonDown(PrevSpell))
		{
			magicController.SelectCurrentSpellIndex(magicController.GetCurrentSpellIndex() - 1);
		}
		if(Controller.GetButtonDown(NextSpell))
		{
			magicController.SelectCurrentSpellIndex(magicController.GetCurrentSpellIndex() + 1);
		}
		
		movementController.Movement(movement, turning);
	}
}
