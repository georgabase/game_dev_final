using System;
using UnityEngine;

//namespace UnityStandardAssets._2D
//{
public class PlatformerCharacter2DTWO : MonoBehaviour
{
	[SerializeField] private float m_MaxSpeed2 = 10f;
	// The fastest the player can travel in the x axis.
	[SerializeField] private float m_JumpForce2 = 400f;
	// Amount of force added when the player jumps.
	[Range (0, 1)] [SerializeField] private float m_CrouchSpeed2 = .36f;
	// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[SerializeField] private bool m_AirControl2 = false;
	// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround2;
	// A mask determining what is ground to the character

	private Transform m_GroundCheck2;
	// A position marking where to check if the player is grounded.
	const float k_GroundedRadius2 = .2f;
	// Radius of the overlap circle to determine if grounded
	private bool m_Grounded2;
	// Whether or not the player is grounded.
	private Transform m_CeilingCheck2;
	// A position marking where to check for ceilings
	const float k_CeilingRadius2 = .01f;
	// Radius of the overlap circle to determine if the player can stand up
	private Animator m_Anim2;
	// Reference to the player's animator component.
	private Rigidbody2D m_Rigidbody2D2;
	public bool m_FacingRight2 = true;
	// For determining which way the player is currently facing.
	public bool finish = false;
	public bool hasFlag2 = false;
	private GameObject Player;
	private GameObject Player2;
	private GameObject GameMaster;
	private GameObject orangeFlag;
	private GameObject orangeHome;
	private GameObject greenFlag;
	private GameObject greenHome;




	private void Awake ()
	{
		// Setting up references.
		m_GroundCheck2 = transform.Find ("GroundCheck");
		m_CeilingCheck2 = transform.Find ("CeilingCheck");
		m_Anim2 = GetComponent<Animator> ();
		m_Rigidbody2D2 = GetComponent<Rigidbody2D> ();

		Player = GameObject.FindWithTag ("Player");
		Player2 = GameObject.FindWithTag ("Player2");
		GameMaster = GameObject.FindGameObjectWithTag ("GM");

		//CTF
		orangeFlag = GameObject.FindWithTag ("orangeFlag");
		orangeHome = GameObject.FindWithTag ("orangeHome");
		greenFlag = GameObject.FindWithTag ("greenFlag");
		greenHome = GameObject.FindWithTag ("greenHome");

	}


	private void FixedUpdate ()
	{
		m_Grounded2 = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll (m_GroundCheck2.position, k_GroundedRadius2, m_WhatIsGround2);
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders [i].gameObject != gameObject)
				m_Grounded2 = true;
		}
		m_Anim2.SetBool ("Ground", m_Grounded2);

		// Set the vertical animation
		m_Anim2.SetFloat ("vSpeed", m_Rigidbody2D2.velocity.y);
	}



	public void Move (float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch && m_Anim2.GetBool ("Crouch")) {
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle (m_CeilingCheck2.position, k_CeilingRadius2, m_WhatIsGround2)) {
				crouch = true;
			}
		}

		// Set whether or not the character is crouching in the animator
		m_Anim2.SetBool ("Crouch", crouch);

		//only control the player if grounded or airControl is turned on
		if (m_Grounded2 || m_AirControl2) {
			// Reduce the speed if crouching by the crouchSpeed multiplier
			move = (crouch ? move * m_CrouchSpeed2 : move);

			// The Speed animator parameter is set to the absolute value of the horizontal input.
			m_Anim2.SetFloat ("Speed", Mathf.Abs (move));

			// Move the character
			m_Rigidbody2D2.velocity = new Vector2 (move * m_MaxSpeed2, m_Rigidbody2D2.velocity.y);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight2) {
				// ... flip the player.
				Flip ();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight2) {
				// ... flip the player.
				Flip ();
			}
		}
		// If the player should jump...
		if (m_Grounded2 && jump && m_Anim2.GetBool ("Ground")) {
			// Add a vertical force to the player.
			m_Grounded2 = false;
			m_Anim2.SetBool ("Ground", false);
			m_Rigidbody2D2.AddForce (new Vector2 (0f, m_JumpForce2));
		}
	}


	private void Flip ()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight2 = !m_FacingRight2;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerEnter2D (Collider2D otherCollider)
	{

		if (otherCollider.CompareTag ("enemyShot")) {
			Player2.GetComponent<Player2Health> ().DamagePlayer2 (25);
		}


		if (otherCollider.CompareTag ("medPack")) {
			Player2.GetComponent<Player2Health> ().player2Stats.Health += 25;
		}

		if (otherCollider.CompareTag ("fin")) {
			Debug.Log ("quit");
			finish = true;

		}

		if (otherCollider.CompareTag ("orangeFlag")) {
			hasFlag2 = true;
			orangeFlag.GetComponent<orangeFlag> ().of_Rigidbody2D.transform.parent = m_Rigidbody2D2.transform;
		}

		if (otherCollider.CompareTag ("greenFlag")) {
			greenFlag.transform.position = greenHome.transform.position;
			greenFlag.transform.parent = null;
			Player.GetComponent<PlatformerCharacter2D> ().hasFlag = false;
		}



		if (otherCollider.CompareTag ("greenPlatform")) {
			if (hasFlag2) {
				orangeFlag.transform.position = orangeHome.transform.position;
				orangeFlag.transform.parent = null;
				GameMaster.GetComponent<GameMaster> ().score2 += 500;
				hasFlag2 = false;
			}
		}



	}

}
