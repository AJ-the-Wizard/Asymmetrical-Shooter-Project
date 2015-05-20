using UnityEngine;
using System.Collections;

public class AI_FireConstantly : MonoBehaviour
{
	public WeaponHardpoint Hardpoint;
	public float FirstShotDelay;
	public bool Enabled = true;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(Enabled == false)
		{
			return;
		}

		if(FirstShotDelay > 0)
		{
			FirstShotDelay -= Time.fixedDeltaTime;
			return;
		}

		Hardpoint.Shoot();
	}
}
