using UnityEngine;
using System.Collections;

public class Region_Torque : MonoBehaviour
{
	public Vector3 Acceleration;
	public bool Impulse = true;
	public bool UseLocalRotation = true;

	void OnTriggerEnter(Collider other)
	{
		Rigidbody target = null;
		
		if((other != null)
		   && (other.gameObject != null))
		{
			target = other.gameObject.GetComponent<Rigidbody>();
		}
		
		if(target == null)
		{
			return;
		}
		
		if(Impulse)
		{
			if(UseLocalRotation)
			{
				target.angularVelocity += GameMath.AsLocalVector(this.transform, Acceleration);
			}
			else
			{
				target.angularVelocity += Acceleration;
			}
		}
	}
}
