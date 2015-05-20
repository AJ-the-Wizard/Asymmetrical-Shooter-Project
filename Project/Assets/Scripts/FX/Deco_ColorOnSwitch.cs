using UnityEngine;
using System.Collections;
[RequireComponent (typeof(MeshRenderer))]

public class Deco_ColorOnSwitch : MonoBehaviour
{
	protected MeshRenderer mesh;

	public Switch ParentSwitch;
	public Material InactiveColor;
	public Material ActiveColor;

	protected bool LastActive;

	void Awake()
	{
		mesh = GetComponent<MeshRenderer>();
	}

	void Start()
	{
		if(ParentSwitch.GetActive())
		{
			if(ActiveColor == null)
			{
				ActiveColor = mesh.material;
			}
		}
		else
		{
			if(InactiveColor == null)
			{
				InactiveColor = mesh.material;
			}
		}

		LastActive = ParentSwitch.GetActive();
	}
	
	void FixedUpdate()
	{
		if(ParentSwitch.GetActive() != LastActive)
		{
			LastActive = ParentSwitch.GetActive();

			if(LastActive)
			{
				mesh.material = ActiveColor;
			}
			else
			{
				mesh.material = InactiveColor;
			}
		}
	}
}
