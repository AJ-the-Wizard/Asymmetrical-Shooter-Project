using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
	public Controller.Xbox360Button TabUp = Controller.Xbox360Button.LeftStickUpAxis;
	public Controller.Xbox360Button TabDown = Controller.Xbox360Button.LeftStickDownAxis;
	public Controller.Xbox360Button Select = Controller.Xbox360Button.RightTriggerAxis;
	public Controller.Xbox360Button SelectAlt = Controller.Xbox360Button.AButton;

    public MenuSelection[] Selections;
    protected int SelectionIndex;

    public bool Enabled = false;
	
	void Awake()
	{
	
	}
	
	void FixedUpdate()
	{
		Enabled = GetComponent<GUIText>().enabled;

	    if(Enabled)
        {
            if(Controller.GetButtonDown(TabUp))
			{
				Selections[SelectionIndex].SetSelected(false);
				SelectionIndex += 1;
			}
			if(Controller.GetButtonDown(TabDown))
			{
				Selections[SelectionIndex].SetSelected(false);
				SelectionIndex -= 1;
			}

			if(SelectionIndex < 0)
			{
				SelectionIndex = Selections.Length - 1;
			}
			else if(SelectionIndex > (Selections.Length - 1))
			{
				SelectionIndex = 0;
			}
			Selections[SelectionIndex].SetSelected(true);

			if(Controller.GetButtonDown(Select)
			   || Controller.GetButtonDown(SelectAlt))
			{
				Selections[SelectionIndex].Activate();
			}
        }
	}
}
