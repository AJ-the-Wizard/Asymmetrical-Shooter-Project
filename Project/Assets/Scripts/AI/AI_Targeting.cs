using UnityEngine;
using System.Collections;

public class AI_Targeting : MonoBehaviour
{
	public enum QueuingType {InOrder, CurrentClosest};
	public enum TargetingType {Hostile, Friendly, Traveling};
	public enum TargetingRange {Following = 0, Firing = 1, Targeting = 2, Chase = 3, OutOfRange = 4};

	public GameObject[] TargetQueue;
	public TargetingType[] TargetQueueTT;
	public QueuingType QueueType;

	public string[] EnemyTags = new string[0];

	public float MaxChaseDistance = 100f;
	public float MaxTargetingDistance = 50f;
	public float MaxFiringDistance = 25f;
	public float MaxFollowingDistance = 10f;

	public int UpdateRate = 15;
	int UpdateCount = 1;
	bool UpdatedThisFrame = false;

	GameObject CurrentTarget;
	int CurrentTargetIndex;
	TargetingType CurrentTargetType;
	float CurrentTargetDistance;
	TargetingRange CurrentTargetRange;

	// Use this for initialization
	void Start ()
	{
		if(TargetQueue == null)
		{
			TargetQueue = new GameObject[0];
		}

		if(TargetQueueTT == null)
		{
			TargetQueueTT = new TargetingType[TargetQueue.Length];
		}
		else if(TargetQueueTT.Length != TargetQueue.Length)
		{
			System.Array.Resize(ref TargetQueueTT, TargetQueue.Length);
		}

		if((EnemyTags == null) || (EnemyTags.Length == 0))
		{
			EnemyTags = new string[1];
			EnemyTags[0] = "";
		}
		else if(EnemyTags[0] == null)
		{
			EnemyTags[0] = "";
		}
		else
		{
			foreach(string tag in EnemyTags)
			{
				AddTarget(GameObject.FindGameObjectsWithTag(tag), TargetingType.Hostile);
			}
		}

		MaxChaseDistance = 9999f;

		UpdateCount = Random.Range(0, UpdateRate);
	}
	
	// Update is called once per frame
	public void UpdateTarget ()
	{
		UpdateCount -= 1;

		if(UpdateCount <= 0)
		{
			UpdateCount = UpdateRate;
			UpdatedThisFrame = true;
		}
		else
		{
			UpdatedThisFrame = false;
			return;
		}


		GameObject target = null;
		float distance = Mathf.Infinity;
		int index = -1;
		
		if(QueueType == QueuingType.InOrder)
		{
			bool inQueue = false;
			
			if(CurrentTarget != null)
			{
				for(int i = 0; i < TargetQueue.Length; ++i)
				{
					if(TargetQueue[i] == CurrentTarget)
					{
						float distanceCheck = Vector3.Distance(transform.position, TargetQueue[i].transform.position);

						if(distanceCheck < MaxChaseDistance)
						{
							inQueue = true;
							target = CurrentTarget;
							index = CurrentTargetIndex;
							distance = distanceCheck;
							break;
						}

						break;
					}
				}
			}

			if(inQueue == false)
			{
				for(int i = 0; i < TargetQueue.Length; ++i)
				{
					if(TargetQueue[i] != null)
					{
						float distanceCheck = Vector3.Distance(transform.position, TargetQueue[i].transform.position);

						if((distanceCheck < MaxChaseDistance)
						   || ((TargetQueueTT[i] == TargetingType.Traveling)
						        && ((distanceCheck < distance) || (distance > MaxChaseDistance))))
						{
							target = TargetQueue[i];
							index = i;
							distance = distanceCheck;
							break;
						}
					}
				}
			}
		}
		else if(QueueType == QueuingType.CurrentClosest)
		{
			if(CurrentTarget != null)
			{
				CurrentTargetDistance = Vector3.Distance(transform.position, CurrentTarget.transform.position);
			}
			else
			{
				CurrentTargetDistance = Mathf.Infinity;
			}

			if(CurrentTargetDistance > MaxFiringDistance)
			{
				for(int i = 0; i < TargetQueue.Length; ++i)
				{
					if(TargetQueue[i] != null)
					{
						float distanceCheck = Vector3.Distance(TargetQueue[i].transform.position,
						                                       transform.position);
						
						if(distanceCheck < distance)
						{
							target = TargetQueue[i];
							distance = distanceCheck;
							index = i;
						}
					}
				}

				if((index > -1)
				   && (TargetQueueTT[index] == TargetingType.Traveling))
				{
					// ?
				}
				else if(distance > MaxChaseDistance)
				{
					target = null;
					distance = Mathf.Infinity;
				}
			}
			else
			{
				target = CurrentTarget;
				index = CurrentTargetIndex;
				distance = CurrentTargetDistance;
			}
		}

		CurrentTarget = target;
		CurrentTargetIndex = index;
		CurrentTargetDistance = distance;

		if(index != -1)
		{
			CurrentTargetType = TargetQueueTT[index];
		}

		if(CurrentTargetDistance < MaxFollowingDistance)
		{
			CurrentTargetRange = TargetingRange.Following;
		}
		else if(CurrentTargetDistance < MaxFiringDistance)
		{
			CurrentTargetRange = TargetingRange.Firing;
		}
		else if(CurrentTargetDistance < MaxTargetingDistance)
		{
			CurrentTargetRange = TargetingRange.Targeting;
		}
		else if(CurrentTargetDistance < MaxChaseDistance)
		{
			CurrentTargetRange = TargetingRange.Chase;
		}
		else
		{
			CurrentTargetRange = TargetingRange.OutOfRange;
		}
	}

