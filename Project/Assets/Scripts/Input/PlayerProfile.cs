using UnityEngine;
using System.Collections;

public class PlayerProfile : MonoBehaviour
{
	static public KeyCode YawLeft = KeyCode.LeftArrow;
	static public KeyCode YawRight = KeyCode.RightArrow;
	static public KeyCode PivotUp = KeyCode.DownArrow;
	static public KeyCode PivotDown = KeyCode.UpArrow;
	static public KeyCode BankLeft = KeyCode.Q;
	static public KeyCode BankRight = KeyCode.E;
	static public KeyCode ThrottleUp = KeyCode.CapsLock;
	static public KeyCode ThrottleDown = KeyCode.LeftShift;
	static public KeyCode ToggleThrust = KeyCode.X;
	
	static public KeyCode ShootMidSides = KeyCode.Space;
	static public KeyCode ShootFarSides = KeyCode.LeftControl;
	static public KeyCode ShootCenter = KeyCode.C;
	static public KeyCode ShootUp = KeyCode.W;
	static public KeyCode ShootLeft = KeyCode.A;
	static public KeyCode ShootDown = KeyCode.S;
	static public KeyCode ShootRight = KeyCode.D;
	
	static public Controller.Xbox360Button GamepadYawLeft = Controller.Xbox360Button.LeftStickLeftAxis;
	static public Controller.Xbox360Button GamepadYawRight = Controller.Xbox360Button.LeftStickRightAxis;
	static public Controller.Xbox360Button GamepadPivotUp = Controller.Xbox360Button.LeftStickUpAxis;
	static public Controller.Xbox360Button GamepadPivotDown = Controller.Xbox360Button.LeftStickDownAxis;
	static public Controller.Xbox360Button GamepadBankLeft = Controller.Xbox360Button.RightStickLeftAxis;
	static public Controller.Xbox360Button GamepadBankRight = Controller.Xbox360Button.RightStickRightAxis;
	static public Controller.Xbox360Button GamepadThrottleUp = Controller.Xbox360Button.LeftTriggerAxis;
	static public Controller.Xbox360Button GamepadThrottleDown = Controller.Xbox360Button.RightTriggerAxis;
	static public Controller.Xbox360Button GamepadToggleThrust = Controller.Xbox360Button.YButton;
	static public Controller.Xbox360Button GamepadLockOn = Controller.Xbox360Button.Unassigned;

	static public Controller.Xbox360Button GamepadShootMidSides = Controller.Xbox360Button.RightBumper;
	static public Controller.Xbox360Button GamepadShootFarSides = Controller.Xbox360Button.LeftBumper;
	static public Controller.Xbox360Button GamepadShootCenter = Controller.Xbox360Button.Unassigned;
	static public Controller.Xbox360Button GamepadShootUp = Controller.Xbox360Button.DPadDown;
	static public Controller.Xbox360Button GamepadShootLeft = Controller.Xbox360Button.DPadRight;
	static public Controller.Xbox360Button GamepadShootDown = Controller.Xbox360Button.DPadUp;
	static public Controller.Xbox360Button GamepadShootRight = Controller.Xbox360Button.DPadLeft;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
