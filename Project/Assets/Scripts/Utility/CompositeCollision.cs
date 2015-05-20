using UnityEngine;
using System.Collections;

public class CompositeCollision : MonoBehaviour
{
	public bool EnableOnAwake = false;
	public bool AutomaticSetup = true;
	public Collider[] CustomSetup;

	// Use this for initialization
	void Awake ()
	{
		Collider[] objectColliders;

		if(AutomaticSetup)
		{
			objectColliders = GetComponentsInChildren<Collider>();
		}
		else
		{
			objectColliders = CustomSetup;
		}

		bool[] active = new bool[objectColliders.Length];

		for(int i = 0; i < objectColliders.Length; ++i)
		{
			if(objectColliders[i] != null)
			{
				active[i] = EnableOnAwake || objectColliders[i].enabled;
				objectColliders[i].enabled = true;
			}
		}

		for(int i = 0; i < objectColliders.Length; ++i)
		{
			for(int j = i + 1; j < objectColliders.Length; ++j)
			{
				if((objectColliders[i] != null)
				   && (objectColliders[j] != null))
				{
					Physics.IgnoreCollision(objectColliders[i],
					                        objectColliders[j]);
				}
			}

			if(objectColliders[i] != null)
			{
				objectColliders[i].enabled = active[i];
			}
		}
	}
}
