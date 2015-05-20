using UnityEngine;
using System.Collections;
[RequireComponent (typeof(GUIText))]

public class DisplayButton : MonoBehaviour
{
	private GUIText Text;

	public Controller.Xbox360Button TargetButton;
	public string UnpressedText;
	public string ReleasedText;
	public string PressedText;
	public string HeldText;

	// Use this for initialization
	void Awake ()
	{
		Text = GetComponent<GUIText>();	
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		if(Controller.GetButtonDown(TargetButton))
		{
			Text.text = PressedText;
		}
		else if(Controller.GetButton(TargetButton))
		{
			Text.text = HeldText;
		}
		else if(Controller.GetButtonUp(TargetButton))
		{
			Text.text = ReleasedText;
		}
		else if(Controller.GetButton(TargetButton) == false)
		{
			Text.text = UnpressedText;
		}
		else
		{
			Text.text = "Button state could not be found.";
		}
		Text.text = string.Concat(Text.text, ": ", Controller.GetAxis(TargetButton).ToString());
	}
}
