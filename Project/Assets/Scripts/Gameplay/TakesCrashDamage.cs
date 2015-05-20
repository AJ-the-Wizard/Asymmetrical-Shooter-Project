using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Health))]

public class Health_CrashDamage : MonoBehaviour
{
	protected Health health;

	public float MinCrashSpeed = 6;
	public int MinCrashDamage = 1;

	public int MaxCrashDamage = 1;
	public float CrashSpeedIncrement = 0;
	public int CrashDamageIncrement = 0;

	void Awake()
	{
		health = GetComponent<Health>();
	}
	
	void OnCollisionEnter(Collision collision)
	{
		float crashSpeed = collision.relativeVelocity.magnitude;
		if(crashSpeed > MinCrashSpeed)
		{
			int damage = MinCrashDamage;

			if(CrashSpeedIncrement > 0f)
			{
				int increments = (int) Mathf.Floor((crashSpeed - MinCrashSpeed) / CrashSpeedIncrement);
				damage = Mathf.Min(MaxCrashDamage, damage + (increments * CrashDamageIncrement));
			}

			health.TakeDamage(damage, collision.contacts[0].otherCollider.gameObject);
		}
	}
}
