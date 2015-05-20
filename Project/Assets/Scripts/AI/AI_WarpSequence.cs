using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Rigidbody))]

public class AI_WarpSequence : MonoBehaviour
{
	private Transform transform;
	private Rigidbody rigidBody;

	private bool Warping = false;

	public float BaseZLength = 1f;
	public float WarmUpTime = 1.5f;
	public float ZoomOffTime = 1.5f;
	public float ZoomOffSpeed = 1.0f;
	public float StretchFactor = 10f;

	// Use this for initialization
	void Start ()
	{
		transform = GetComponent<Transform>();
		rigidBody = GetComponent<Rigidbody>();
	}
	
	public void StartWarp(bool destroyOnEnd = true)
	{
		StartCoroutine(StartWarpSequence(destroyOnEnd));
	}

	public bool IsWarping()
	{
		return Warping;
	}

	IEnumerator StartWarpSequence(bool destroyOnEnd)
	{
		Warping = true;

		bool colliderEnabled = false;
		if(GetComponent<Collider>())
		{
			colliderEnabled = GetComponent<Collider>().enabled;
			GetComponent<Collider>().enabled = false;
		}

		float timer = 0f;
		Vector3 basePos = rigidBody.position;
		Vector3 baseScale = rigidBody.transform.localScale;
		float length = 1f;
		rigidBody.velocity = new Vector3();

		while(timer < (WarmUpTime + ZoomOffTime))
		{
			length = (baseScale.z * (timer * StretchFactor));

			transform.localScale = new Vector3(transform.localScale.x,
			                                   transform.localScale.y,
			                                   baseScale.z + length);
			transform.position = basePos + (transform.forward * length * BaseZLength);

			if(timer > WarmUpTime)
			{
				transform.position += transform.forward * ((timer - WarmUpTime) * length) * ZoomOffSpeed;
			}

			timer += Time.fixedDeltaTime;
			yield return new WaitForSeconds(Time.fixedDeltaTime);
		}

		if(colliderEnabled)
		{
			GetComponent<Collider>().enabled = true;
		}

		Warping = false;

		if(destroyOnEnd)
		{
			Destroy(this.gameObject);
		}
	}
}
