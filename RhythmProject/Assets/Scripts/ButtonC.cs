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
			buttonC.GetComponent<SpriteRenderer> ().color = new Color(1.0F, 0, 0, 0.8F);
			hit = true;
		}
		if (Input.GetKeyUp (KeyCode.C)) {
			buttonC.GetComponent<SpriteRenderer> ().color = Color.red;
			hit = false;
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Note") {
			//perfect
			if (coll.gameObject.transform.position.y == -3.5f && hit) {
				Debug.Log ("Perfect");
				Destroy (coll.gameObject);
			}
			//great
			else if ((coll.gameObject.transform.position.y > -3.5f && coll.gameObject.transform.position.y < -3.3f) && hit) {
				Debug.Log ("Great");
				Destroy (coll.gameObject);
			} 
			//bad
			else if ((coll.gameObject.transform.position.y > -3.3f && coll.gameObject.transform.position.y < -3.0f) && hit) {
				Debug.Log ("Bad");
				Destroy (coll.gameObject);
			}
		}
		
	}
}
