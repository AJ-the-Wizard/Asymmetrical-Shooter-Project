using UnityEngine;
using System.Collections;

public class Collectible_UnlockSpell : Collectible
{
	public GameObject SpellOwner;
	public MagicSpell Spell;
	
	void Awake()
	{
		base.Awake();
	}
	
	public override void Collect(GameObject collector)
	{
		if(collector == SpellOwner)
		{
			Spell.Castable = true;
			collector.GetComponent<MagicController>().RecheckSpells();

			base.Collect(collector);
		}
	}
}
