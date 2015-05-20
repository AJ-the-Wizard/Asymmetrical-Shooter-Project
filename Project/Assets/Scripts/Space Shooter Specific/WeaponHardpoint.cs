using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Transform))]
[RequireComponent (typeof (ObjectEvents))]

public class WeaponHardpoint : MonoBehaviour
{
	Transform transform;
	Rigidbody rigidBody;
	ObjectEvents objectEvents;
	AudioSource audioSource;

	public GameObject Shot;

	public float CoolDown = 1.0f;
	public float ChargeUp = 0.0f;
	float CoolDownTimer = 0.0f;
	bool Charging = false;

	public int AmmoMax = -1;
	public int AmmoLeft;

	public bool ShotsInheritVelocity;

	public WeaponHardpoint[] CooldownGroup;

	// Use this for initialization
	void Start ()
	{
		transform = GetComponent<Transform>();
		rigidBody = GetComponent<Rigidbody>();
		objectEvents = GetComponent<ObjectEvents>();
		audioSource = GetComponent<AudioSource>();

		if(rigidBody == null)
		{
			rigidBody = objectEvents.Owner.GetComponent<Rigidbody>();
		}

		AmmoLeft = AmmoMax;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		CoolDownTimer -= Time.fixedDeltaTime;
	}

	public bool CanShoot()
	{
		return (CoolDownTimer <= 0f)
			   && ((AmmoMax == -1) || (AmmoLeft > 0));
	}
	
	public bool CanGroupShoot()
	{
		bool canShoot = true;
		for(int i = 0; i < CooldownGroup.Length; ++i)
		{
			canShoot = (canShoot && CooldownGroup[i].CanShoot());
		}
		
		return canShoot;
	}

	public float GetCooldown()
	{
		return CoolDownTimer;
	}

	public GameObject Shoot (Vector3 vector)
	{
		GameObject shot = Shoot();

		if(shot != null)
		{
			shot.GetComponent<Transform>().rotation = Quaternion.FromToRotation(new Vector3(0f, 0f, 1f),
			                                                                    vector);
		}

		return shot;
	}

	public GameObject Shoot ()
	{
		if((CanShoot() && CanGroupShoot()) == false)
		{
			return null;
		}

		if(ChargeUp >= 0)
		{
			if(Charging)
			{
				Charging = false;
			}
			else
			{
				CoolDownTimer = ChargeUp;
				Charging = true;
				return null;
			}
		}

		GameObject shot = Instantiate(Shot,
		                              transform.position,
		                              transform.rotation) as GameObject;
		shot.GetComponent<Transform>().rotation = transform.rotation;

		if(shot.GetComponent<ObjectEvents>() != null)
		{
			shot.GetComponent<ObjectEvents>().Owner = objectEvents.Owner;
			shot.GetComponent<ObjectEvents>().Team = objectEvents.Team;
		}

		if(ShotsInheritVelocity
		   && (rigidBody != null))
		{
			shot.GetComponent<Rigidbody>().velocity += rigidBody.velocity;
		}

		if(audioSource != null)
		{
			audioSource.Play();
		}

		if(AmmoMax != -1)
		{
			AmmoLeft -= 1;
		}

		CoolDownTimer = CoolDown;
		return shot;
	}

	public void AddCooldown(float Value)
	{
		if(Value > 0)
		{
			CoolDownTimer += Value;
		}
	}
}
