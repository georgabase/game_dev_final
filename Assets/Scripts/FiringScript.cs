using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
public class FiringScript : MonoBehaviour
{
	
	public Transform shotPrefabLeft;
	public Transform shotPrefabRight;
	public Transform firingPoint;
	private Transform shotTransform;
	public float shootingRate = 1f;
	private float shootCooldown;
	private GameObject Player;

	void Start ()
	{
		shootCooldown = 0f;
		Player = GameObject.FindWithTag ("Player");
	}

	void Update ()
	{
		if (shootCooldown > 0) {
			shootCooldown -= Time.deltaTime;
		}
	}


	public void Attack (bool isEnemy)
	{
		if (CanAttack) {
			shootCooldown = shootingRate;
			if (Player.GetComponent<PlatformerCharacter2D> ().m_FacingRight) {
				// Create a new shot
				shotTransform = Instantiate (shotPrefabRight) as Transform;
			} else if (!Player.GetComponent<PlatformerCharacter2D> ().m_FacingRight) {
				shotTransform = Instantiate (shotPrefabLeft) as Transform;
			}
				
			// Assign position
			shotTransform.position = firingPoint.transform.position;  
		}
	}


	public bool CanAttack {
		get {
			return shootCooldown <= 0f;
		}
	}
}
