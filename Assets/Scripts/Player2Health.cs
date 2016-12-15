using UnityEngine;
using System.Collections;

public class Player2Health : MonoBehaviour
{

	private GameObject Player2;
	private GameObject orangeFlag;
	private GameObject orangeHome;

	[System.Serializable]
	public class Player2Stats
	{
		public int Health = 3;
	}

	public Player2Stats player2Stats = new Player2Stats ();

	public int fallBoundary = -20;


	void Start ()
	{
		Player2 = GameObject.FindWithTag ("Player2");
		orangeFlag = GameObject.FindWithTag ("orangeFlag");
		orangeHome = GameObject.FindWithTag ("orangeHome");
	}

	void Update ()
	{
		if (transform.position.y <= fallBoundary) {
			DamagePlayer2 (9999999);
			orangeFlag.transform.position = orangeHome.transform.position;
			orangeFlag.transform.parent = null;
			Player2.GetComponent<PlatformerCharacter2DTWO> ().hasFlag2 = false;
		}
	}

	public void DamagePlayer2 (int damage)
	{
		player2Stats.Health -= damage;
		if (player2Stats.Health <= 0) {
			GameMaster.KillPlayer2 (this);
		}
	}

}
