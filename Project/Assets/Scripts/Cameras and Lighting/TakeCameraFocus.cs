using UnityEngine;
using System.Collections;

public class TakeCameraFocus : MonoBehaviour
{
	Camera_LookAtTarget MainCamera;

	// Use this for initialization
	void Start ()
	{
		MainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera_LookAtTarget>();
	}

	void FixedUpdate ()
	{
		if(MainCamera.Target == null)
		{
			MainCamera.Target = this.gameObject;
		}
	}
}
