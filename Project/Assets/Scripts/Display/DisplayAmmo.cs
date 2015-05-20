using UnityEngine;
using System.Collections;
[RequireComponent (typeof(GUIText))]

public class DisplayAmmo : MonoBehaviour
{
	GUIText Text;

	public WeaponHardpoint[] Hardpoints;
	public string AmmoCharacter = "{}";
	public bool UseLineBreaks = false;
	public string SpacingCharacter = "";

	// Use this for initialization
	void Start ()
	{
		Text = GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		Text.text = "";
		int totalAmmo = 0;

		for(int i = 0; i < Hardpoints.Length; ++i)
		{
			if(Hardpoints[i].AmmoMax != -1)
			{
				totalAmmo += Hardpoints[i].AmmoLeft;
			}
		}

		for(int i = 0; i < totalAmmo; ++i)
		{
			if(i > 0)
			{
				if(UseLineBreaks)
				{
					Text.text = string.Concat(Text.text, "\n");
				}
				else
				{
					Text.text = string.Concat(Text.text, SpacingCharacter);
				}
			}

			Text.text = string.Concat(Text.text, AmmoCharacter);
		}
	}
}
