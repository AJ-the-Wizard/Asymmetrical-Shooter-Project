using UnityEngine;
using System.Collections;

public class Deco_VisibleWhenCastable : MonoBehaviour
{
	public MagicSpell Spell;
	protected bool LastCastable = true;
	
	void LateUpdate()
	{
		if(Spell.Castable != LastCastable)
		{
			LastCastable = Spell.Castable;

			MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();

			foreach(MeshRenderer mesh in meshes)
			{
				mesh.enabled = LastCastable;
			}
		}
	}
}
