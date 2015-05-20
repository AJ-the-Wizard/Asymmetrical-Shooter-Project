using UnityEngine;
using System.Collections;

public class ScaleParticles : MonoBehaviour
{
	public float DefaultScale = 1f;
	public float DefaultRelativeSpeed = 1f;
	public ParticleSystem[] ParticleSystems;
	float[] BaseScales;
	float[] BaseSpeeds;
	
	// Use this for initialization
	void Start ()
	{
		BaseScales = new float[ParticleSystems.Length];
		BaseSpeeds = new float[ParticleSystems.Length];

		for(int i = 0; i < ParticleSystems.Length; ++i)
		{
			BaseScales[i] = ParticleSystems[i].startSize;
			BaseSpeeds[i] = ParticleSystems[i].startSpeed;
		}

		SetScale(DefaultScale, DefaultRelativeSpeed);
	}

	public void SetScale(float scale, float relativeSpeed)
	{
		for(int i = 0; i < ParticleSystems.Length; ++i)
		{
			ParticleSystems[i].startSize = BaseScales[i] * scale;
			ParticleSystems[i].startSpeed = BaseScales[i] * BaseSpeeds[i] * relativeSpeed;
		}
	}
}
