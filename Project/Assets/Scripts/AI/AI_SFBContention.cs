using UnityEngine;
using System.Collections;

public class AI_SFBContention : MonoBehaviour
{
	private Transform transform;
	//private Rigidbody rigidBody;
	private SpaceShipController shipController;

	public Deco_ShiftIntoPosition[] CombatPlating;
	
	private bool InCombat = false;

	void Awake ()
	{
		transform = GetComponent<Transform>();
		//rigidBody = GetComponent<Rigidbody>();
		shipController = GetComponent<SpaceShipController>();
	}

	void FixedUpdate()
	{
		if(transform.rotation.eulerAngles.y < 45f)
		{
			shipController.Movement(new Vector3(0f, 0f, 0f),
			                        0f,
			                        0);
		}
	}

	public void SetInCombat(bool value)
	{
		if(InCombat == value)
		{
			return;
		}
		InCombat = value;

		foreach(Deco_ShiftIntoPosition plating in CombatPlating)
		{
			plating.SetTriggered(!value);
		}
	}
	
	public bool GetInCombat()
	{
		return InCombat;
	}
}
