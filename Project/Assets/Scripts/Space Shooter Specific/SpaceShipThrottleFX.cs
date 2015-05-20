using UnityEngine;
using System.Collections;

public class SpaceShipThrottleFX : MonoBehaviour
{
	public ScaleParticles scaleParticles;
	public SpaceShipController shipController;

	public float SizeAtZeroThrottle = 0f;
	public float SizeAtFullThrottle = 1f;
	public float SizeAtFullBoost = 1f;
	public float SizeAtFullReverse = 0f;

	// Use this for initialization
	void Start ()
	{
		scaleParticles = GetComponent<ScaleParticles>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		float throttle = shipController.GetThrottle();
		float throttlePercent = 0f;
		float scale = 0f;

		if(shipController.GetCutThrust())
		{
			scale = SizeAtZeroThrottle;
		}
		else if(throttle < 0f)
		{
			throttlePercent = Mathf.Clamp(throttle / shipController.GetMaxReverse(),
			                              0f, 1f);

			scale = (SizeAtFullReverse * throttlePercent)
				    + (SizeAtZeroThrottle * (1f - throttlePercent));
		}
		else if(throttle > 1f)
		{
			throttlePercent = Mathf.Clamp(throttle / shipController.GetMaxBoost(),
			                              0f, 1f);
			
			scale = (SizeAtFullBoost * throttlePercent)
			    	+ (SizeAtFullThrottle * (1f - throttlePercent));
		}
		else
		{
			throttlePercent = Mathf.Clamp(throttle / 1f,
			                              0f, 1f);

			scale = (SizeAtFullThrottle * throttlePercent)
	     			+ (SizeAtZeroThrottle * (1 - throttlePercent));
		}

		if(scale <= 0f)
		{
			scaleParticles.SetScale(0f, 0f);
		}
		else
		{
			scaleParticles.SetScale(scale, 1 / scale / scale);
		}
	}
}
