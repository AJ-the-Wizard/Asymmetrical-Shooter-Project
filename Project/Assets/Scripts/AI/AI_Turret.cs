using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Transform))]
[RequireComponent (typeof (AI_Targeting))]

public class AI_Turret : MonoBehaviour
{
	Transform transform;
	AI_Targeting targeting;
	
	Quaternion DefaultAngle;
	
	public float AimingSpeed = 100f;
	public Vector3 MinRotation = new Vector3(0f, 0f, 0f);
	public Vector3 MaxRotation = new Vector3(360f, 360f, 360f);
	public bool LeadTargets = false;
	public float ShotSpeed = 0f;

	public WeaponHardpoint[] Hardpoints;

	public bool Enabled = true;
	
	// Use this for initialization
	void Start ()
	{
		transform = GetComponent<Transform>();
		targeting = GetComponent<AI_Targeting>();
		
		DefaultAngle = transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(Enabled == false)
		{
			return;
		}

		targeting.UpdateTarget();

		if(targeting.GetCurrentTarget() == null)
		{
			transform.rotation = Quaternion.RotateTowards(transform.rotation,
			                                              DefaultAngle,
			                                              Time.fixedDeltaTime * AimingSpeed);
		}
		else
		{
			if(targeting.GetCurrentTargetRange() <= AI_Targeting.TargetingRange.Targeting)
			{
				Vector3 target;

				if(LeadTargets)
				{
					target = targeting.GetInterceptPoint(ShotSpeed);
				}
				else
				{
					target = targeting.GetCurrentTarget().GetComponent<Transform>().position;
				}

				Vector3 relativePosition = new Vector3();
				relativePosition.x = target.x - transform.position.x;
				relativePosition.y = target.y - transform.position.y;
				relativePosition.z = target.z - transform.position.z;
				
				Quaternion targetAngle = Quaternion.LookRotation(relativePosition);
				
				transform.rotation = Quaternion.RotateTowards(transform.rotation,
				                                              targetAngle,
				                                              Time.fixedDeltaTime * AimingSpeed);

				Vector3 endAngle = transform.localRotation.eulerAngles;
				endAngle.x = Mathf.Clamp(endAngle.x, MinRotation.x, MaxRotation.x);
				endAngle.y = Mathf.Clamp(endAngle.y, MinRotation.y, MaxRotation.y);
				endAngle.z = Mathf.Clamp(endAngle.z, MinRotation.z, MaxRotation.z);
				transform.localRotation = Quaternion.Euler(endAngle);

				if(targeting.GetCurrentTargetRange() <= AI_Targeting.TargetingRange.Firing)
				{
					foreach(WeaponHardpoint hardpoint in Hardpoints)
					{
						hardpoint.Shoot();
					}
				}
			}
		}
	}
}