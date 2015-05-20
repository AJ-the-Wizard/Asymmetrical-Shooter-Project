using UnityEngine;
using System.Collections;

public class Region_LocalSpace : MonoBehaviour
{
	public Transform Parent;

	void Awake()
	{
		if(Parent == null)
		{
			Parent = this.transform;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.transform.parent == null)
		{
			other.transform.SetParent(Parent);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.transform.parent == Parent)
		{
			other.transform.SetParent(null);
		}
	}
}
