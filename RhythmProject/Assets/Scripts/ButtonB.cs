using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonB : MonoBehaviour {

	private GameObject buttonB;

	// Use this for initialization
	void Start () {
		if (buttonB == null) {
			buttonB = GameObject.FindWithTag ("B");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.B)) {
			buttonB.GetComponent<SpriteRenderer> ().color = Color.yellow;
		}
		if (Input.GetKeyUp (KeyCode.B)) {
			buttonB.GetComponent<SpriteRenderer> ().color = Color.white;
		}
	}
}
