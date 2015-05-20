using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Rigidbody))]

public class Deco_ShiftIntoPosition : MonoBehaviour
{
	private Transform transform;
	private Rigidbody rigidBody;

	private Vector3 RestPosition;

	public Vector3 ActivePosition;
	public float MoveSpeed = 1f;
	public bool StartActive = false;

	private bool Active = false;
	private bool Moving = false;
	private Vector3 MoveVel = new Vector3();

	// Use this for initialization
	void Awake ()
	{
		transform = GetComponent<Transform>();
		rigidBody = GetComponent<Rigidbody>();
		
		if(StartActive == false)
		{
			RestPosition = transform.localPosition;
			ActivePosition = transform.localPosition + ActivePosition;
		}
		else
		{
			RestPosition = transform.localPosition - ActivePosition;
			ActivePosition = transform.localPosition;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(Moving)
		{
			Vector3 target;

			if(Active)
			{
				target = ActivePosition;
			}
			else
			{
				target = RestPosition;
			}

			if(Vector3.SqrMagnitude(target - transform.localPosition)
			   <= (MoveVel.sqrMagnitude * Time.fixedDeltaTime))
			{
				if(Active)
				{
					transform.localPosition = ActivePosition;
				}
				else
				{
					transform.localPosition = RestPosition;
				}

				Moving = false;
				rigidBody.velocity = new Vector3();
			}
			else
			{
				rigidBody.velocity = MoveVel;
			}
		}
	}

	public void SetTriggered(bool value)
	{
		Active = value;
		Moving = true;

		if(Active)
		{
			MoveVel = ActivePosition - transform.localPosition;
		}
		else
		{
			MoveVel = RestPosition - transform.localPosition;
		}

		MoveVel.Normalize();
		MoveVel *= MoveSpeed;
		rigidBody.velocity = MoveVel;
	}

	public bool GetTriggered()
	{
		return Active;
	}

	public bool GetIsMoving()
	{
		return Moving;
	}
}
