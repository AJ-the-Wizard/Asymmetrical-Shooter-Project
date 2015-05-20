using UnityEngine;
using System.Collections;

public class Level_StationStrike : MonoBehaviour
{
	LevelController levelController;

	public Transform mapCenter;

	int LastEnemyCount = 0;

	float RemindInterval = 15f;
	float RemindTimer = 0f;
	bool WinSequence = false;
	float WinTimer = 0f;

	// Use this for initialization
	void Start ()
	{
		levelController = GameObject.FindWithTag("GameController").GetComponent<LevelController>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if((levelController.Player) == false)
		{
			return;
		}

		int enemyCount = levelController.GetEnemyCount();
		
		if(enemyCount > LastEnemyCount)
		{
			levelController.WritePlayerWarning("Enemy incoming!");
		}
		
		if((LastEnemyCount > 0)
		   && (enemyCount == 0))
		{
			levelController.WritePlayerWarning("All clear.");
		}
		
		LastEnemyCount = enemyCount;

		if(levelController.GetObjectiveCount() == 0)
		{
			if(WinSequence == false)
			{
				RemindTimer -= Time.fixedDeltaTime;
				
				if(RemindTimer <= 0f)
				{
					levelController.WritePlayerWarning("Objectives complete. Exit area to return to base.");
					RemindTimer = RemindInterval;
					RemindInterval *= 1.5f;
				}

				if(Vector3.Distance(mapCenter.position,
				                    levelController.Player.GetComponent<Transform>().position) >= 150)
				{
					levelController.Player.GetComponent<AI_WarpSequence>().StartWarp();
					levelController.Player.GetComponent<PlayerController>().Enabled = false;
					GameObject.FindWithTag("MainCamera").GetComponent<Camera_LookAtTarget>().Owner = null;

					WinSequence = true;
					WinTimer = levelController.Player.GetComponent<AI_WarpSequence>().WarmUpTime
						       + levelController.Player.GetComponent<AI_WarpSequence>().ZoomOffTime
							   + 1f;
				}
			}
			else
			{
				WinTimer -= Time.fixedDeltaTime;

				if(WinTimer <= 0f)
				{
					levelController.SetGameWon();
					DestroyObject(levelController.Player);
				}
			}
		}
	}
}