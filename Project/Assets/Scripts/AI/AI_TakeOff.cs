using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Transform))]
[RequireComponent (typeof(Rigidbody))]

public class AI_TakeOff : MonoBehaviour
{
	public Vector3 TakeOffInitialVel;
	public Vector3 TakeOffAccel;
	public float TakeOffDuration;

	public bool UseLocalVelocity;

	// Use this for initialization
	void Start ()
	{
	}
	
	public void TakeOff ()
	{
		StartCoroutine(TakeOffSequence(this.gameObject));
	}

	public void TakeOff (GameObject target)
	{
		StartCoroutine(TakeOffSequence(target));
	}
	
	IEnumerator TakeOffSequence(GameObject target)
	{
		float timer = 0f;
		Rigidbody targetBody = target.GetComponent<Rigidbody>();
		Vector3 takeOffAccel = TakeOffAccel;

		if(UseLocalVelocity)
		{
			targetBody.velocity = GameMath.AsLocalVector(targetBody.transform, TakeOffInitialVel);
			takeOffAccel = GameMath.AsLocalVector(targetBody.transform, takeOffAccel);
		}
		else
		{
			targetBody.velocity = TakeOffInitialVel;
		}

		if(target.GetComponent<AI_EnemyFighter>() != null)
		{
			target.GetComponent<AI_EnemyFighter>().MovementEnabled = false;
		}

		while(timer < TakeOffDuration)
		{
			targetBody.velocity += takeOffAccel * Time.fixedDeltaTime;
			
			timer += Time.fixedDeltaTime;
			yield return new WaitForSeconds(Time.fixedDeltaTime);
		}
		
		if(target.GetComponent<AI_EnemyFighter>() != null)
		{
			target.GetComponent<AI_EnemyFighter>().MovementEnabled = true;
		}
	}
}
