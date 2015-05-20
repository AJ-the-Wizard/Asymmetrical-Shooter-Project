using UnityEngine;
using System.Collections;

public class MagicSpell_BasicShot : MagicSpell
{
	public GameObject MagicShot;
	public bool TriggerExplosion;
	public bool TriggerOnSecondCast = false;

	protected GameObject CurrentMagicShot;
	protected Transform CurrentCastingLocation;

	protected void Awake()
	{
		base.Awake();
	}

	override public int CastSpell(Transform castingLocation, GameObject caster)
	{
		if(TriggerOnSecondCast)
		{
			if(ExplodeSpell())
			{
				return 0;
			}
		}

		CurrentMagicShot = Instantiate(MagicShot,
		                              castingLocation.position,
		                              castingLocation.rotation) as GameObject;

		CurrentMagicShot.GetComponent<ObjectEvents>().Owner = caster;
		CurrentMagicShot.GetComponent<ObjectEvents>().PreventParentCollision();

		StartContinuousFire(castingLocation, caster);

		return MPCost;
	}

	override public int EndSpell(Transform castingLocation, GameObject caster)
	{
		ExplodeSpell();
		EndContinuousFire(caster);

		return 0;
	}

	protected bool ExplodeSpell()
	{
		if((CurrentMagicShot != null)
		   && TriggerExplosion)
		{
			CurrentMagicShot.GetComponent<ObjectEvents>().Death(ObjectEvents.DeathType.Script,
			                                                    null);
			DestroyObject(CurrentMagicShot);
            CurrentMagicShot = null;

			return true;
		}

		return false;
	}
}
