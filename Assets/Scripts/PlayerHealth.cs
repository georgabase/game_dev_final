using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	
	private GameObject Player;
	private GameObject greenFlag;
	private GameObject greenHome;

	[System.Serializable]
	public class PlayerStats
	{
		public int Health = 3;
	}

	public PlayerStats playerStats = new PlayerStats ();

	public int fallBoundary = -20;


	void Start ()
	{
		Player = GameObject.FindWithTag ("Player");
		greenFlag = GameObject.FindWithTag ("greenFlag");
		greenHome = GameObject.FindWithTag ("greenHome");
	}

	void Update ()
	{
		
		if (transform.position.y <= fallBoundary) {
			greenFlag.transform.position = greenHome.transform.position;
			greenFlag.transform.parent = null; 
			DamagePlayer (9999999);
			Debug.Log ("PINEAPPLE");
			Player.GetComponent<PlatformerCharacter2D> ().hasFlag = false;
		}
	}

	public void DamagePlayer (int damage)
	{
		playerStats.Health -= damage;
		if (playerStats.Health <= 0) {
			GameMaster.KillPlayer (this);
		}
	}

}
