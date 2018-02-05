using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonV : MonoBehaviour {

	private GameObject buttonV;

	// Use this for initialization
	void Start () {
		if (buttonV == null) {
			buttonV = GameObject.FindWithTag ("V");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.V)) {
			buttonV.GetComponent<SpriteRenderer> ().color = Color.blue;
		} 
		if (Input.GetKeyUp (KeyCode.V)) {
			buttonV.GetComponent<SpriteRenderer> ().color = Color.white;
		}
	}
}
