using UnityEngine;
using System.Collections;

public class MenuSelection_LookAround : MenuSelection
{
	public MeshRenderer FadeFX;
	public float FadeDuration;
	public GameObject Player;
	public Vector3 MoveToPosition;
	public Vector3 MoveToRotation;

	protected bool FadingOut;

	public override void Activate()
	{
		Player.transform.position = MoveToPosition;
		Player.transform.rotation = Quaternion.Euler(MoveToRotation);
		FadingOut = true;
	}

	public void FixedUpdate()
	{
		if(FadingOut)
		{
			float alpha = FadeFX.material.color.a;
			alpha = Mathf.Max(0f, alpha - (Time.fixedDeltaTime * (1f / FadeDuration)));
			FadeFX.material.color = new Color(FadeFX.material.color.r,
			                                  FadeFX.material.color.g,
			                                  FadeFX.material.color.b,
			                                  alpha);

			if(alpha <= 0f)
			{
				FadingOut = false;
				Debug.Log("Finished");
			}
		}
	}
}
