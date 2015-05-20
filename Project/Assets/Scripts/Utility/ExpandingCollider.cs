using UnityEngine;
using System.Collections;
[RequireComponent (typeof (SphereCollider))]

public class ExpandingCollider : MonoBehaviour
{
	SphereCollider sphereCollider;

	public float StartSize;
	public float EndSize;
	public float ExpandInterval;
	public float ColliderLifetime = 0f;
	float ExpandTimer;

	public bool Enabled = true;
	
	// Use this for initialization
	void Start ()
	{
		sphereCollider = GetComponent<SphereCollider>();

		sphereCollider.radius = StartSize;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(Enabled == false)
		{
			return;
		}

		ExpandTimer += Time.fixedDeltaTime;

		if((ColliderLifetime <= 0f)
		   || (ExpandTimer < ColliderLifetime))
		{
			float percent = Mathf.Min(1f, ExpandTimer / ExpandInterval);
			float size = (percent * (EndSize - StartSize)) + StartSize;
			sphereCollider.radius = size;
		}
		else
		{
			sphereCollider.enabled = false;
			Enabled = false;
		}
	}
}
