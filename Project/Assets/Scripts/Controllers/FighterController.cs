using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(Collider))]

public class FighterController : MonoBehaviour
{
	Transform transform;
	Rigidbody rigidBody;
	Collider collider;
	
	public float Speed = 6f;
	public float TurnSpeed = 6f;
	public float JumpStrength = 8f;
	public float JumpFudge = 0.1f;
	public float Gravity = 4f;

	// Use this for initialization
	void Awake ()
	{
		transform = GetComponent<Transform>();
		rigidBody = GetComponent<Rigidbody>();
		collider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	}

	public void Movement(Vector2 movement, Vector2 turning)
	{
		Vector3 velocity = new Vector3();
		velocity += transform.right * movement.x * Speed;
		velocity += transform.forward * movement.y * Speed;
		velocity.y += rigidBody.velocity.y;
		velocity.y -= Gravity * Time.fixedDeltaTime;

		rigidBody.velocity = velocity;
		
		rigidBody.angularVelocity = new Vector3(0f,
		                                        turning.x * TurnSpeed,
		                                        0f);
	}

	public void Jump()
	{
		Vector3 origin = transform.position;
		origin.y += (collider.bounds.extents.y - 0.001f) * -1f;
		Vector3 offset;
		Vector3 direction = transform.up * -1f;

		Ray[] rays = new Ray[4];
		offset = new Vector3(collider.bounds.extents.x,
		                     0f,
		                     collider.bounds.extents.z);
		rays[0] = new Ray(origin + offset, direction);

		offset = new Vector3(-collider.bounds.extents.x,
		                     0f,
		                     collider.bounds.extents.z);
		rays[1] = new Ray(origin + offset, direction);

		offset = new Vector3(collider.bounds.extents.x,
		                     0f,
		                     -collider.bounds.extents.z);
		rays[2] = new Ray(origin + offset, direction);

		offset = new Vector3(-collider.bounds.extents.x,
		                     0f,
		                     -collider.bounds.extents.z);
		rays[3] = new Ray(origin + offset, direction);

		RaycastHit[] hitInfos = new RaycastHit[4];

		for(int i = 0; i < rays.Length; ++i)
		{
			//Debug.DrawRay(origin, direction * JumpFudge);

			Physics.Raycast(rays[i], out hitInfos[i], JumpFudge);
		}

		foreach(RaycastHit hitInfo in hitInfos)
		{
			if(hitInfo.collider != null)
			{
				rigidBody.velocity += transform.up * JumpStrength;
				break;
			}
		}
	}
}
