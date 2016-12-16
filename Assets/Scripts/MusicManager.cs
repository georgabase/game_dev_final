using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip SoundClip;
	private AudioSource SoundSource;
	private GameObject gameMusic;

	void Awake()
	{
		// see if we've got game music still playing
		 gameMusic = GameObject.Find("GameMusic");
		if (gameMusic) {
			// kill game music
			Destroy(gameMusic);
		}
		// make sure we survive going to different scenes
		DontDestroyOnLoad(gameObject);
}
}