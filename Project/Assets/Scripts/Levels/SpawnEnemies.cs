// Not spawning relative to spawner rotation!

using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Transform))]

public class SpawnEnemies : MonoBehaviour
{
	public delegate void ObjectSpawned(GameObject spawnedObject);
	public event ObjectSpawned ObjectSpawnedListeners;

	Transform transform;

	LevelController levelController;

	public Vector3 SpawnPosMin;
	public Vector3 SpawnPosMax;
	public Vector3 SpawnRotationMin;
	public Vector3 SpawnRotationMax;
	public Vector3 SpawnVelocityMin;
	public Vector3 SpawnVelocityMax;

	public Vector2 SpawnInterval = new Vector2(1.0f, 1.0f);
	public Vector2 SpawnCount = new Vector2(1, 1);
	public float FirstSpawnDelay;
	public Vector2 InitialSpawns = new Vector2(0, 0);

	public GameObject SpawnObject;
	public int MaxObjectCount = 10;
	GameObject[] SpawnedObjects;

	public bool Enabled = true;

	// Use this for initialization
	void Start ()
	{
		transform = GetComponent<Transform>();

		SpawnedObjects = new GameObject[MaxObjectCount];

		int spawnCount = (int) Mathf.Round(Random.Range(InitialSpawns.x, InitialSpawns.y));
		for(int i = 0; i < spawnCount; ++i)
		{
			SpawnEnemy();
		}

		StartCoroutine(SpawnWave(FirstSpawnDelay));

		levelController = GameObject.FindWithTag("GameController").GetComponent<LevelController>();
	}

	void FixedUpdate()
	{
		if(levelController.IsGameOver())
		{
			Enabled = false;
		}
	}

	// Update is called once per frame
	IEnumerator SpawnWave (float delay)
	{
		yield return new WaitForSeconds(delay);

		while(Enabled)
		{
			int spawnCount = (int) Mathf.Round(Random.Range(SpawnCount.x, SpawnCount.y));
			for(int i = 0; i < spawnCount; ++i)
			{
				SpawnEnemy();
			}
			yield return new WaitForSeconds(Random.Range(SpawnInterval.x, SpawnInterval.y));
		}
	}

	void SpawnEnemy ()
	{
		int index = FirstFreeIndex();

		if(index == -1)
		{
			return;
		}

		Vector3 position = new Vector3(Random.Range(SpawnPosMin.x, SpawnPosMax.x) + transform.position.x,
		                               Random.Range(SpawnPosMin.y, SpawnPosMax.y) + transform.position.y,
		                               Random.Range(SpawnPosMin.z, SpawnPosMax.z) + transform.position.z);

		Vector3 rotation = new Vector3(Random.Range(SpawnRotationMin.x, SpawnRotationMax.x),
			                           Random.Range(SpawnRotationMin.y, SpawnRotationMax.y),
			                           Random.Range(SpawnRotationMin.z, SpawnRotationMax.z));
		Quaternion rotationQuat = Quaternion.Euler(rotation);

		Vector3 velocity = new Vector3(Random.Range(SpawnVelocityMin.x, SpawnVelocityMax.x),
		                               Random.Range(SpawnVelocityMin.y, SpawnVelocityMax.y),
		                               Random.Range(SpawnVelocityMin.z, SpawnVelocityMax.z));


		GameObject enemy = Instantiate(SpawnObject, position, rotationQuat) as GameObject;
		enemy.GetComponent<Rigidbody>().velocity = velocity;

		SpawnedObjects[index] = enemy;

		if(ObjectSpawnedListeners != null)
		{
			ObjectSpawnedListeners(enemy);
		}
	}

	int FirstFreeIndex ()
	{
		for(int i = 0; i < SpawnedObjects.Length; ++i)
		{
			if(SpawnedObjects[i] == null)
			{
				return i;
			}
		}

		return -1;
	}
}
