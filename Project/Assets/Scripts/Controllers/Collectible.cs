using UnityEngine;
using System.Collections;
[RequireComponent (typeof(ObjectEvents))]

public class Collectible : MonoBehaviour
{
	protected ObjectEvents objectEvents;

	public bool CollectOnCollision;

	public GameObject SFXPrefab;
	public AudioClip CollectSound;

	public void Awake()
	{
		objectEvents = GetComponent<ObjectEvents>();
	}

	void OnTriggerEnter(Collider other)
	{
		if(CollectOnCollision)
		{
			Collect(other.gameObject);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if(CollectOnCollision)
		{
			Collect(other.gameObject);
		}
	}

	public virtual void Collect(GameObject collector)
	{
		if(CollectSound != null)
		{
			PlaySound(CollectSound);
		}

		objectEvents.Death(ObjectEvents.DeathType.Script, collector);
	}

	public void PlaySound(AudioClip clip)
	{
		GameObject sfx = Instantiate(SFXPrefab,
		                             this.gameObject.transform.position,
		                             this.gameObject.transform.rotation) as GameObject;

		sfx.GetComponent<AudioSource>().clip = clip;
		sfx.GetComponent<AudioSource>().Play();
	}
}
