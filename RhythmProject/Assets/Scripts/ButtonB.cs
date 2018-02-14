using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonB : MonoBehaviour {

	private GameObject buttonB;
	private bool hit;

	// Use this for initialization
	void Start () {
		if (buttonB == null) {
			buttonB = GameObject.FindWithTag ("B");
		}
		hit = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.B)) {
			buttonB.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 0.92f, 0.016f, 0.8f);
			hit = true;
		}
		if (Input.GetKeyUp (KeyCode.B)) {
			buttonB.GetComponent<SpriteRenderer> ().color = Color.yellow;
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
