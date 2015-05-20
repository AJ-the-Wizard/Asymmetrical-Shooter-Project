using UnityEngine;
using System.Collections;
[RequireComponent (typeof(LevelController))]

public class Level_HomeAmbush : MonoBehaviour
{
	LevelController levelController;

	public AI_SFBContention Contention;
	public GameObject[] Transports;
	public GameObject[] EscapePoints;
	public Region_TargetEntered LandingStrip;

	private bool Escaping = false;
	private bool TransportsDead = false;
	private int EndingSequence = 0;
	private float RemindInterval = 15f;
	private float RemindTimer = 0f;
	private float LandingInterval = 2f;
	private float LandingTimer = 0f;
	private float EndingHoldTime = 4f;
	private float EndingTimer = 0f;
	private Rigidbody PlayerBody;

	// Use this for initialization
	void Start ()
	{
		levelController = GetComponent<LevelController>();

		if(EscapePoints == null)
		{
			EscapePoints = new GameObject[Transports.Length];
		}
		else if(EscapePoints.Length != Transports.Length)
		{
			System.Array.Resize(ref EscapePoints, Transports.Length);
		}

		PlayerBody = levelController.Player.GetComponent<Rigidbody>();

		Contention.SetInCombat(true);
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		if(Escaping == false)
		{
			if((levelController.GetObjectiveCount() < 2)
			   || (levelController.GetEnemyCount() < 4))
			{
				if(levelController.GetObjectiveCount() == 2)
				{
					levelController.WritePlayerWarning("Enemy transports are retreating.");
				}
				else if(levelController.GetObjectiveCount() == 1)
				{
					levelController.WritePlayerWarning("Remaining enemy transport is retreating.");
				}

				for(int i = 0; i < Transports.Length; ++i)
				{
					if(Transports[i] != null)
					{
						Transports[i].GetComponent<AI_Targeting>().ClearAllTargets();
						Transports[i].GetComponent<AI_Targeting>().AddTarget(EscapePoints[i], AI_Targeting.TargetingType.Friendly);
					}
				}

				Escaping = true;
			}
		}
		else if((levelController.GetObjectiveCount() > 0)
		        || (levelController.GetEnemyCount() > 0))
		{
			for(int i = 0; i < Transports.Length; ++i)
			{
				if(Transports[i] != null)
				{
					if(Transports[i].transform.position.z < -300f)
					{
						Transports[i].GetComponent<AI_WarpSequence>().StartWarp();
						Transports[i] = null;
					}
				}
			}
			
			if((TransportsDead == false)
				&& (levelController.GetObjectiveCount() == 0))
			{
				levelController.WritePlayerWarning("Transports destroyed. Enemy fighters stranded.");
				TransportsDead = true;
			}
		}
		else if(EndingSequence == 0)
		{
			if(RemindTimer <= 0f)
			{
				levelController.WritePlayerWarning("Enemies routed. You're cleared for landing.");
				RemindInterval *= 1.5f;
				RemindTimer = RemindInterval;
			}
			RemindTimer -= Time.fixedDeltaTime;

			if(Contention.GetInCombat() == true)
			{
				Contention.SetInCombat(false);
			}

			if(LandingStrip.GetTriggered())
			{
				if(PlayerBody.velocity.sqrMagnitude < 0.5f)
				{
					LandingTimer += Time.fixedDeltaTime;

					if(LandingTimer > LandingInterval)
					{
						PlayerBody.velocity = new Vector3();
						EndingSequence = 1;

						GameObject.FindWithTag("MainCamera").GetComponent<Camera_LookAtTarget>().Owner = Contention.gameObject;
						GameObject.FindWithTag("MainCamera").GetComponent<Camera_LookAtTarget>().FollowTrackOffset = new Vector3(0f, 15f, -55f);
						GameObject.FindWithTag("MainCamera").GetComponent<Camera_LookAtTarget>().FollowTrackSpeed = 50f;
					}
				}
			}
			else
			{
				LandingTimer = 0f;
			}
		}
		else if(levelController.IsGameWon() == false)
		{
			EndingTimer += Time.fixedDeltaTime;

			if(EndingSequence == 1)
			{
				if(EndingTimer > EndingHoldTime)
				{
					GameObject.FindWithTag("MainCamera").GetComponent<Camera_LookAtTarget>().Owner = null;
					Contention.GetComponent<AI_WarpSequence>().StartWarp();
					EndingSequence = 2;
					EndingTimer = 0f;
				}
			}
			else if(EndingSequence == 2)
			{
				if(EndingTimer > Contention.GetComponent<AI_WarpSequence>().WarmUpTime)
				{
					EndingSequence = 3;
					levelController.SetGameWon();
				}
			}
		}
	}
}
