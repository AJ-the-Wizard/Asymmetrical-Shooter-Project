using UnityEngine;
using System.Collections;
[RequireComponent (typeof(ObjectEvents))]

public class Magic_Fetch : MonoBehaviour
{
	private ObjectEvents objectEvents;
	
	void Awake()
	{
		objectEvents = GetComponent<ObjectEvents>();
	}

	void OnTriggerEnter(Collider other)
	{
		CheckForCollectible(other.gameObject);
	}

	void OnCollisionEnter(Collision other)
	{
		CheckForCollectible(other.gameObject);
	}

	void CheckForCollectible(GameObject other)
	{
		if(other != null)
		{
			if(other.GetComponent<Collectible>() != null)
			{
				other.GetComponent<Collectible>().Collect(objectEvents.Owner);
			}
		}
	}
}
