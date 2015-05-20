using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Transform))]
[RequireComponent (typeof (Rigidbody))]

public class GameBoundaries : MonoBehaviour
{
	Rigidbody rigidBody;
	
	public string Type = "LockToScreen";
	public Vector3 Lowerbounds = new Vector3(-1000f, -1000f, -1000f);
	public Vector3 Upperbounds = new Vector3(1000f, 1000f, 1000f);
	public bool OnScreen = true;
	
	// Use this for initialization
	void Start ()
	{
		rigidBody = GetComponent<Rigidbody>();
	}
		
	// Update is called once per frame
	void FixedUpdate ()
	{
		Vector3 position = rigidBody.position;

		if(Type == "LockToScreen")
		{
			Vector3 velocity = rigidBody.velocity;

			if((position.x < Lowerbounds.x) || (position.x > Upperbounds.x))
			{
				velocity.x = 0.0f;
			}
			if((position.y < Lowerbounds.y) || (position.y > Upperbounds.y))
			{
				velocity.y = 0.0f;
			}
			if((position.z < Lowerbounds.z) || (position.z > Upperbounds.z))
			{
				velocity.z = 0.0f;
			}

			position.x = Mathf.Clamp(position.x, Lowerbounds.x, Upperbounds.x);
			position.y = Mathf.Clamp(position.y, Lowerbounds.y, Upperbounds.y);
			position.z = Mathf.Clamp(position.z, Lowerbounds.z, Upperbounds.z);

			rigidBody.position = position;
			rigidBody.velocity = velocity;
		}
		else if(Type == "DestroyOffscreen")
		{
			if(OnScreen == false)
			{
				Destroy(this.gameObject);
			}
		}
	}
	}
