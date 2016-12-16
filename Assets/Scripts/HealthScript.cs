 using UnityEngine;

public class HealthScript : MonoBehaviour
{
	
	public int hp = 1;

	public bool isEnemy = true;


	public void Damage(int damageCount)
	{
		hp -= damageCount;

		if (hp <= 0)
		{
			// Dead!
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
		if (shot != null)
		{
			// Avoid friendly fire
			if (shot.isEnemyShot != isEnemy)  
			{ 
				if (otherCollider.CompareTag ("shot"))
				{
				Damage(shot.damage);
				var playerScript = GameObject.FindWithTag ("GM").GetComponent<GameMaster> ();
				playerScript.score += 10;
				// Destroy the shot
				Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
				} 
				else 
					if (otherCollider.CompareTag ("shot2"))
				{
						Damage(shot.damage);
					var playerScript = GameObject.FindWithTag ("GM").GetComponent<GameMaster> ();
					playerScript.score2 += 10;
					// Destroy the shot
					Destroy(shot.gameObject); 
				}
			}
		}
	}
}