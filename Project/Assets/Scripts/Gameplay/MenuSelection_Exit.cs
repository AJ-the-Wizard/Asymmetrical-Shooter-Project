using UnityEngine;
using System.Collections;

public class MenuSelection_Exit : MenuSelection
{
	public override void Activate()
	{
		Application.Quit();
	}
}
