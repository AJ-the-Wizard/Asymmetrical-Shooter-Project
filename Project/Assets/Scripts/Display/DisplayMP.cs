using UnityEngine;
using System.Collections;
[RequireComponent (typeof(GUIText))]

public class DisplayMP : MonoBehaviour
{
	GUIText Text;

	public MagicController magicController;
	public string PreText;
	public string MidText;
	public string PostText;

	MagicSpell CurrentSpell;
	int MaxCasts;

	void Awake()
	{
		Text = GetComponent<GUIText>();
	}

	// Update is called once per frame
	void LateUpdate ()
	{
		if(magicController.GetCurrentSpell() != CurrentSpell)
		{
			CurrentSpell = magicController.GetCurrentSpell();

			Color mpColor = CurrentSpell.SpellColor;
			mpColor.r *= 0.5f;
			mpColor.g *= 0.5f;
			mpColor.b *= 0.5f;
			Text.color = mpColor;
			MaxCasts = (int) Mathf.Floor(magicController.MaxMP / CurrentSpell.MPCost);
		}

		int castsLeft = (int) Mathf.Floor(magicController.CurMP / CurrentSpell.MPCost);

		string text = string.Concat(PreText,
		                            castsLeft.ToString(),
		                            MidText,
		                            MaxCasts.ToString(),
		                            PostText);

		Text.text = text;
	}
}
