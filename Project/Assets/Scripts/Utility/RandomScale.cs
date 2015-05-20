using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Transform))]

public class RandomScale: MonoBehaviour
{
	public Vector3 MinScale = new Vector3(1f, 1f, 1f);
	public Vector3 MaxScale = new Vector3(1f, 1f, 1f);
	public float MaxDiscrepency = 0f;

	// Use this for initialization
	void Start ()
	{
		Vector3 newScale = new Vector3(Random.Range(MinScale.x, MaxScale.x),
		                               Random.Range(MinScale.y, MaxScale.y),
		                               Random.Range(MinScale.z, MaxScale.z));

		float largestAxis = Mathf.Max(newScale.x, newScale.y, newScale.z);
		float minAxis = largestAxis * MaxDiscrepency;
		newScale.x = Mathf.Max(minAxis, newScale.x);
		newScale.y = Mathf.Max(minAxis, newScale.y);
		newScale.z = Mathf.Max(minAxis, newScale.z);

		GetComponent<Transform>().localScale = newScale;
	}
}
