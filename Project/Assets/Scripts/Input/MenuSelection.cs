using UnityEngine;
using System.Collections;
[RequireComponent (typeof(GUIText))]

public class MenuSelection : MonoBehaviour
{
	protected GUIText Text;

	public string BaseText;
	protected bool Selected = false;

	public string SelectedPreText;
	public string SelectedPostText;

	void Awake()
	{
		Text = GetComponent<GUIText>();

		if(BaseText == "")
		{
			BaseText = Text.text;
		}
	}

	public virtual void Activate()
	{
		return;
	}

	public bool GetSelected()
	{
		return Selected;
	}

	public void SetSelected(bool value)
	{
		Selected = value;

		if(Selected)
		{
			Text.text = string.Concat(SelectedPreText, BaseText, SelectedPostText);
		}
		else
		{
			Text.text = BaseText;
		}
	}
}
