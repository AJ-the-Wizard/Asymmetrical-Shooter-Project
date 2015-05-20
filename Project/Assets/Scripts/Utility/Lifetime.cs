using UnityEngine;
using System.Collections;

public class Lifetime : MonoBehaviour
{
	public float TotalLifetime = 5.0f;
	float LifetimeTimer;

	void Start ()
	{
		LifetimeTimer = TotalLifetime;
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		LifetimeTimer -= Time.deltaTime;

		if(LifetimeTimer <= 0)
		{
			if(GetComponent<ObjectEvents>() != null)
			{
				GetComponent<ObjectEvents>().Death(ObjectEvents.DeathType.Script, null);
			}

			DestroyObject(this.gameObject);
		}
	}
}
