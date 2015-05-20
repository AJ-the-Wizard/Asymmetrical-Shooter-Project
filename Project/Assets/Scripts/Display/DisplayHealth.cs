using UnityEngine;
using System.Collections;
[RequireComponent (typeof (GUIText))]

public class DisplayHealth : MonoBehaviour
{
	GUIText Text;

	public string BaseText;
	public GameObject Owner;
	public Vector2 ScreenOffset;

	private Color BaseColor;
	public Color FlashColor;
	public float FlashDuration = 0.75f;
	public float FlashInterval = 0.2f;
	private float FlashTimer = 0f;
	private float FlashColorTimer = 0f;
	private int LastHealth = 0;
	
	// Use this for initialization
	void Start ()
	{
		Text = GetComponent<GUIText>();

		if(BaseText == "")
		{
			BaseText = Text.text;
		}
		
		if(Owner == null)
		{
			Owner = GameObject.FindWithTag("GameController");
		}
	
		BaseColor = Text.color;
		LastHealth = Owner.GetComponent<Health>().CurHealth;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if((Owner) == false)
		{
			Text.text = "";
			return;
		}

		int health = Owner.GetComponent<Health>().CurHealth;

		if(health < LastHealth)
		{
			FlashTimer = FlashDuration;
			FlashColorTimer = 0f;
		}
		LastHealth = health;

		string text = "";
		for(int i = 0; i < Owner.GetComponent<Health>().CurHealth; )
		{
			if(i != 0)
			{
				text = string.Concat(text, " ");
			}
			
			text = string.Concat(text, "<>");
			i += 1;
		}
		Text.text = text;
		
		if(FlashTimer > 0f)
		{
			FlashTimer -= Time.fixedDeltaTime;
			FlashColorTimer += Time.fixedDeltaTime;

			if(FlashColorTimer <= (FlashInterval * 0.5f))
			{
				Text.color = FlashColor;
			}
			else if(FlashColorTimer <= FlashInterval)
			{
				Text.color = BaseColor;
			}
			else
			{
				Text.color = FlashColor;
				FlashColorTimer -= FlashInterval * 2;
			}

			if(FlashTimer <= 0f)
			{
				Text.color = BaseColor;
			}
		}
	}

	Vector2 GetScreenPosition (GameObject Target)
	{
		if(Target == null)
		{
			return new Vector2(0f, 0f);
		}

		float ScreenWidth = 600f / 20f;
		float ScreenHeight = 600f / 20;
		float CameraX = 0.0f;
		float CameraY = 0.0f;
		Vector2 ScreenPosition = new Vector2();

		ScreenPosition.x = (Target.GetComponent<Transform>().position.x - CameraX);
		ScreenPosition.y = (Target.GetComponent<Transform>().position.z - CameraY);

		ScreenPosition.x *= ScreenWidth;
		ScreenPosition.y *= ScreenHeight;

		ScreenPosition.x += (ScreenWidth * 0.5f * 20f) + ScreenOffset.x;
		ScreenPosition.y += (ScreenHeight * 0.5f * 20f) + ScreenOffset.y;

		return ScreenPosition;
	}
}
