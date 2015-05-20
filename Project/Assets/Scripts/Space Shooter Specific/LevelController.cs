using UnityEngine;
using System.Collections;
[RequireComponent (typeof (AudioSource))]

public class LevelController : MonoBehaviour
{
	AudioSource Music;
	private float BaseMusicVolume;

	public int Score = 0;

	private bool GameOver = false;
	private bool GameWon = false;
	private int EnemyCount = 0;
	private GameObject[] Enemies = new GameObject[0];
	private int HazardCount = 0;
	private int ObjectiveCount = 0;
	public GameObject[] Objectives = new GameObject[0];

	public GameObject Player;
	public DisplayWarnings PlayerWarnings;
	public GameObject LockOnReticule;

	// Use this for initialization
	void Start ()
	{
		Music = GetComponent<AudioSource>();
		BaseMusicVolume = Music.volume;

		if(Player == null)
		{
			Player = GameObject.FindWithTag("Player");
		}
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(GameOver
		   && (GetEnemyCount() == 0))
		{
			if(Player != null)
			{
				//Player.GetComponent<PlayerController>().DisableControls();
				Player = null;
			}
		}
	}

	public void AddScore (int Value)
	{
		Score += Value;
	}

	public bool IsGameOver()
	{
		return GameOver;
	}

	public void SetGameOver()
	{
		if(GameOver)
		{
			return;
		}

		GameOver = true;

		Music.volume = BaseMusicVolume * 0.5f;
	}
	
	public bool IsGameWon()
	{
		return GameWon;
	}
	
	public void SetGameWon()
	{
		if(GameOver)
		{
			return;
		}

		GameWon = true;
		GameOver = true;
        
        Music.volume = BaseMusicVolume * 0.75f;

		if(Player != null)
		{
			Player.GetComponent<Health>().Invincible = true;
			Player.GetComponent<PlayerController>().Enabled = false;
		}
	}
	
	public void ObjectiveSpawned(GameObject objective)
	{
		ObjectiveCount += 1;

		for(int i = 0; i < Objectives.Length; ++i)
		{
			if(Objectives[i] == null)
			{
				Objectives[i] = objective;
				return;
			}
		}

		System.Array.Resize(ref Objectives, Objectives.Length + 1);
		Objectives[Objectives.Length - 1] = objective;
	}
	
	public void ObjectiveDestroyed()
	{
		ObjectiveCount -= 1;
	}
	
	public int GetObjectiveCount()
	{
		return ObjectiveCount;
	}

	public void EnemySpawned()
	{
		EnemyCount += 1;
	}

	public void EnemyDestroyed()
	{
		EnemyCount -= 1;
	}
	
	public int GetEnemyCount()
	{
		return EnemyCount;
    }
	
	public void HazardSpawned()
	{
		HazardCount += 1;
	}
	
	public void HazardDestroyed()
	{
        HazardCount -= 1;
	}
	
	public int GetHazardCount()
	{
		return HazardCount;
    }

	public void WritePlayerWarning(string message)
	{
		PlayerWarnings.WriteMessage(message);
	}
	
	public GameObject CreateLockOn(GameObject target)
	{
		GameObject reticule = Instantiate(LockOnReticule, new Vector3(0.5f, 0.5f, 0f), new Quaternion()) as GameObject;
		
		reticule.GetComponent<DisplayTarget>().Target = target;
		
		return reticule;
	}
}
