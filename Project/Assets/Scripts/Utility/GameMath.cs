using UnityEngine;
using System.Collections;

public class GameMath : MonoBehaviour
{
	static public Vector3 GetScreenPosition(GameObject target)
	{
		/*      Using my own math             */
		/*      Returns Vector2
		/* 
		GameObject camera = GameObject.FindWithTag("MainCamera");
		Vector2 temp = GetRelativeAngle(camera.transform, target.transform);

		float fieldOfView = camera.GetComponent<Camera>().fieldOfView;
		Vector2 aspect = new Vector2(Mathf.Max(1f, (float) Screen.width / Screen.height),
		                             Mathf.Max(1f, (float) Screen.height / Screen.width));
		Vector2 fullRotations = new Vector2(360f / (aspect.x / fieldOfView),
		                                    360f / (aspect.y / fieldOfView));

		Vector2 facingAngle = new Vector2();
		facingAngle.x = AsPlusMinusValue(AsPlusMinusDegrees(temp.y) / (fieldOfView * aspect.x),
		                                 fullRotations.x);
		facingAngle.y = AsPlusMinusValue(AsPlusMinusDegrees(temp.x) / (-fieldOfView * aspect.y),
		                                 fullRotations.y);

		facingAngle += new Vector2(0.5f, 0.5f);

		return facingAngle;
		*/

		
		return GameObject.FindWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint(target.transform.position);
	}

	static public Vector2 GetRelativeAngle(Transform fromTransform, Transform toTransform)
	{
		//float fieldOfView = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().fieldOfView;
		Vector2 facingAngle = new Vector2();

		Vector3 localPosition = new Vector3();
		localPosition = fromTransform.InverseTransformPoint(toTransform.position);
		
		Quaternion localRotation = Quaternion.LookRotation(localPosition);

		facingAngle.x = localRotation.eulerAngles.x;
		facingAngle.y = localRotation.eulerAngles.y;

		return facingAngle;
	}

	static public float GetAngleBetween(float radiansFrom, float radiansTo)
	{
		radiansFrom = AsPositiveAngle(radiansFrom);
		radiansTo = AsPositiveAngle(radiansTo);

		return radiansTo - radiansFrom;
	}

	static public float AsPositiveAngle(float radians)
	{
		radians = (radians % (Mathf.PI * 2f));

		while(radians < 0f)
		{
			radians = (Mathf.PI * 2f) + radians;
		}

		return radians;
	}

	static public float AsPlusMinusRadians(float radians)
	{
		return AsPlusMinusValue(radians, Mathf.PI);
	}
	
	static public float AsPlusMinusDegrees(float degrees)
	{
		return AsPlusMinusValue(degrees, 180f);
	}

	static public float AsPlusMinusValue(float value, float cap)
	{
		value = (value % (cap * 2f));
		
		if(value > cap)
		{
			value = (cap * -2f) + value;
		}
		else if(value < -cap)
		{
			value = (cap * 2f) + value;
		}
		
		return value;
	}
	
	public static Vector3 AsLocalVector(Transform transform, Vector3 vector)
	{
		Vector3 localVelocity = new Vector3();
		localVelocity += transform.right * vector.x;
		localVelocity += transform.up * vector.y;
		localVelocity += transform.forward * vector.z;
		
		return localVelocity;
	}

	public static Vector3 GetInterceptPoint(Vector3 shooterPos, float shotSpeed,
	                                        Vector3 targetPos, Vector3 targetVel,
	                                        int accuracy = 3)
	{

		float time = Vector3.Distance(shooterPos, targetPos) / shotSpeed;
		Vector3 lead = targetVel * time;

		for(int i = 0; i < accuracy; ++i)
		{
			//time = Vector3.Distance(shooterPos, targetPos + lead) / shotSpeed;
			//lead = targetVel * time;
		}

		return targetPos + (lead * 0.5f);
	}
}
