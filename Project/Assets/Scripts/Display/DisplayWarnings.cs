using UnityEngine;
using System.Collections;
[RequireComponent (typeof (GUIText))]

public class DisplayWarnings : MonoBehaviour
{
	private GUIText Text;

	private bool Writing = false;
	private string LastMessage = "";
	private string[] MessagesWaiting = new string[5];

	public float WriteTime = 1f;
	public float ClearTime = 1f;
	public float HoldTime = 1f;

	// Use this for initialization
	void Start ()
	{
		Text = GetComponent<GUIText>();
        
		for(int i = 0; i < MessagesWaiting.Length; ++i)
		{
			MessagesWaiting[i] = "";
		}

		Text.text = "";
	}
	
	public void WriteMessage(string text)
	{
		if(Writing)
		{
			AddToWaitlist(text);
		}
		else
		{
			StartCoroutine(WriteText(text));
		}
	}

	void AddToWaitlist(string text)
	{
		for(int i = 0; i < MessagesWaiting.Length; ++i)
		{
			if(MessagesWaiting[i] == "")
			{
				MessagesWaiting[i] = text;
				return;
			}
		}
	}

	void UpdateWaitlist()
	{
		if(MessagesWaiting[0] != "")
		{
			string text = MessagesWaiting[0];

			for(int i = 0; (i + 1) < MessagesWaiting.Length; ++i)
			{
				MessagesWaiting[i] = MessagesWaiting[i + 1];
			}

			MessagesWaiting[MessagesWaiting.Length - 1] = "";

			WriteMessage(text);
		}
	}

	IEnumerator WriteText(string text)
	{
		if(Writing)
		{
			StopCoroutine(WriteText(LastMessage));
		}

		Writing = true;
		LastMessage = text;
		Text.text = "";

		for(int i = 0; i < text.Length; ++i)
		{
			Text.text = string.Concat(Text.text, text.Substring(i, 1));
			yield return new WaitForSeconds(Time.deltaTime * WriteTime);
		}

		yield return new WaitForSeconds(HoldTime);
		
		for(int i = Text.text.Length; i >= 0; --i)
		{
			Text.text = Text.text.Substring(0, i);
			yield return new WaitForSeconds(Time.deltaTime * ClearTime);
        }
		
		Text.text = "";
		Writing = false;

		UpdateWaitlist();
	}
}