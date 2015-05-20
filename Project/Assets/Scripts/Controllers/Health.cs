
// Flaw: Crash damage does not account for wall's velocity or angle of impact.

using UnityEngine;
using System.Collections;
[RequireComponent (typeof (ObjectEvents))]

public class Health : MonoBehaviour
{
	ObjectEvents objectEvents;

	public int MaxHealth = 1;
	public int CurHealth;
	public float InvulnInterval = 1.0f;
	private float InvulnTimer = 0.0f;

	public GameObject OnHitFX;

	public bool TakesCrashDamage = false;
	public float MinCrashSpeed = 6f;
	public int CrashDamage = 1;

	public bool Invincible = false;

	// Use this for initialization
	void Start ()
	{
		objectEvents = GetComponent<ObjectEvents>();

		CurHealth = MaxHealth;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(InvulnTimer > 0.0f)
		{
			InvulnTimer -= Time.deltaTime;
		}
	}

	public int TakeDamage (int Value, GameObject attacker)
	{
		string attackerTeam = "";

		if((attacker != null)
		   && (attacker.GetComponent<ObjectEvents>() != null))
		{
			attackerTeam = attacker.GetComponent<ObjectEvents>().Team;
		}

		if((InvulnTimer > 0.0f)
		   || ((attackerTeam != "") && (attackerTeam == objectEvents.Team)))
		{
			return 0;
		}

		int prevHealth = CurHealth;

		if(Invincible == false)
		{
			CurHealth = Mathf.Clamp(CurHealth - Value, 0, MaxHealth);
			InvulnTimer = InvulnInterval;
		}

		if(CurHealth == 0)
		{
			if(GetComponent<ObjectEvents>() != null)
			{
				GetComponent<ObjectEvents>().Death(ObjectEvents.DeathType.Damage, attacker);
			}

			DestroyObject(this.gameObject);
		}
		else
		{
			if(OnHitFX != null)
			{
				Instantiate(OnHitFX, GetComponent<Transform>().position, GetComponent<Transform>().rotation);
			}
		}

		objectEvents.Damaged(prevHealth - CurHealth, attacker);
		return prevHealth - CurHealth;
	}
}
