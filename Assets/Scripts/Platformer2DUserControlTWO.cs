using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


	[RequireComponent(typeof (PlatformerCharacter2DTWO))]
	public class Platformer2DUserControlTWO : MonoBehaviour
	{
		private PlatformerCharacter2DTWO m_Character2;
		private bool m_Jump2;


		private void Awake()
		{
			m_Character2 = GetComponent<PlatformerCharacter2DTWO>();
		}


		private void Update()
		{
			if (!m_Jump2)
			{
				// Read the jump input in Update so button presses aren't missed.
				m_Jump2 = CrossPlatformInputManager.GetButtonDown("Jump2");
			}

			// ...

			// 5 - Shooting
			bool shoot = Input.GetButtonDown("Fire2");


			if (shoot)
			{
				FiringScript2 weapon = GetComponent<FiringScript2>();
				if (weapon != null)
				{
					// false because the player is not an enemy
					weapon.Attack(false);
				}
			}

			// ...

		}


		private void FixedUpdate()
		{
			// Read the inputs.
			bool crouch2 = Input.GetKey(KeyCode.RightControl);
			float h2 = CrossPlatformInputManager.GetAxis("Horizontal2");
			// Pass all parameters to the character control script.
			m_Character2.Move(h2, crouch2, m_Jump2);
			m_Jump2 = false;
		}
	}

