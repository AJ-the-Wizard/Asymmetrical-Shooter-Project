using UnityEngine;
using System.Collections;

public class HardpointHandler : MonoBehaviour
{
	public WeaponHardpoint HardPoint_MidLeft;
	public WeaponHardpoint HardPoint_MidRight;
	public WeaponHardpoint HardPoint_FarLeft;
	public WeaponHardpoint HardPoint_FarRight;
	public WeaponHardpoint HardPoint_Center;
	public WeaponHardpoint HardPoint_Pivot;
	
	double FireFromMidLeftInterval = 0.0f;
	double FireFromMidLeftTimer = 0.0f;
	bool FireFromMidLeft = false;

	double FireFromFarLeftInterval = 0.0f;
	double FireFromFarLeftTimer = 0.0f;
	bool FireFromFarLeft = false;
	
	public float MainWeaponFireDuration = 4.0f;
	public float MainWeaponCooldownTime = 6.0f;
	private bool MainWeaponFiring = false;
	private float MainWeaponFireTimer = 0.0f;

	// Use this for initialization
	void Start ()
	{
		if(HardPoint_MidLeft != null)
		{
			if(HardPoint_MidRight != null)
			{
				FireFromMidLeftInterval = (HardPoint_MidLeft.GetComponent<WeaponHardpoint>().CoolDown
				                           + HardPoint_MidRight.GetComponent<WeaponHardpoint>().CoolDown)
					                       * 0.5;
				FireFromMidLeftTimer = FireFromMidLeftInterval;
			}
			else
			{
				FireFromMidLeftInterval = Time.fixedDeltaTime;
				FireFromMidLeftTimer = FireFromMidLeftInterval;
			}
		}

		if(HardPoint_FarLeft != null)
		{
			FireFromFarLeftInterval = (HardPoint_FarLeft.GetComponent<WeaponHardpoint>().CoolDown
			                           + HardPoint_FarRight.GetComponent<WeaponHardpoint>().CoolDown)
				                       * 0.5;
			FireFromFarLeftTimer = FireFromFarLeftInterval;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		FireFromMidLeftTimer -= Time.fixedDeltaTime;
		if(FireFromMidLeftTimer <= 0)
		{
			FireFromMidLeftTimer = FireFromMidLeftInterval;
			FireFromMidLeft = !FireFromMidLeft;
		}

		FireFromFarLeftTimer -= Time.fixedDeltaTime;
		if(FireFromFarLeftTimer <= 0)
		{
			FireFromFarLeftTimer = FireFromFarLeftInterval;
			FireFromFarLeft = !FireFromFarLeft;
		}
	}
	
	public void ShootMidSides()
	{
		if(FireFromMidLeft)
		{
			if(HardPoint_MidLeft != null)
			{
				HardPoint_MidLeft.Shoot();
			}
		}
		else
		{
			if(HardPoint_MidRight != null)
            {
                HardPoint_MidRight.Shoot();
			}
		}
	}
	
	public void ShootFarSides()
	{
		if(FireFromFarLeft)
		{
			if(HardPoint_FarLeft != null)
            {
                HardPoint_FarLeft.Shoot();
			}
		}
		else
		{
			if(HardPoint_FarRight != null)
            {
                HardPoint_FarRight.Shoot();
			}
		}
	}
	
	public void ShootPivot(Vector3 vector)
	{
		if(HardPoint_Pivot != null)
		{
            HardPoint_Pivot.Shoot(vector);
		}
	}
	
	public void ShootCenter()
	{
		if(HardPoint_Center != null)
		{
            if(HardPoint_Center.CanShoot() && HardPoint_Center.CanGroupShoot())
			{
				MainWeaponFireTimer = MainWeaponFireDuration;
				MainWeaponFiring = true;

				StartCoroutine(FireMainWeapon());
			}
		}
	}

	public bool CanSideWeaponsShoot()
	{
		return (   ((HardPoint_MidLeft == null) || (HardPoint_MidLeft.CanShoot())
  		        && ((HardPoint_MidRight == null) || (HardPoint_MidRight.CanShoot())))
		        && ((HardPoint_FarLeft == null) || (HardPoint_FarLeft.CanShoot()))
		        && ((HardPoint_FarRight == null) || (HardPoint_FarRight.CanShoot())));
	}

	public bool CanAllWeaponsShoot()
	{
		return ((MainWeaponFiring == false)
		        && ((HardPoint_Center == null) || (HardPoint_Center.CanShoot()))
		        && ((HardPoint_MidLeft == null) || (HardPoint_MidLeft.CanShoot())
				&& ((HardPoint_MidRight == null) || (HardPoint_MidRight.CanShoot())))
				&& ((HardPoint_FarLeft == null) || (HardPoint_FarLeft.CanShoot()))
   		        && ((HardPoint_FarRight == null) || (HardPoint_FarRight.CanShoot()))
		        && ((HardPoint_Pivot == null) || (HardPoint_Pivot.CanShoot())));
	}

	public bool IsMainWeaponFiring()
	{
		return MainWeaponFiring;
	}

	public bool IsMainWeaponCooling()
	{
		return ((MainWeaponFiring == false)
		        && (HardPoint_Center.GetCooldown() >= 0));
	}

	IEnumerator FireMainWeapon()
	{
		while(MainWeaponFiring)
		{
			if(MainWeaponFireTimer <= 0)
			{
				HardPoint_Center.AddCooldown(MainWeaponCooldownTime);

				MainWeaponFiring = false;
			}

			HardPoint_Center.Shoot();

			MainWeaponFireTimer -= Time.fixedDeltaTime;
			yield return new WaitForSeconds(Time.fixedDeltaTime * 0.5f);
		}
	}
}
