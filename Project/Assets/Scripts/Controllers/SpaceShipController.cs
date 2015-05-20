using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Rigidbody))]

public class SpaceShipController : MonoBehaviour
{
	Rigidbody rigidBody;

	float BaseDrag;

	float Throttle = 0f;
	Vector3 AppliedVelocity = new Vector3();

	public float MaxSpeed = 10f;
	public float AccelSpeed = 10f;
	public float BrakeSpeed = 10f;
	public float MaxBoostThrottle = 1.5f;
	public float MaxReverseThrottle = -0.2f;
	public float TurnSpeed = 90f;

	public bool StraightMovement = false;
	public bool CutThrust = false;
	public bool HighManeuvering = false;
	public float HighManeuveringAccel = 10f;
	float BaseAccel;

	public float InitialThrottle = 0f;

	public bool Enabled = true;

	// Use this for initialization
	void Start ()
	{
		rigidBody = GetComponent<Rigidbody>();

		BaseDrag = rigidBody.drag;

		BaseAccel = AccelSpeed;

		Throttle = InitialThrottle;
	}

	public void Movement(Vector3 turning, float throttleAccel, int reverseOrBoost = 0)
	{
		if(Enabled)
		{
			Movement(turning);
			Movement(throttleAccel, reverseOrBoost);
		}
	}
	
	public void Movement(Quaternion turning, float throttleAccel, int reverseOrBoost = 0)
	{
		if(Enabled)
		{
			Movement(turning);
			Movement(throttleAccel, reverseOrBoost);
		}
	}

	void Movement(Vector3 turning)
	{
		float inputLength = Mathf.Min(Vector3.Distance(new Vector3(), turning), 1f);
		turning.Normalize();
		turning *= Time.fixedDeltaTime * TurnSpeed * inputLength;
		GetComponent<Transform>().Rotate(turning);
	}
	
	void Movement(Quaternion turning)
	{
		transform.rotation = Quaternion.RotateTowards(transform.rotation,
		                                              turning,
		                                              Time.fixedDeltaTime * TurnSpeed);
	}

	void Movement(float throttleAccel, int reverseOrBoost)
	{
		Throttle += throttleAccel;

		float maxThrottle = 1f;
		float minThrottle = 0f;

		if(reverseOrBoost == 1)
		{
			maxThrottle = MaxBoostThrottle;
		}
		else if(reverseOrBoost == -1)
		{
			minThrottle = MaxReverseThrottle;
		}

		Throttle = Mathf.Clamp(Throttle + throttleAccel,
		                       minThrottle,
		                       maxThrottle);

		if(AppliedVelocity.sqrMagnitude > 0f)
		{
			float length = AppliedVelocity.magnitude - (BrakeSpeed * Time.fixedDeltaTime);

			Vector3 newVelocity;

			if(length > 0f)
			{
				newVelocity = new Vector3(AppliedVelocity.x, AppliedVelocity.y, AppliedVelocity.z);
				newVelocity.Normalize();
				newVelocity *= length;
			}
			else
			{
				newVelocity = new Vector3();
			}

			rigidBody.velocity -= (AppliedVelocity - newVelocity);
			AppliedVelocity = newVelocity;
		}

		if(CutThrust)
		{
			return;
		}
		else if(StraightMovement)
		{
			rigidBody.velocity = transform.forward * MaxSpeed * Throttle;
		}
		else
		{
			/*
			Vector3 velocity = rigidBody.velocity;
			if(Throttle > 0f)
			{
				velocity += rigidBody.transform.forward * (AccelSpeed * Time.fixedDeltaTime);
			}
			else
			{
				velocity -= rigidBody.transform.forward * (AccelSpeed * Time.fixedDeltaTime);
			}
			float magnitude = Mathf.Min(Mathf.Abs(MaxSpeed * Throttle), velocity.magnitude);
			velocity.Normalize();
			velocity *= magnitude;
			rigidBody.velocity = velocity;
			*/

			float acceleration = AccelSpeed * Time.fixedDeltaTime;
			Vector3 targetVel = rigidBody.transform.forward * Throttle * MaxSpeed;
			Vector3 currentVel = rigidBody.velocity - AppliedVelocity;
			Vector3 thrustVel = targetVel - currentVel;

			if(Vector3.Distance(targetVel, currentVel) < acceleration)
			{
				rigidBody.velocity = targetVel + AppliedVelocity;
			}
			else
			{
				thrustVel.Normalize();
				thrustVel *= acceleration;
				thrustVel += currentVel + AppliedVelocity;
				rigidBody.velocity = thrustVel;
			}
		}
	}

	public void ToggleCutThrust()
	{
		CutThrust = !CutThrust;

		if(CutThrust)
		{
			rigidBody.drag = 0f;
		}
		else
		{
			rigidBody.drag = BaseDrag;
		}
	}
	
	public void ToggleHighManeuvering()
	{
		HighManeuvering = !HighManeuvering;
		
		if(HighManeuvering)
		{
			AccelSpeed = HighManeuveringAccel;
		}
		else
		{
			AccelSpeed = BaseAccel;
		}
	}

	public void ApplyVelocity(Vector3 velocity)
	{
		AppliedVelocity += velocity;
		rigidBody.velocity += velocity;
	}

	public bool GetCutThrust()
	{
		return CutThrust;
	}

	public float GetThrottle()
	{
		return Throttle;
	}

	public float GetMaxBoost()
	{
		return MaxBoostThrottle;
	}

	public float GetMaxReverse()
	{
		return MaxReverseThrottle;
	}

	public void SetMaxBoost(float value)
	{
		MaxBoostThrottle = Mathf.Max(value, 1f);
	}

	public void SetMaxReverse(float value)
	{
		MaxReverseThrottle = Mathf.Min(value, 0f);
	}
}
