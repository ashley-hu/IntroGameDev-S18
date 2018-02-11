using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonB : MonoBehaviour {

	private GameObject buttonB;
	private bool hit;

	// Use this for initialization
	void Start () {
		if (buttonB == null) {
			buttonB = GameObject.FindWithTag ("B");
		}
		hit = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.B)) {
			buttonB.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 0.92f, 0.016f, 0.8f);
			if (hit) {
				Debug.Log ("Hit");
				hit = false;
			}
		}
		if (Input.GetKeyUp (KeyCode.B)) {
			buttonB.GetComponent<SpriteRenderer> ().color = Color.yellow;
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Note") {
			if (coll.gameObject.transform.position.y < -3.5) {
				hit = true;
			}
		}

	}
}
