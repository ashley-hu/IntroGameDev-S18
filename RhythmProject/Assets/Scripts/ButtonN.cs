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
			buttonN.GetComponent<SpriteRenderer> ().color = new Color (0, 1.0f, 0, 0.8f);
			hit = true;
		}
		if (Input.GetKeyUp (KeyCode.N)) {
			buttonN.GetComponent<SpriteRenderer> ().color = Color.green;
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
