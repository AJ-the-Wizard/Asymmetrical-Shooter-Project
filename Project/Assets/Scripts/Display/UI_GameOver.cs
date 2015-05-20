using UnityEngine;
using System.Collections;

public class UI_GameOver : MonoBehaviour
{
	LevelController levelController;

	public GameObject GameOverObject;
	GUIText GameOverText;
	public GameObject RestartObject;
	GUIText RestartText;
	public GameObject ExitObject;
	GUIText ExitText;

	public float DelayUntilSelections = 2.0f;
	float DelayTimer;

	bool Active = false;
	int Selection = 0;

	// Use this for initialization
	void Start ()
	{
		levelController = GameObject.FindWithTag("GameController").GetComponent<LevelController>();

		GameOverText = GameOverObject.GetComponent<GUIText>();
		RestartText = RestartObject.GetComponent<GUIText>();
		ExitText = ExitObject.GetComponent<GUIText>();

		GameOverText.text = "";
		RestartText.text = "";
		ExitText.text = "";
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(Active == false)
		{
			if(levelController.IsGameOver()
			   && (levelController.IsGameWon() == false))
			{
				GameOverText.text = "Game Over";

				DelayTimer += Time.fixedDeltaTime;

                if(DelayTimer >= DelayUntilSelections)
				{
					Active = true;
				}
			}
		}
		if(Active)
		{
			if(Input.GetKeyDown(PlayerProfile.PivotDown)
			   || Controller.GetButtonDown(PlayerProfile.GamepadPivotDown))
			{
				Selection -= 1;
			}
			else if(Input.GetKeyDown(PlayerProfile.PivotUp)
			        || Controller.GetButtonDown(PlayerProfile.GamepadPivotUp))
			{
				Selection += 1;
			}

			if(Selection > 1)
			{
				Selection = 0;
			}
			else if(Selection < 0)
			{
				Selection = 1;
			}
			
			if(Selection == 0)
			{
				RestartText.text = "< Restart >";
				ExitText.text = "Exit";
			}
			else if(Selection == 1)
            {
                RestartText.text = "Restart";
                ExitText.text = "< Exit >";
            }

			if(Input.GetKeyDown(PlayerProfile.ShootMidSides)
			   || Controller.GetButtonDown(PlayerProfile.GamepadShootMidSides))
			{
				if(Selection == 0)
				{
					Application.LoadLevel(Application.loadedLevel);
				}
				else if(Selection == 1)
				{
                    Application.Quit();
                }
			}
		}
	}
}
