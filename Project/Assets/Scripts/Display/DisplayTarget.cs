using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Transform))]

public class DisplayTarget : MonoBehaviour
{
	Transform transform;

	public GameObject Target;

	// Use this for initialization
	void Start ()
	{
		transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		if(Target == null)
		{
			Destroy(this.gameObject);
		}

		Vector3 position = GameMath.GetScreenPosition(Target);

		if(position.z < 0f)
		{
			transform.position = new Vector3(-500f, -500f, 0f);
		}
		else
		{
			position.x /= Screen.width;
			position.y /= Screen.height;
			transform.position = position;
		}
	}
}
