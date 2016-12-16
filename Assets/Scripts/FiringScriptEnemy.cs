using UnityEngine;
using System.Collections;

public class FiringScriptEnemy : MonoBehaviour
{
		
	public Transform shotPrefabLeft;
	public Transform shotPrefabRight;
	public Transform firingPoint;
	private Transform shotTransform;
	public float shootingRate = 1f;
	private float shootCooldown;
	private GameObject Enemy;

	void Start ()
	{
		shootCooldown = 0f;
		Enemy = GameObject.FindWithTag ("Enemy");
	}

	void Update ()
	{
		if (shootCooldown > 0) {
			shootCooldown -= Time.deltaTime;
		}

		if (CanAttack) {
			shootCooldown = shootingRate;
			if (Enemy.GetComponent<EnemyMovenemt> ().facingRight) {
				// Create a new shot
				shotTransform = Instantiate (shotPrefabRight) as Transform;
			} else if (!Enemy.GetComponent<EnemyMovenemt> ().facingRight) {
				shotTransform = Instantiate (shotPrefabLeft) as Transform;
			}
			
			shotTransform.position = firingPoint.transform.position; 
		}
	}

	public bool CanAttack {
		get {
			return shootCooldown <= 0f;
		}
	}
}
