using UnityEngine;
using System.Collections;

public class Deco_FadeOnCollision : MonoBehaviour
{
	public GameObject Target;
	public MeshRenderer FadeFX;
	public float FadeDuration;
	protected bool Fading;
	protected float FadeTimer;
	
	void Awake()
	{
		
	}

	void FixedUpdate()
	{
		if(Fading)
		{
			float alpha = FadeFX.material.color.a;
			alpha = Mathf.Min(1f, alpha + ((1f / FadeDuration) * Time.fixedDeltaTime));
			Color color = new Color(FadeFX.material.color.r,
			                        FadeFX.material.color.g,
			                        FadeFX.material.color.b,
			                        alpha);
			FadeFX.material.color = color;

			if(FadeFX.material.color.a >= 1f)
			{
				Fading = false;
			}
		}
	}

	void OnCollisionEnter(Collision other)
	{
		CheckIfTarget(other.gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		CheckIfTarget(other.gameObject);
	}

	void CheckIfTarget(GameObject other)
	{
		if(other == Target)
		{
			Fading = true;
		}
	}
}
