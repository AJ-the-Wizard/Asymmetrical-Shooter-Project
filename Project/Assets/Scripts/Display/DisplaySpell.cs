using UnityEngine;
using System.Collections;
[RequireComponent (typeof(GUIText))]

public class DisplaySpell : MonoBehaviour
{
	GUIText Text;

	public MagicController magicController;
	public string PreText;
	public string PostText;
	public int IndexOffset = 0;

	// Use this for initialization
	void Awake ()
	{
		Text = GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		if(magicController.GetSpellsUpdated())
		{
			MagicSpell displayedSpell = magicController.GetSpellAtIndex(magicController.GetCurrentSpellIndex() + IndexOffset);

			string text = string.Concat(PreText, displayedSpell.name, PostText);
			Text.text = text;
			Text.color = displayedSpell.SpellColor;
		}
	}
}
