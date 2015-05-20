using UnityEngine;
using System.Collections;
[RequireComponent (typeof (GUIText))]

public class DisplayScore : MonoBehaviour
{
	GUIText Text;

	public string BaseText;
	public GameObject GameController;

	// Use this for initialization
	void Start ()
	{
		Text = GetComponent<GUIText>();

		if(BaseText == "")
		{
			BaseText = Text.text;
		}

		if(GameController == null)
		{
			GameController = GameObject.FindWithTag("GameController");
		}
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		string text = string.Concat(BaseText, GameController.GetComponent<LevelController>().Score.ToString());
		Text.text = text;
	}
}
