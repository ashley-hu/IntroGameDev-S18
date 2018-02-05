using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonN : MonoBehaviour {

	private GameObject buttonN;
	private bool hit;

	// Use this for initialization
	void Start () {
		if (buttonN == null) {
			buttonN = GameObject.FindWithTag ("N");
		}
		hit = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.N)) {
			buttonN.GetComponent<SpriteRenderer> ().color = Color.green;
			if (hit) {
				Debug.Log ("Hit");
				hit = false;
			}
		}
		if (Input.GetKeyUp (KeyCode.N)) {
			buttonN.GetComponent<SpriteRenderer> ().color = Color.white;
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
