using UnityEngine;
using System.Collections;

public class Tag_Enemy : MonoBehaviour
{
	LevelController levelController;

	// Use this for initialization
	void Start ()
	{
		levelController = GameObject.FindWithTag("GameController").GetComponent<LevelController>();

		levelController.EnemySpawned();
	}
	
	// Update is called once per frame
	void OnDestroy ()
	{
		levelController.EnemyDestroyed();
	}
}
