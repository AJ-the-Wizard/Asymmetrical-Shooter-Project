using UnityEngine;
using System.Collections;

public class Disable_OnGameOver : MonoBehaviour
{
	private LevelController levelController;

	// Use this for initialization
	void Start ()
	{
		levelController = GameObject.FindWithTag("GameController").GetComponent<LevelController>();
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		if(levelController.IsGameOver())
		{
			gameObject.SetActive(false);
		}
	}
}
