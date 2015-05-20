using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
	public enum Xbox360Button {Unassigned = 0,
		                       AButton = 1, BButton = 2, XButton = 3, YButton = 4, LeftBumper = 5,
							   RightBumper = 6, LeftStick = 7, RightStick = 8,
	                           LeftTriggerAxis = 9, RightTriggerAxis = 10, TriggersAxis = 11,
		                       LeftStickXAxis = 12, LeftStickYAxis = 13, RightStickXAxis = 14,
		                       RightStickYAxis = 15, DPadUp = 16, DPadLeft = 17, DPadDown = 18,
		                       DPadRight = 19, DPadXAxis = 20, DPadYAxis = 21,
	                           LeftStickUpAxis = 22, LeftStickLeftAxis = 23, LeftStickDownAxis = 24,
	                           LeftStickRightAxis = 25, RightStickUpAxis = 26, RightStickLeftAxis = 27,
	                           RightStickDownAxis = 28, RightStickRightAxis = 29};
	static Xbox360Button[] ButtonList = new Xbox360Button[30];
	static bool[] ButtonDown = new bool[30];
	static Vector2[] AxisCaps = new Vector2[30];

	public const string Unassigned = "Unassigned";

	public const string AButton = "A Button";
	public const string BButton = "B Button";
	public const string XButton = "X Button";
	public const string YButton = "Y Button";
	public const string LeftBumper = "Left Bumper";
	public const string RightBumper = "Right Bumper";
	public const string LeftStick = "Left Stick";
	public const string RightStick = "Right Stick";

	public const string LeftStickXAxis = "Left Stick X Axis";
	public const string LeftStickYAxis = "Left Stick Y Axis";
	public const string RightStickXAxis = "Right Stick X Axis";
	public const string RightStickYAxis = "Right Stick Y Axis";
	public const string LeftStickUpAxis = "Left Stick Up Axis";
	public const string LeftStickLeftAxis = "Left Stick Left Axis";
	public const string LeftStickDownAxis = "Left Stick Down Axis";
	public const string LeftStickRightAxis = "Left Stick Right Axis";
	public const string RightStickUpAxis = "Right Stick Up Axis";
	public const string RightStickLeftAxis = "Right Stick Left Axis";
	public const string RightStickDownAxis = "Right Stick Down Axis";
	public const string RightStickRightAxis = "Right Stick Right Axis";

	public const string LeftTriggerAxis = "Left Trigger Axis";
	public const string RightTriggerAxis = "Right Trigger Axis";
	public const string TriggerAxis = "Triggers Axis";
	public const string DPadUpAxis = "D-Pad Up Axis";
	public const string DPadLeftAxis = "D-Pad Left Axis";
	public const string DPadDownAxis = "D-Pad Down Axis";
	public const string DPadRightAxis = "D-Pad Right Axis";
	public const string DPadXAxis = "D-Pad X Axis";
	public const string DPadYAxis = "D-Pad Y Axis";

	public static GameObject UpdateChild;

	void Start()
	{
		Controller.Initialize(this.gameObject);
	}

	public static void Initialize(GameObject updateChild)
	{
		UpdateChild = updateChild;

		if((UpdateChild.GetComponent<Controller>()) == false)
		{
			UpdateChild.AddComponent<Controller>();
		}

		ButtonList[0] = Xbox360Button.LeftStick;
		ButtonList[1] = Xbox360Button.AButton;
		ButtonList[2] = Xbox360Button.BButton;
		ButtonList[3] = Xbox360Button.XButton;
		ButtonList[4] = Xbox360Button.YButton;
		ButtonList[5] = Xbox360Button.LeftBumper;
		ButtonList[6] = Xbox360Button.RightBumper;
		ButtonList[7] = Xbox360Button.LeftStick;
		ButtonList[8] = Xbox360Button.RightStick;
		ButtonList[9] = Xbox360Button.LeftTriggerAxis;
		ButtonList[10] = Xbox360Button.RightTriggerAxis;
		ButtonList[11] = Xbox360Button.TriggersAxis;
		ButtonList[12] = Xbox360Button.LeftStickXAxis;
		ButtonList[13] = Xbox360Button.LeftStickYAxis;
		ButtonList[14] = Xbox360Button.RightStickXAxis;
		ButtonList[15] = Xbox360Button.RightStickYAxis;
		ButtonList[16] = Xbox360Button.DPadUp;
		ButtonList[17] = Xbox360Button.DPadLeft;
		ButtonList[18] = Xbox360Button.DPadDown;
		ButtonList[19] = Xbox360Button.DPadRight;
		ButtonList[20] = Xbox360Button.DPadXAxis;
		ButtonList[21] = Xbox360Button.DPadYAxis;
		ButtonList[22] = Xbox360Button.LeftStickUpAxis;
		ButtonList[23] = Xbox360Button.LeftStickLeftAxis;
		ButtonList[24] = Xbox360Button.LeftStickDownAxis;
		ButtonList[25] = Xbox360Button.LeftStickRightAxis;
		ButtonList[26] = Xbox360Button.RightStickUpAxis;
		ButtonList[27] = Xbox360Button.RightStickLeftAxis;
		ButtonList[28] = Xbox360Button.RightStickDownAxis;
		ButtonList[29] = Xbox360Button.RightStickRightAxis;

		AxisCaps[0] = new Vector2(0f, 0f);
		AxisCaps[1] = new Vector2(0f, 1f);
		AxisCaps[2] = new Vector2(0f, 1f);
		AxisCaps[3] = new Vector2(0f, 1f);
		AxisCaps[4] = new Vector2(0f, 1f);
		AxisCaps[5] = new Vector2(0f, 1f);
		AxisCaps[6] = new Vector2(0f, 1f);
		AxisCaps[7] = new Vector2(0f, 1f);
		AxisCaps[8] = new Vector2(0f, 1f);
		AxisCaps[9] = new Vector2(0f, 1f);
		AxisCaps[10] = new Vector2(0f, 1f);
		AxisCaps[11] = new Vector2(-1f, 1f);
		AxisCaps[12] = new Vector2(-1f, 1f);
		AxisCaps[13] = new Vector2(-1f, 1f);
		AxisCaps[14] = new Vector2(-1f, 1f);
		AxisCaps[15] = new Vector2(-1f, 1f);
		AxisCaps[16] = new Vector2(0f, 1f);
		AxisCaps[17] = new Vector2(0f, 1f);
		AxisCaps[18] = new Vector2(0f, 1f);
		AxisCaps[19] = new Vector2(0f, 1f);
		AxisCaps[20] = new Vector2(-1f, 1f);
		AxisCaps[21] = new Vector2(-1f, 1f);
		AxisCaps[22] = new Vector2(0f, 1f);
		AxisCaps[23] = new Vector2(0f, 1f);
		AxisCaps[24] = new Vector2(0f, 1f);
		AxisCaps[25] = new Vector2(0f, 1f);
		AxisCaps[26] = new Vector2(0f, 1f);
		AxisCaps[27] = new Vector2(0f, 1f);
		AxisCaps[28] = new Vector2(0f, 1f);
		AxisCaps[29] = new Vector2(0f, 1f);
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		for(int i = 0; i < Controller.ButtonDown.Length; ++i)
		{
			Controller.ButtonDown[i] = Controller.GetButton(Controller.ButtonList[i]);
		}
	}

	static bool WasButtonDown(Xbox360Button button)
	{
		for(int i = 0; i < ButtonDown.Length; ++i)
		{
			if(ButtonList[i] == button)
			{
				return ButtonDown[i];
			}
		}

		return false;
	}
	
	public static bool GetButton(Xbox360Button button)
	{
		string name = GetButtonName(button);

		if(name.Substring(name.Length - 4, 4) == "Axis")
		{
			return (Input.GetAxis(name) > 0f);
		}
		else
		{
			return Input.GetButton(name);
		}
	}
	
	public static bool GetButtonDown(Xbox360Button button)
	{
		return GetButton(button) && (WasButtonDown(button) == false);
	}
	
	public static bool GetButtonUp(Xbox360Button button)
	{
		return (GetButton(button) == false) && WasButtonDown(button);
	}

	public static float GetAxis(Xbox360Button axis)
	{
		string name = GetButtonName(axis);

		if(name.Substring(name.Length - 4, 4) != "Axis")
		{
			if(Input.GetButton(name))
			{
				return 1f;
			}
			else
			{
				return 0f;
			}
		}
		else
		{
			return Mathf.Clamp(Input.GetAxis(name),
			                   AxisCaps[(int) axis].x,
			                   AxisCaps[(int) axis].y);
		}
	}

	public static string GetButtonName(Xbox360Button button)
	{
		switch((int) button)
		{
			case 0:
				return Unassigned;
			break;

			case 1:
				return AButton;
			break;

			case 2:
				return BButton;
			break;

			case 3:
				return XButton;
			break;

			case 4:
				return YButton;
			break;

			case 5:
				return LeftBumper;
			break;

			case 6:
				return RightBumper;
			break;

			case 7:
				return LeftStick;
			break;

			case 8:
				return RightStick;
			break;

			case 9:
				return LeftTriggerAxis;
			break;

			case 10:
				return RightTriggerAxis;
			break;

			case 11:
				return TriggerAxis;
			break;

			case 12:
				return LeftStickXAxis;
			break;

			case 13:
				return LeftStickYAxis;
			break;

			case 14:
				return RightStickXAxis;
			break;

			case 15:
				return RightStickYAxis;
			break;

			case 16:
				return DPadUpAxis;
			break;

			case 17:
				return DPadLeftAxis;
			break;

			case 18:
				return DPadDownAxis;
			break;

			case 19:
				return DPadRightAxis;
			break;

			case 20:
				return DPadXAxis;
			break;

			case 21:
				return DPadYAxis;
			break;

			case 22:
				return LeftStickUpAxis;
			break;

			case 23:
				return LeftStickLeftAxis;
			break;

			case 24:
				return LeftStickDownAxis;
			break;

			case 25:
				return LeftStickRightAxis;
			break;

			case 26:
				return RightStickUpAxis;
			break;

			case 27:
				return RightStickLeftAxis;
			break;

			case 28:
				return RightStickDownAxis;
			break;

			case 29:
				return RightStickRightAxis;
			break;
		}

		return "ERROR";
	}
}
