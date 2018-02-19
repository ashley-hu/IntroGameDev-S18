using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonN : MonoBehaviour {

	private GameObject buttonN;
	private GameObject enemyHealth;
	private bool hit;

	// Use this for initialization
	void Start () {
		if (buttonN == null) {
			buttonN = GameObject.FindWithTag ("N");
		}
		hit = false;
		enemyHealth = GameObject.FindWithTag ("Health");
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
			//bad above
			if ((coll.gameObject.transform.position.y >= -3.25f && coll.gameObject.transform.position.y < -2.5f) && hit) {
				Debug.Log ("Bad");
				if (enemyHealth.GetComponent<Slider> ().value > 0) {
					enemyHealth.GetComponent<Slider> ().value -= 5;
				}
				Destroy (coll.gameObject);
			}
			//great above
			else if ((coll.gameObject.transform.position.y >= -3.45f && coll.gameObject.transform.position.y < -3.25f) && hit) {
				Debug.Log ("Great");
				if (enemyHealth.GetComponent<Slider> ().value > 0) {
					enemyHealth.GetComponent<Slider> ().value -= 10;
				}
				Destroy (coll.gameObject);
			}
			//perfect
			else if (coll.gameObject.transform.position.y >= -3.55f && coll.gameObject.transform.position.y < -3.45f && hit) {
				Debug.Log ("Perfect");
				if (enemyHealth.GetComponent<Slider> ().value > 0) {
					enemyHealth.GetComponent<Slider> ().value -= 20;
				}
				Destroy (coll.gameObject);
			}
			//great below
			else if ((coll.gameObject.transform.position.y >= -3.75f && coll.gameObject.transform.position.y < -3.55f) && hit) {
				Debug.Log ("Great");
				if (enemyHealth.GetComponent<Slider> ().value > 0) {
					enemyHealth.GetComponent<Slider> ().value -= 10;
				}
				Destroy (coll.gameObject);
			} 
			//bad below
			else if ((coll.gameObject.transform.position.y > -4.5f && coll.gameObject.transform.position.y < -3.75f) && hit) {
				Debug.Log ("Bad");
				if (enemyHealth.GetComponent<Slider> ().value > 0) {
					enemyHealth.GetComponent<Slider> ().value -= 5;
				}
				Destroy (coll.gameObject);
			}
		}
	}
}
