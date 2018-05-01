using UnityEngine;
using System.Collections;

public class BallMove : MonoBehaviour {
	int speed = 10;
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("left")) {
			MovePlayerLeft ();
	}
		if (Input.GetKey("right")) {
			MovePlayerRight ();
		}
		GameObject.FindGameObjectWithTag("Player").GetComponent<Transform> ().Translate(0,0,Time.deltaTime * speed);

	}

	void MovePlayerLeft() {
		
		var x = -1 * Time.deltaTime * speed;
		//rigidbody.AddForce (x);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Transform> ().Translate(x,0,0);
	}

	void MovePlayerRight() {

		var x = Time.deltaTime * speed;
		//rigidbody.AddForce (x);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Transform> ().Translate(x,0,0);
	}

	/*
	void OnCollisionEnter (Collision col){
		Application.Quit();
		//Destroy(GameObject.FindGameObjectWithTag("Player"));
	}
	*/

}