using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControls : MonoBehaviour {
	private GameObject buttonC;
	private GameObject buttonV;
	private GameObject buttonB;
	private GameObject buttonN;

	// Use this for initialization
	void Start () {
		if (buttonC == null) {
			buttonC = GameObject.FindWithTag ("C");
		}
		if (buttonV == null) {
			buttonV = GameObject.FindWithTag ("V");
		}
		if (buttonB == null) {
			buttonB = GameObject.FindWithTag ("B");
		}
		if (buttonN == null) {
			buttonN = GameObject.FindWithTag ("N");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.C)) {
			buttonC.GetComponent<SpriteRenderer> ().color = Color.red;
		}
		if (Input.GetKeyDown (KeyCode.V)) {
			buttonV.GetComponent<SpriteRenderer> ().color = Color.blue;
		} 
		if (Input.GetKeyDown (KeyCode.B)) {
			buttonB.GetComponent<SpriteRenderer> ().color = Color.yellow;
		} 
		if (Input.GetKeyDown (KeyCode.N)) {
			buttonN.GetComponent<SpriteRenderer> ().color = Color.green;
		}

		if (Input.GetKeyUp (KeyCode.C)) {
			buttonC.GetComponent<SpriteRenderer> ().color = Color.white;
		}
		if (Input.GetKeyUp (KeyCode.V)) {
			buttonV.GetComponent<SpriteRenderer> ().color = Color.white;
		}
		if (Input.GetKeyUp (KeyCode.B)) {
			buttonB.GetComponent<SpriteRenderer> ().color = Color.white;
		}
		if (Input.GetKeyUp (KeyCode.N)) {
			buttonN.GetComponent<SpriteRenderer> ().color = Color.white;
		}
	}
}
