using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonC : MonoBehaviour {

	private GameObject buttonC;
	private bool hit;

	// Use this for initialization
	void Start () {
		if (buttonC == null) {
			buttonC = GameObject.FindWithTag ("C");
		}
		hit = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.C)) {
			buttonC.GetComponent<SpriteRenderer> ().color = Color.red;
			if (hit) {
				Debug.Log ("Hit");
				hit = false;
			}
		}
		if (Input.GetKeyUp (KeyCode.C)) {
			buttonC.GetComponent<SpriteRenderer> ().color = Color.white;
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
