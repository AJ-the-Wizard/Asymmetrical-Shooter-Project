using UnityEngine;
using System.Collections;

public class Tag_Objective : MonoBehaviour
{
	LevelController levelController;

	public bool ImmediateLockOn = false;

	// Use this for initialization
	void Start ()
	{
		levelController = GameObject.FindWithTag("GameController").GetComponent<LevelController>();
		
		levelController.ObjectiveSpawned(this.gameObject);

		if(ImmediateLockOn)
		{
			GameObject.FindWithTag("GameController").GetComponent<LevelController>().CreateLockOn(this.gameObject);
		}
	}
	
	// Update is called once per frame
	void OnDestroy ()
	{
		levelController.ObjectiveDestroyed();
	}
}
