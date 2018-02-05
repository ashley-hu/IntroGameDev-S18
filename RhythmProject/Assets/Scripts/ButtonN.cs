using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonN : MonoBehaviour {

	private GameObject buttonN;

	// Use this for initialization
	void Start () {
		if (buttonN == null) {
			buttonN = GameObject.FindWithTag ("N");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.N)) {
			buttonN.GetComponent<SpriteRenderer> ().color = Color.green;
		}
		if (Input.GetKeyUp (KeyCode.N)) {
			buttonN.GetComponent<SpriteRenderer> ().color = Color.white;
		}
	}
}
