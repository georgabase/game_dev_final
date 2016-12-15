using UnityEngine;

/// <summary>
/// Simply moves the current game object
/// </summary>
public class MoveScript : MonoBehaviour
{
	// 1 - Designer variables

	/// <summary>
	/// Object speed
	/// </summary>
	public Vector2 speed = new Vector2(10, 10);

	/// <summary>
	/// Moving direction
	/// </summary>
	public Vector2 direction;// = new Vector2(-1, 0);
	 


	private Vector2 movement;
	private Rigidbody2D rigidbodyComponent;
	private bool Facing;
	private bool canShoot;
	private bool FacingAlt;


	void Start() {
		GameObject Player = GameObject.Find("Player");
		PlatformerCharacter2D playerScript = Player.GetComponent<PlatformerCharacter2D>();
		FiringScript playerScript1 = Player.GetComponent<FiringScript>();
		Facing = playerScript.m_FacingRight;
		canShoot = playerScript1.CanAttack;
	}



	void Update()
	{
		// 2 - Movement

		if (canShoot) {
			if (Facing) {
				FacingAlt = true;
			} if (!Facing) {
				FacingAlt = false;
			}
		}

		// If the player is facing left...
		if (FacingAlt)
		{
			direction = new Vector2(1,0);
			movement = new Vector2(
				speed.x * direction.x,
				speed.y * direction.y);
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (!FacingAlt)
		{
			direction = new Vector2(-1,0);
			movement = new Vector2(
				speed.x * direction.x,
				speed.y * direction.y);
		}


	}

	void FixedUpdate()
	{
		if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

		// Apply movement to the rigidbody
		rigidbodyComponent.velocity = movement;
	}
}