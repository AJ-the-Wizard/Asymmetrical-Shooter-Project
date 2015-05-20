using UnityEngine;
using System.Collections;
[RequireComponent (typeof (SpaceShipController))]
[RequireComponent (typeof (HardpointHandler))]

public class PlayerController : MonoBehaviour
{
	SpaceShipController shipController;
	HardpointHandler hardpointHandler;

	public float ThrottleEaseIn = 1.0f;
	public float ThrottleEaseOut = 0.5f;
	public float GamepadThrottleEaseIn = 0.05f;
	public float GamepadThrottleEaseOut = 0.01f;

	public float TurningEaseIn = 5.0f;
	public float TurningEaseOut = 5.0f;
	
	public bool Enabled = true;

	// Use this for initialization
	void Start ()
	{
		shipController = GetComponent<SpaceShipController>();
		hardpointHandler = GetComponent<HardpointHandler>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(Enabled == false)
		{
			return;
		}

		float throttle = 0f;
		int reverseOrBoost = 0;
		Vector3 turning = new Vector3();

		//==============================================
		//= Keyboard Movement Controls

		if(Input.GetKey(PlayerProfile.ThrottleUp))
		{
			throttle += Time.fixedDeltaTime * ThrottleEaseIn;
			reverseOrBoost = 1;
		}
		if(Input.GetKey(PlayerProfile.ThrottleDown))
		{
			throttle -= Time.fixedDeltaTime * ThrottleEaseOut;
			reverseOrBoost = -1;
		}
        
		if(Input.GetKey(PlayerProfile.YawLeft))
		{
			turning.y -= 1;
		}
		if(Input.GetKey(PlayerProfile.YawRight))
		{
			turning.y += 1;
		}
		if(Input.GetKey(PlayerProfile.PivotDown))
		{
			turning.x += 1;
		}
		if(Input.GetKey(PlayerProfile.PivotUp))
		{
			turning.x -= 1;
		}
		if(Input.GetKey(PlayerProfile.BankLeft))
		{
			turning.z += 1;
		}
		if(Input.GetKey(PlayerProfile.BankRight))
		{
			turning.z -= 1;
		}
		
		//==============================================
		//= Gamepad Movement Controls

		if(Controller.GetButton(PlayerProfile.GamepadThrottleUp))
		{
			throttle += Controller.GetAxis(PlayerProfile.GamepadThrottleUp) * GamepadThrottleEaseIn;
			reverseOrBoost = 1;

			float maxBoost = Controller.GetAxis(PlayerProfile.GamepadThrottleUp)
                  			 * shipController.MaxBoostThrottle;
			throttle = Mathf.Clamp(throttle,
			                       0f,
			                       maxBoost - shipController.GetThrottle());
		}
		if(Controller.GetButton(PlayerProfile.GamepadThrottleDown))
		{
			throttle -= Controller.GetAxis(PlayerProfile.GamepadThrottleDown) * GamepadThrottleEaseOut;
			reverseOrBoost = -1;

			float maxReverse = Controller.GetAxis(PlayerProfile.GamepadThrottleDown)
				               * shipController.MaxReverseThrottle;
			throttle = Mathf.Clamp(throttle,
			                       maxReverse - shipController.GetThrottle(),
			                       0f);
		}

		if((Controller.GetAxis(PlayerProfile.GamepadYawLeft) != 0f)
		   || (Controller.GetAxis(PlayerProfile.GamepadYawRight) != 0f))
		{
			turning.y -= Controller.GetAxis(PlayerProfile.GamepadYawLeft);
			turning.y += Controller.GetAxis(PlayerProfile.GamepadYawRight);
		}

		if((Controller.GetAxis(PlayerProfile.GamepadPivotDown) != 0f)
		   || (Controller.GetAxis(PlayerProfile.GamepadPivotUp) != 0f))
		{
			turning.x -= Controller.GetAxis(PlayerProfile.GamepadPivotDown);
			turning.x += Controller.GetAxis(PlayerProfile.GamepadPivotUp);
		}
		
		if((Controller.GetAxis(PlayerProfile.GamepadBankLeft) != 0f)
		   || (Controller.GetAxis(PlayerProfile.GamepadBankRight) != 0f))
		{
			turning.z += Controller.GetAxis(PlayerProfile.GamepadBankLeft);
			turning.z -= Controller.GetAxis(PlayerProfile.GamepadBankRight);
        }
		if(Controller.GetButtonDown(PlayerProfile.GamepadLockOn))
		{
			if(LockedOn())
			{
				LockOffTarget();
			}
			else
			{
				LockOnTarget();
			}
		}

		//==============================================
		//= Firing Controls

		if(Input.GetKey(PlayerProfile.ShootMidSides)
		   || Controller.GetButton(PlayerProfile.GamepadShootMidSides))
		{
			hardpointHandler.ShootMidSides();
		}
		if(Input.GetKeyDown(PlayerProfile.ShootFarSides)
		   || Controller.GetButtonDown(PlayerProfile.GamepadShootFarSides))
		{
			hardpointHandler.ShootFarSides();
		}
		if(Input.GetKey(PlayerProfile.ShootCenter)
		   || Controller.GetButtonDown(PlayerProfile.GamepadShootCenter))
		{
			hardpointHandler.ShootCenter();
		}

		Vector3 pivotAim = new Vector3();
		if(Input.GetKey(PlayerProfile.ShootUp)
		   || Controller.GetButton(PlayerProfile.GamepadShootUp))
		{
			pivotAim += transform.up;
		}
		if(Input.GetKey(PlayerProfile.ShootLeft)
		   || Controller.GetButton(PlayerProfile.GamepadShootLeft))
		{
			pivotAim -= transform.right;
        }
		if(Input.GetKey(PlayerProfile.ShootDown)
		   || Controller.GetButton(PlayerProfile.GamepadShootDown))
		{
			pivotAim -= transform.up;
		}
		if(Input.GetKey(PlayerProfile.ShootRight) 
		   || Controller.GetButton(PlayerProfile.GamepadShootRight))
		{
			pivotAim += transform.right;
        }

		if((pivotAim.x != 0)
		   || (pivotAim.y != 0)
		   || (pivotAim.z != 0))
		{
			hardpointHandler.ShootPivot(pivotAim);
		}
		
		//==============================================
		//= Miscellaneous Controls
		
		if(Input.GetKeyDown(PlayerProfile.ToggleThrust)
		   || Controller.GetButtonDown(PlayerProfile.GamepadToggleThrust))
		{
			shipController.ToggleHighManeuvering();
		}
		
		//==============================================
		//= End Frame

		shipController.Movement(turning, throttle, reverseOrBoost);
	}

