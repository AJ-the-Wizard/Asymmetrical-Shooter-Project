using UnityEngine;
using System.Collections;

public class UI_Success : MonoBehaviour
{
	LevelController levelController;
	
	public GameObject SuccessObject;
	GUIText SuccessText;
	public GameObject NextObject;
	GUIText NextText;
	public GameObject RestartObject;
	GUIText RestartText;
	public GameObject ExitObject;
	GUIText ExitText;

	public string NextLevel;

	bool Active = false;
	int Selection = 0;

	int MinSelection;
	int MaxSelection;
	
	// Use this for initialization
	void Start ()
	{
		levelController = GameObject.FindWithTag("GameController").GetComponent<LevelController>();
		
		NextText = NextObject.GetComponent<GUIText>();
		SuccessText = SuccessObject.GetComponent<GUIText>();
		RestartText = RestartObject.GetComponent<GUIText>();
		ExitText = ExitObject.GetComponent<GUIText>();
		
		NextText.text = "";
		SuccessText.text = "";
		RestartText.text = "";
		ExitText.text = "";

		if(NextLevel != "")
		{
			MinSelection = 0;
		}
		else
		{
			MinSelection = 1;
		}
		MaxSelection = 2;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(Active == false)
		{
			if(levelController.IsGameOver()
			   && levelController.IsGameWon())
			{
				SuccessText.text = "Success";

				Active = true;
			}
		}
		if(Active)
		{
			if(Input.GetKeyDown(PlayerProfile.PivotDown)
			   || Controller.GetButtonDown(PlayerProfile.GamepadPivotDown))
			{
				Selection += 1;
			}
			else if(Input.GetKeyDown(PlayerProfile.PivotUp)
			        || Controller.GetButtonDown(PlayerProfile.GamepadPivotUp))
			{
				Selection -= 1;
			}
			
			if(Selection > MaxSelection)
			{
				Selection = MinSelection;
			}
			else if(Selection < MinSelection)
			{
				Selection = MaxSelection;
			}
			
			if(Selection == 0)
			{
				NextText.text = "< Next Level >";
				RestartText.text = "Restart";
				ExitText.text = "Exit";
			}
			else if(Selection == 1)
			{
				NextText.text = "Next Level";
				RestartText.text = "< Restart >";
				ExitText.text = "Exit";
			}
			else if(Selection == 2)
			{
				NextText.text = "Next Level";
				RestartText.text = "Restart";
				ExitText.text = "< Exit >";
			}

			if(MinSelection > 0)
			{
				NextText.text = "";
			}

			if(Input.GetKeyDown(PlayerProfile.ShootMidSides)
			   || Controller.GetButtonDown(PlayerProfile.GamepadShootMidSides))
			{
				if(Selection == 0)
				{
					Application.LoadLevel(NextLevel);
				}
				else if(Selection == 1)
				{
					Application.LoadLevel(Application.loadedLevel);
				}
				else if(Selection == 2)
				{
					Application.Quit();
				}
			}
		}
	}
}
