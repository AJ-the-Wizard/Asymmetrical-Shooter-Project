using UnityEngine;
using System.Collections;

public class Deco_EnableOnFadeOut : MonoBehaviour
{
    public MeshRenderer FadeFX;
	public bool StartActive = true;
    protected bool LastActive = true;
	
	void Awake()
	{
		LastActive = StartActive;
	}
	
	void FixedUpdate()
	{
	    if(LastActive != (FadeFX.material.color.a >= 1f))
        {
            LastActive = (FadeFX.material.color.a >= 1f);

            MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
            foreach(MeshRenderer mesh in meshes)
            {
                mesh.enabled = LastActive;
            }

            GUITexture[] textures = GetComponentsInChildren<GUITexture>();
            foreach(GUITexture texture in textures)
            {
                texture.enabled = LastActive;
            }

            GUIText[] texts = GetComponentsInChildren<GUIText>();
            foreach(GUIText text in texts)
            {
                text.enabled = LastActive;
            }
        }
	}
}
