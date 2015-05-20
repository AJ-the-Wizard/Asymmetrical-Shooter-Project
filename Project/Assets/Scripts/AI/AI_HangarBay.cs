using UnityEngine;
using System.Collections;
[RequireComponent (typeof(AI_TakeOff))]

public class AI_HangarBay : MonoBehaviour
{
	AI_TakeOff takeOffAI;

	public SpawnEnemies Spawner;
	public Vector3 TakeOffVelocity;
	public float TakeOffDuration;
	public GameObject[] BayedFighters;

	// Use this for initialization
	void Start ()
	{
		takeOffAI = GetComponent<AI_TakeOff>();

		Spawner.ObjectSpawnedListeners += OnObjectSpawned;
	}
	
	void OnObjectSpawned(GameObject objectSpawned)
	{
		int index = FirstFreeFighter();

		if(index != -1)
		{
			Rigidbody bayFighter = BayedFighters[index].GetComponent<Rigidbody>();
			Rigidbody newFighter = objectSpawned.GetComponent<Rigidbody>();

			newFighter.transform.position = bayFighter.transform.position;
			newFighter.transform.rotation = bayFighter.transform.rotation;

			DestroyObject(bayFighter.gameObject);

			takeOffAI.TakeOff(objectSpawned);
		}
		else
		{
			DestroyObject(objectSpawned);
			Spawner.Enabled = false;
		}
	}

	int FirstFreeFighter()
	{
		for(int i = 0; i < BayedFighters.Length; ++i)
		{
			if(BayedFighters[i] != null)
			{
				return i;
			}
		}

		return -1;
	}
}
