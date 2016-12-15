using UnityEngine;
using System.Collections;

public class medpack : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D otherCollider)
	{

		if (otherCollider.CompareTag("Player") || otherCollider.CompareTag("Player2")) {

			Destroy (this.gameObject);
			//}
		} }

}