	public GameObject GetCurrentTarget()
	{
		return CurrentTarget;
	}
	
	public float GetCurrentTargetDistance()
	{
		return CurrentTargetDistance;
	}
	
	public TargetingRange GetCurrentTargetRange()
	{
		return CurrentTargetRange;
	}

	public TargetingType GetCurrentTargetingType()
	{
		return CurrentTargetType;
	}
	
	public void SetCurrentTargetingType(TargetingType value)
	{
		TargetQueueTT[CurrentTargetIndex] = value;
	}

	public Vector3 GetInterceptPoint(float shotSpeed)
	{
		return GameMath.GetInterceptPoint(this.transform.position,
		                                  shotSpeed,
		                                  CurrentTarget.transform.position,
		                                  CurrentTarget.GetComponent<Rigidbody>().velocity);
	}

	public void AddTarget(GameObject[] targets, TargetingType targeting)
	{
		foreach(GameObject target in targets)
		{
			AddTarget(target, targeting);
		}
	}
	
	public void AddTarget(GameObject target, TargetingType targeting)
	{
		for(int i = 0; i < TargetQueue.Length; ++i)
		{
			if(TargetQueue[i] == null)
			{
				TargetQueue[i] = target;
				TargetQueueTT[i] = targeting;
				return;
			}
		}
		
		System.Array.Resize(ref TargetQueue, TargetQueue.Length + 1);
		System.Array.Resize(ref TargetQueueTT, TargetQueue.Length);
		TargetQueue[TargetQueue.Length - 1] = target;
		TargetQueueTT[TargetQueue.Length - 1] = targeting;
	}

	public void ClearTarget(GameObject[] targets)
	{
		foreach(GameObject target in targets)
		{
			ClearTarget(target);
		}
	}

	public void ClearTarget(GameObject target)
	{
		for(int i = 0; i < TargetQueue.Length; ++i)
		{
			if(TargetQueue[i] == target)
			{
				TargetQueue[i] = null;
				return;
			}
		}
	}

	public void ClearAllTargets()
	{
		for(int i = 0; i < TargetQueue.Length; ++i)
		{
			TargetQueue[i] = null;
		}
	}

	public bool HasTarget(GameObject target)
	{
		foreach(GameObject targetCheck in TargetQueue)
		{
			if(targetCheck == target)
			{
				return true;
			}
		}

		return false;
	}

	public bool GetUpdatedThisFrame()
	{
		return UpdatedThisFrame;
	}
}
