using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Transform))]
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (SpaceShipController))]
[RequireComponent (typeof (HardpointHandler))]
[RequireComponent (typeof (AI_Targeting))]

public class AI_EnemyFighter : MonoBehaviour
{
	public enum TargetingType {Hostile, Friendly};

	Transform transform;
	//Rigidbody rigidBody;
	SpaceShipController shipController;
	HardpointHandler hardpointHandler;
	AI_Targeting targeting;

	public float ThrottleSpeed = 5f;

	public bool LeadTargets = false;
	public float ShotSpeed = 0f;

	public bool MovementEnabled = true;
	public bool WeaponsEnabled = true;

	GameObject DefaultDestination;

	// Use this for initialization
	void Start ()
	{
		transform = GetComponent<Transform>();
		//rigidBody = GetComponent<Rigidbody>();
		shipController = GetComponent<SpaceShipController>();
		hardpointHandler = GetComponent<HardpointHandler>();
		targeting = GetComponent<AI_Targeting>();

		DefaultDestination = GameObject.Find("EnemyDestination");
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(MovementEnabled == false)
		{
			return;
		}

		targeting.UpdateTarget();

		if(targeting.GetUpdatedThisFrame())
		{
			if(targeting.GetCurrentTarget() == null)
			{
				if(targeting.HasTarget(DefaultDestination) == false)
				{
					targeting.AddTarget(DefaultDestination, AI_Targeting.TargetingType.Traveling);
					targeting.UpdateTarget();
				}
			}
		}

		if(targeting.GetCurrentTarget() != null)
		{
			Vector3 target; 

			if((LeadTargets == false)
			   || (targeting.GetCurrentTargetRange() > AI_Targeting.TargetingRange.Firing))
			{
				target = targeting.GetCurrentTarget().GetComponent<Transform>().position;
			}
			else
			{
				target = targeting.GetInterceptPoint(ShotSpeed);
			}

			Vector3 relativePosition = new Vector3();
			relativePosition.x = target.x - transform.position.x;
			relativePosition.y = target.y - transform.position.y;
			relativePosition.z = target.z - transform.position.z;

			Quaternion targetAngle = Quaternion.LookRotation(relativePosition);

			if(targeting.GetCurrentTargetRange() == AI_Targeting.TargetingRange.Following)
			{
				shipController.Movement(targetAngle, Time.fixedDeltaTime * -ThrottleSpeed, -1);
			}
			else if(targeting.GetCurrentTargetRange() < AI_Targeting.TargetingRange.OutOfRange)
            {
				shipController.Movement(targetAngle, Time.fixedDeltaTime * ThrottleSpeed, 0);
			}
            
			if(targeting.GetCurrentTargetRange() <= AI_Targeting.TargetingRange.Firing)
            {
				Shoot();
			}
		}
	}

	void Shoot()
	{
		if((WeaponsEnabled == false)
		   || (targeting.GetCurrentTargetingType() == AI_Targeting.TargetingType.Friendly))
		{
			return;
		}

		hardpointHandler.ShootMidSides();
	}
}
