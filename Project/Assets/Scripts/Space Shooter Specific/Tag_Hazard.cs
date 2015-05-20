using UnityEngine;
using System.Collections;

public class Tag_Hazard : MonoBehaviour
{
	LevelController levelController;
	
	// Use this for initialization
	void Start ()
	{
		levelController = GameObject.FindWithTag("GameController").GetComponent<LevelController>();
		
		levelController.HazardSpawned();
	}
	
	// Update is called once per frame
	void OnDestroy ()
	{
		levelController.HazardDestroyed();
	}
}
