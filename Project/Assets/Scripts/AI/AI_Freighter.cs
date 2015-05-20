using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Transform))]
[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(AI_EnemyFighter))]
[RequireComponent (typeof (AI_Targeting))]
[RequireComponent (typeof(AI_TakeOff))]
[RequireComponent (typeof(AI_WarpSequence))]
[RequireComponent (typeof(ObjectEvents))]

public class AI_Freighter : MonoBehaviour
{
	Transform transform;
	//Rigidbody rigidBody;
	AI_EnemyFighter fighterAI;
	AI_Targeting targeting;
	AI_TakeOff takeOffAI;
	AI_WarpSequence warpAI;
    
	public GameObject TargetThreat;
	public GameObject EscapePoint;
	public float FleeDistance = 60f;
	private bool Escaping = false;
	private bool Warping = false;

	// Use this for initialization
	void Start ()
	{
		transform = GetComponent<Transform>();
		//rigidBody = GetComponent<Rigidbody>();
		fighterAI = GetComponent<AI_EnemyFighter>();
		targeting = GetComponent<AI_Targeting>();
		takeOffAI = GetComponent<AI_TakeOff>();
		warpAI = GetComponent<AI_WarpSequence>();

		GetComponent<ObjectEvents>().OnDeathListeners += OnDeath;

		fighterAI.MovementEnabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(Escaping == false)
		{
			if(Vector3.Distance(TargetThreat.GetComponent<Transform>().position,
			                    transform.position) < FleeDistance)
			{
				Escaping = true;
				targeting.AddTarget(EscapePoint, AI_Targeting.TargetingType.Friendly);
				targeting.UpdateTarget();
                takeOffAI.TakeOff();

				GameObject.FindWithTag("GameController").GetComponent<LevelController>().WritePlayerWarning(
					"Freighter escaping!");
			}
		}
		else if(Warping == false)
		{
			if(Vector3.Distance(EscapePoint.GetComponent<Transform>().position,
			                    transform.position) < 10f)
			{
				GameObject.FindWithTag("GameController").GetComponent<LevelController>().WritePlayerWarning(
					"Freighter has escaped.");

				Warping = true;
				fighterAI.MovementEnabled = false;
				warpAI.StartWarp();
            }
		}
		else if(warpAI.IsWarping() == false)
		{
			DestroyObject(this.gameObject);
		}
	}

	void OnDeath(ObjectEvents.DeathType deathType, GameObject attacker)
	{
		GameObject.FindWithTag("GameController").GetComponent<LevelController>().WritePlayerWarning(
			"Freighter destroyed!");
    }
}
