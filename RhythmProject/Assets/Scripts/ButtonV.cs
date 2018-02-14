﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonV : MonoBehaviour {

	private GameObject buttonV;
	private bool hit;

	// Use this for initialization
	void Start () {
		if (buttonV == null) {
			buttonV = GameObject.FindWithTag ("V");
		}
		hit = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.V)) {
			buttonV.GetComponent<SpriteRenderer> ().color = new Color(0, 0, 1.0F, 0.8F);
			hit = true;
		} 
		if (Input.GetKeyUp (KeyCode.V)) {
			buttonV.GetComponent<SpriteRenderer> ().color = Color.blue;
			hit = false;
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Note") {
			//bad above
			if ((coll.gameObject.transform.position.y >= -3.25f && coll.gameObject.transform.position.y < -2.5f) && hit) {
				Debug.Log ("Bad");
				Destroy (coll.gameObject);
			}
			//great above
			else if ((coll.gameObject.transform.position.y >= -3.45f && coll.gameObject.transform.position.y < -3.25f) && hit) {
				Debug.Log ("Great");
				Destroy (coll.gameObject);
			}
			//perfect
			else if (coll.gameObject.transform.position.y >= -3.55f && coll.gameObject.transform.position.y < -3.45f && hit) {
				Debug.Log ("Perfect");
				Destroy (coll.gameObject);
			}
			//great below
			else if ((coll.gameObject.transform.position.y >= -3.75f && coll.gameObject.transform.position.y < -3.55f) && hit) {
				Debug.Log ("Great");
				Destroy (coll.gameObject);
			} 
			//bad below
			else if ((coll.gameObject.transform.position.y > -4.5f && coll.gameObject.transform.position.y < -3.75f) && hit) {
				Debug.Log ("Bad");
				Destroy (coll.gameObject);
			}
		}
	}
}
