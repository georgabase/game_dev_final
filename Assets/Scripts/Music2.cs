using UnityEngine;
using System.Collections;

public class Music2 : MonoBehaviour {
	private GameObject menuMusic;


	// Use this for initialization
	void Start () {
		// see if we've got menu music still playing
		menuMusic = GameObject.Find("MenuMusic");
		if (menuMusic) {
			// kill menu music
			Destroy(menuMusic);
		}
		// make sure we survive going to different scenes
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
