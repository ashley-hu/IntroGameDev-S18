using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonC : MonoBehaviour {

	private GameObject buttonC;

	// Use this for initialization
	void Start () {
		if (buttonC == null) {
			buttonC = GameObject.FindWithTag ("C");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.C)) {
			buttonC.GetComponent<SpriteRenderer> ().color = Color.red;
		}
	}

	void onCollisionEnter(){
		
	}
}
