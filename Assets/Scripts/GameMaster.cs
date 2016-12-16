using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

	public static GameMaster gm;
	public Texture2D scoreIconTexture;
	public Texture2D lIconTexture;
	public int score;

	public Texture2D scoreIconTexture2;
	public Texture2D lIconTexture2;
	public int score2;
	private bool isPause = false;



	//CTF
	private static GameObject orangeFlag;
	private static GameObject greenFlag;
	private static GameObject Player1;
	private static GameObject Player2;

	void Update () {
		if( Input.GetKeyDown(KeyCode.Escape))
		{
			isPause = !isPause;
	
		}

			}

	void OnGUI ()
	{
		DisplayScoreCount ();
		DisplayLCount ();
		DisplayScoreCount2 ();
		DisplayLCount2 ();
		DisplayRestartButton ();
		if (isPause)
			PauseMenu ();
	}

	void PauseMenu () {
		if(GUILayout.Button("Main Menu")){
			SceneManager.LoadScene (0);
				}
		if(GUILayout.Button("Restart")){
			if (SceneManager.GetActiveScene().buildIndex == 1)			SceneManager.LoadScene (1);
			if (SceneManager.GetActiveScene().buildIndex == 2)			SceneManager.LoadScene (2);

		}
		if(GUILayout.Button("Quit")){
			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#else
			Application.Quit();
			#endif
		}
	}

	void DisplayLCount ()
	{
		Rect coinIconRect = new Rect (10, 10, 32, 32);
		GUI.DrawTexture (coinIconRect, lIconTexture);                         

		GUIStyle style = new GUIStyle ();
		style.fontSize = 30;
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.yellow;

		Rect labelRect = new Rect (coinIconRect.xMax, coinIconRect.y, 60, 32);
		var playerScript = GameObject.FindWithTag ("Player").GetComponent<PlayerHealth> ();
		GUI.Label (labelRect, playerScript.playerStats.Health.ToString (), style);
	}

	void DisplayScoreCount ()
	{
		Rect coinIconRect = new Rect (10, 50, 32, 32);
		GUI.DrawTexture (coinIconRect, scoreIconTexture);                         

		GUIStyle style = new GUIStyle ();
		style.fontSize = 30;
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.yellow;

		Rect labelRect = new Rect (coinIconRect.xMax, coinIconRect.y, 60, 32);
		GUI.Label (labelRect, score.ToString (), style);
	}



	void DisplayLCount2 ()
	{
		Rect coinIconRect = new Rect (10, 320, 32, 32);
		GUI.DrawTexture (coinIconRect, lIconTexture);                         

		GUIStyle style = new GUIStyle ();
		style.fontSize = 30;
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.yellow;

		Rect labelRect = new Rect (coinIconRect.xMax, coinIconRect.y, 60, 32);
		var playerScript = GameObject.FindWithTag ("Player2").GetComponent<Player2Health> ();
		GUI.Label (labelRect, playerScript.player2Stats.Health.ToString (), style);
	}

	void DisplayScoreCount2 ()
	{
		Rect coinIconRect = new Rect (10, 360, 32, 32);
		GUI.DrawTexture (coinIconRect, scoreIconTexture);                         

		GUIStyle style = new GUIStyle ();
		style.fontSize = 30;
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.yellow;

		Rect labelRect = new Rect (coinIconRect.xMax, coinIconRect.y, 60, 32);
		GUI.Label (labelRect, score2.ToString (), style);
	}


	void DisplayRestartButton ()
	{
		
		if (score >= 500 ) {
			Rect buttonRect = new Rect (Screen.width * 0.35f, Screen.height * 0.45f, Screen.width * 0.30f, Screen.height * 0.1f);
			if (GUI.Button (buttonRect, "ORANGE DUDE WON! Click to restart!")) {
				Application.LoadLevel (Application.loadedLevelName);
			}
		}

		if (score2 >= 500) {
			Rect buttonRect2 = new Rect (Screen.width * 0.35f, Screen.height * 0.45f, Screen.width * 0.30f, Screen.height * 0.1f);
			if (GUI.Button (buttonRect2, "GREEN DUDE WON! Click to restart!")) {
				Application.LoadLevel (Application.loadedLevelName);
			}

		}
	}

	void Start ()
	{
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster> ();

			orangeFlag = GameObject.FindWithTag ("orangeFlag");
			greenFlag = GameObject.FindWithTag ("greenFlag");
			Player1 = GameObject.FindWithTag ("Player");
			Player2 = GameObject.FindWithTag ("Player2");
		}
	}

	public Transform playerPrefab;
	public Transform spawnPoint;
	public Transform playerPrefab2;
	public Transform spawnPoint2;
	public float spawnDelay = 2;
	public Transform spawnPrefab;

	public IEnumerator RespawnPlayer ()
	{
		//audio.Play ();
		yield return new WaitForSeconds (spawnDelay);

		Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
		GameObject clone = Instantiate (spawnPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
		Destroy (clone, 3f);

	}

	public IEnumerator RespawnPlayer2 ()
	{
		//audio.Play ();
		yield return new WaitForSeconds (spawnDelay);

		Instantiate (playerPrefab2, spawnPoint2.position, spawnPoint2.rotation);
		GameObject clone2 = Instantiate (spawnPrefab, spawnPoint2.position, spawnPoint2.rotation) as GameObject;
		Destroy (clone2, 3f);
	}

	public static void KillPlayer (PlayerHealth player)
	{
		greenFlag.GetComponent<greenFlag> ().gf_Rigidbody2D.transform.parent = null;
		Destroy (player.gameObject);
		gm.StartCoroutine (gm.RespawnPlayer ());
		Player1.GetComponent<PlatformerCharacter2D> ().hasFlag = false;
	}

	public static void KillPlayer2 (Player2Health player)
	{
		orangeFlag.GetComponent<orangeFlag> ().of_Rigidbody2D.transform.parent = null;
		Destroy (player.gameObject);
		gm.StartCoroutine (gm.RespawnPlayer2 ());
		Player2.GetComponent<PlatformerCharacter2DTWO> ().hasFlag2 = false;
	}

}