	public bool LockedOn()
	{
		Debug.Log(GameObject.FindWithTag("MainCamera").GetComponent<Camera_LookAtTarget>().Target != null);
		return (GameObject.FindWithTag("MainCamera").GetComponent<Camera_LookAtTarget>().Target != null);
	}

	private void LockOffTarget()
	{
		GameObject.FindWithTag("MainCamera").GetComponent<Camera_LookAtTarget>().Target = null;
	}

	private void LockOnTarget()
	{
		GameObject[] enemies = GameObject.FindWithTag("GameController").GetComponent<LevelController>().Objectives;

		int index = -1;
		float distance = 999f;

		float maxAngleFromCenter = 0.8f;
		float maxDistanceToLockOn = 100;

		for(int i = 0; i < enemies.Length; ++i)
		{
			Vector3 screenPosition = (Vector2) GameMath.GetScreenPosition(enemies[i]);

			if(screenPosition.z < 0f)
			{
				continue;
			}

			screenPosition.x /= Screen.width;
			screenPosition.y /= Screen.height;
			screenPosition.z = 0f;
			float distanceCheck = screenPosition.magnitude;

			Debug.Log(screenPosition);

			float angle = distanceCheck;

			if(distanceCheck < maxAngleFromCenter)
			{
				distanceCheck *= distanceCheck * Vector3.Distance(transform.position,
				                                                  enemies[i].transform.position);

				if(distanceCheck < distance)
				{
					index = i;
					distance = distanceCheck;
				}
			}

			Debug.Log(i + ": " + angle + ", " + distanceCheck);
		}

		Debug.Log(index);
		Debug.Log(distance);

		if((index != -1)
		   && (distance < maxDistanceToLockOn))
		{
			GameObject.FindWithTag("MainCamera").GetComponent<Camera_LookAtTarget>().Target = enemies[index];
		}
		else
		{
			GameObject.FindWithTag("MainCamera").GetComponent<Camera_LookAtTarget>().Target = null;
		}
	}
}
