using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
public class FiringScript : MonoBehaviour
{
	//--------------------------------
	// 1 - Designer variables
	//--------------------------------

	/// <summary>
	/// Projectile prefab for shooting
	/// </summary>
	public Transform shotPrefabLeft;
	public Transform shotPrefabRight;
	public Transform firingPoint;
	private Transform shotTransform;


	/// <summary>
	/// Cooldown in seconds between two shots
	/// </summary>
	public float shootingRate = 1f;

	//--------------------------------
	// 2 - Cooldown
	//--------------------------------

	private float shootCooldown;

	void Start()
	{
		shootCooldown = 0f;
	}

	void Update()
	{
		if (shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}
	}

	//--------------------------------
	// 3 - Shooting from another script
	//--------------------------------

	/// <summary>
	/// Create a new projectile if possible
	/// </summary>
	public void Attack(bool isEnemy)
	{
		if (CanAttack)
		{
			shootCooldown = shootingRate;

			//playerScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlatformerCharacter2D>();
			//PlatformerCharacter2D playerScript = Player.GetComponent<PlatformerCharacter2D>();


		//	GameObject Player = GameObject.Find("Player");
		//	PlatformerCharacter2D playerScript = Player.GetComponent<PlatformerCharacter2D>();


			var playerScript = GameObject.FindWithTag ("Player").GetComponent<PlatformerCharacter2D>();

			//var shotTransform;
				if (playerScript.m_FacingRight) {
				// Create a new shot
				shotTransform = Instantiate (shotPrefabRight) as Transform;
				} else if (!playerScript.m_FacingRight) {shotTransform = Instantiate (shotPrefabLeft) as Transform;
			}
				

			// Assign position
			shotTransform.position = firingPoint.transform.position;  //transform.position;

			// The is enemy property
		//	ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
		//	if (shot != null)
		//	{
		//		shot.isEnemyShot = isEnemy;
		//	}

			// Make the weapon shot always towards it
		//	MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
	//		if (move != null)
	//		{
	//			move.direction = this.transform.right; // towards in 2D space is the right of the sprite
	//	}	
		}
	}

	/// <summary>
	/// Is the weapon ready to create a new projectile?
	/// </summary>
	public bool CanAttack
	{
		get
		{
			return shootCooldown <= 0f;
		}
	}
}
