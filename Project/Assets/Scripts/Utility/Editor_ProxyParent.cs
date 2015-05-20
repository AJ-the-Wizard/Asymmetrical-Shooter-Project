using UnityEngine;
using System.Collections;

public class Editor_ProxyParent : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		GetComponent<Transform>().DetachChildren();
		DestroyObject(this.gameObject);
	}
}
