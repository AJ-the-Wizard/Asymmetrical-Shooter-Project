using UnityEngine;
using System.Collections;
[RequireComponent (typeof(GUIText))]

public class DisplayMPBar : MonoBehaviour
{
    private GUIText Text;

	public MagicController magicController;
    public string Character;
    public int MaxCharacterCount;

	private MagicSpell CurrentSpell;

	void Awake()
	{
		Text = GetComponent<GUIText>();
	}
	
	void LateUpdate()
	{
	    if(magicController.GetCurrentSpell() != CurrentSpell)
		{
			CurrentSpell = magicController.GetCurrentSpell();

			Color mpColor = CurrentSpell.SpellColor;
			mpColor.r *= 0.5f;
			mpColor.g *= 0.5f;
			mpColor.b *= 0.5f;
			Text.color = mpColor;
		}

		float percent = (float) magicController.CurMP / magicController.MaxMP;
		int characters = (int) Mathf.Floor(percent * MaxCharacterCount);
		string text = "";

		for(int i = 0; i < characters; ++i)
		{
			text = string.Concat(text, "=");
		}

		Text.text = text;
	}
}
