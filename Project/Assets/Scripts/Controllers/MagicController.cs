using UnityEngine;
using System.Collections;

public class MagicController : MonoBehaviour
{
	public int MaxMP;
	public int CurMP;
	public float MPRegen;
	protected float RegenCount;

	public GameObject Spellbook;
	protected MagicSpell[] Spells;
	public Transform CastingLocation;
	protected int CurrentSpellIndex = 0;

	protected int SpellsLastUpdated;

	// Use this for initialization
	void Awake ()
	{
		CurMP = MaxMP;
	}

	void Start()
	{
		RecheckSpells();
	}

	public void RecheckSpells()
	{
		MagicSpell currentSpell = null;

		if(Spells != null)
		{
			currentSpell = Spells[CurrentSpellIndex];
		}

		Spells = null;
		Spells = new MagicSpell[0];
		MagicSpell[] spellList = Spellbook.GetComponentsInChildren<MagicSpell>();

		foreach(MagicSpell spell in spellList)
		{
			if(spell.Castable)
			{
				System.Array.Resize(ref Spells, Spells.Length + 1);
				Spells[Spells.Length - 1] = spell;
			}
		}

		if(currentSpell != null)
		{
			CurrentSpellIndex = Mathf.Max(0, GetSpellIndex(currentSpell));
		}

		SpellsLastUpdated = Time.frameCount;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		RegenCount += Time.fixedDeltaTime * MPRegen;
		int regen = (int) RegenCount;
		RegenCount -= regen;

		CurMP = Mathf.Min(CurMP + regen, MaxMP);
	}

	public void CastSpell()
	{
		MagicSpell spell = Spells[CurrentSpellIndex];

		if(spell.MPCost <= CurMP)
		{
			int cost = spell.CastSpell(CastingLocation, this.gameObject);
			CurMP -= cost;
		}
	}

	public void EndSpell()
	{
		if(Spells[CurrentSpellIndex].Continuous)
		{
			Spells[CurrentSpellIndex].EndSpell(CastingLocation, this.gameObject);
		}
	}

	public MagicSpell GetCurrentSpell()
	{
		return Spells[CurrentSpellIndex];
	}

	public int GetCurrentSpellIndex()
	{
		return CurrentSpellIndex;
	}

	public MagicSpell GetSpellAtIndex(int index)
	{
		if(index < 0)
		{
			index += Spells.Length;
		}
		index %= Spells.Length;

		return Spells[index];
	}

	public int GetSpellIndex(MagicSpell spell)
	{
		for(int i = 0; i < Spells.Length; ++i)
		{
			if(Spells[i] == spell)
			{
				return i;
			}
		}

		return -1;
	}

	public void SelectCurrentSpellIndex(int value)
	{
		while(value < 0)
		{
			value += Spells.Length;
		}
		value %= Spells.Length;

		CurrentSpellIndex = value;
		SpellsLastUpdated = Time.frameCount;
	}

	public bool GetSpellsUpdated()
	{
		return ((SpellsLastUpdated + 1) >= Time.frameCount);
	}
}
