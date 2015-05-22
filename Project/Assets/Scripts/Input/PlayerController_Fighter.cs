using UnityEngine;
using System.Collections;
[RequireComponent (typeof(FighterController))]
[RequireComponent (typeof(MagicController))]

public class PlayerController_Fighter : MonoBehaviour
{
	protected FighterController movementController;

	public Controller.Xbox360Button MoveForward = Controller.Xbox360Button.LeftStickUpAxis;
	public Controller.Xbox360Button MoveLeft = Controller.Xbox360Button.LeftStickLeftAxis;
	public Controller.Xbox360Button MoveBack = Controller.Xbox360Button.LeftStickDownAxis;
	public Controller.Xbox360Button MoveRight = Controller.Xbox360Button.LeftStickRightAxis;
	
	public Controller.Xbox360Button LookLeft = Controller.Xbox360Button.RightStickLeftAxis;
	public Controller.Xbox360Button LookRight = Controller.Xbox360Button.RightStickRightAxis;
	public Controller.Xbox360Button LookUp = Controller.Xbox360Button.RightStickDownAxis;
	public Controller.Xbox360Button LookDown = Controller.Xbox360Button.RightStickUpAxis;

	public Controller.Xbox360Button Jump = Controller.Xbox360Button.LeftTriggerAxis;

	// Use this for initialization
	void Start ()
	{
		movementController = GetComponent<FighterController>();
	}

	void Update()
	{
		
		if(Controller.GetButtonDown(Jump))
		{
			movementController.Jump();
		}
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

		movementController.Movement(movement, turning);
	}
}
