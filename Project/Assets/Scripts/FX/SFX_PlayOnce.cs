using UnityEngine;
using System.Collections;
[RequireComponent (typeof(AudioSource))]

public class SFX_PlayOnce : MonoBehaviour
{
	private AudioSource Audio;
	private bool SoundStarted;
	
	void Awake()
	{
		Audio = GetComponent<AudioSource>();
	}
	
	void LateUpdate()
	{
		if(SoundStarted == false)
		{
			if(Audio.isPlaying)
			{
				SoundStarted = true;
			}
		}
		else
		{
			if(Audio.isPlaying == false)
			{
				DestroyObject(this.gameObject);
			}
		}
	}
}
