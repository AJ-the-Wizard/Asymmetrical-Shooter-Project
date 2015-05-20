using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Rigidbody))]

public class Deco_Bob : MonoBehaviour
{
	private Transform transform;
	private Rigidbody rigidBody;

	public float Movement;
	public float Interval;

	private Vector3 InitialPosition;
	public bool GoingPositive = true;

	void Awake()
	{
		transform = GetComponent<Transform>();
		rigidBody = GetComponent<Rigidbody>();

		InitialPosition = transform.localPosition;
	}
	
	void FixedUpdate()
	{
		float percent = (transform.localPosition.y - InitialPosition.y) / (Movement * 1.1f);
		percent = Mathf.Abs(percent);

		Vector3 velocity = new Vector3();
		velocity.y = Mathf.Cos(percent * Mathf.PI * 0.5f) * (8f * Movement / Interval);

		if(GoingPositive == false)
		{
			velocity.y *= -1f;
		}

		rigidBody.velocity = velocity;

		if(GoingPositive)
		{
			if(transform.localPosition.y >= InitialPosition.y + Movement)
			{
				GoingPositive = false;
			}
		}
		else
		{
			if(transform.localPosition.y <= InitialPosition.y - Movement)
			{
				GoingPositive = true;
			}
		}
		
	}
}
