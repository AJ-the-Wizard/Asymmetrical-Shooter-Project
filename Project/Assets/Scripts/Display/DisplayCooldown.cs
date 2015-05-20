using UnityEngine;
using System.Collections;
[RequireComponent (typeof (GUIText))]

public class DisplayCooldown : MonoBehaviour
{
	GUIText Text;

	public HardpointHandler Target;
	private float FXTimer;

	// Use this for initialization
	void Start ()
	{
		Text = GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(Target.IsMainWeaponFiring())
		{
			Text.text = "Firing!";
		}
		else if(Target.IsMainWeaponCooling())
		{
			FXTimer += Time.fixedDeltaTime;

			if(FXTimer > 3f)
			{
				FXTimer = 0f;
			}
			else if(FXTimer > 2f)
			{
				Text.text = "Cooling...";
			}
			else if(FXTimer > 1f)
			{
				Text.text = "Cooling..";
			}
			else
			{
				Text.text = "Cooling.";
			}
		}
		else
		{
			Text.text = "";
		}
	}
}
