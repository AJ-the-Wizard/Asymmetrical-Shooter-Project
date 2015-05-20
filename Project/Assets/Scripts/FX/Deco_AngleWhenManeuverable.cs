using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Transform))]

public class Deco_AngleWhenManeuverable : MonoBehaviour
{
	Transform transform;

	public SpaceShipController Owner;
	public Vector3 ManeuveringAngle;
	Vector3 BaseAngle;

	// Use this for initialization
	void Start ()
	{
		transform = GetComponent<Transform>();

		BaseAngle = transform.localRotation.eulerAngles;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(Owner.HighManeuvering)
		{
			transform.localRotation = Quaternion.Euler(ManeuveringAngle);
		}
		else
		{
			transform.localRotation = Quaternion.Euler(BaseAngle);
		}
	}
}
