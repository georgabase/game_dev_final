using UnityEngine;


public class FiringScript2 : MonoBehaviour
{
	
	public Transform shotPrefabLeft;
	public Transform shotPrefabRight;
	public Transform firingPoint;
	private Transform shotTransform;
	public float shootingRate = 1f;
	private float shootCooldown;
	private GameObject Player2;

	void Start ()
	{
		shootCooldown = 0f;
		Player2 = GameObject.FindWithTag ("Player2");
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
			if (Player2.GetComponent<PlatformerCharacter2DTWO> ().m_FacingRight2) {
				// Create a new shot
				shotTransform = Instantiate (shotPrefabRight) as Transform;
			} else if (!Player2.GetComponent<PlatformerCharacter2DTWO> ().m_FacingRight2) {
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
