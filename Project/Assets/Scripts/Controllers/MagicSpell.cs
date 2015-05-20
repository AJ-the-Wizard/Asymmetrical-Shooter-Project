using UnityEngine;
using System.Collections;

public class MagicSpell : MonoBehaviour
{
	public string Name;
	public Color SpellColor;
	public string[] Keywords;
	public bool Castable = true;
	
	public int MPCost;
	public bool Continuous;
	public float ContinuousFireRate;
	protected GameObject[] ContinuousCasters;

	// Use this for initialization
	protected void Awake ()
	{
		ContinuousCasters = new GameObject[0];
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public virtual int CastSpell(Transform castingLocation, GameObject caster)
	{
		return 0;
	}

	public virtual int EndSpell(Transform castingLocation, GameObject caster)
	{
		return 0;
	}

	protected void StartContinuousFire(Transform castingLocation, GameObject caster)
	{
		if(Continuous
		   && (ContinuousFireRate > 0f))
		{
			System.Array.Resize(ref ContinuousCasters, ContinuousCasters.Length + 1);
			ContinuousCasters[ContinuousCasters.Length - 1] = caster;
			StartCoroutine(ContinuousFire(castingLocation, caster));
		}
	}

	protected void EndContinuousFire(GameObject caster)
	{
		for(int i = 0; i < ContinuousCasters.Length; ++i)
		{
			if(ContinuousCasters[i] == caster)
			{
				for(int j = i; (j + 1) < ContinuousCasters.Length; ++j)
				{
					ContinuousCasters[j] = ContinuousCasters[j + 1];
				}

				System.Array.Resize(ref ContinuousCasters, ContinuousCasters.Length - 1);
			}
		}
	}

	IEnumerator ContinuousFire(Transform castingLocation, GameObject caster)
	{
		bool casting = true;
		float castRate = 1f / ContinuousFireRate;

		while(casting)
		{
			casting = false;
			foreach(GameObject cast in ContinuousCasters)
			{
				if(cast == caster)
				{
					casting = true;
					break;
				}
			}

			if(casting)
			{
				CastSpell(castingLocation, caster);
			}

			yield return new WaitForSeconds(castRate);
		}
	}
}
