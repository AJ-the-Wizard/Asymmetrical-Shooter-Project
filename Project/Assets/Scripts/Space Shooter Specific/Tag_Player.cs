using UnityEngine;
using System.Collections;

public class Tag_Player : MonoBehaviour
{
	LevelController levelController;

	// Use this for initialization
	void Start ()
	{
		levelController = GameObject.FindWithTag("GameController").GetComponent<LevelController>();
	}

	void OnDestroy ()
	{
		levelController.SetGameOver();
	}
}
