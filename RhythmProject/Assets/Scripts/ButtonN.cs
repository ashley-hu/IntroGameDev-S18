using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonN : MonoBehaviour {

	private GameObject buttonN;
	private GameObject enemyHealth;
	private bool hit;
	private GameObject badGoodPerfectText;

	// Use this for initialization
	void Start () {
		if (buttonN == null) {
			buttonN = GameObject.FindWithTag ("N");
		}
		hit = false;
		enemyHealth = GameObject.FindWithTag ("Health");
		badGoodPerfectText = GameObject.FindWithTag ("BadGoodPerfect");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.N)) {
			buttonN.GetComponent<SpriteRenderer> ().color = new Color (0, 1.0f, 0, 0.5f);
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
			if ((coll.gameObject.transform.position.y >= -3.75f && coll.gameObject.transform.position.y < -3.0f) && hit) {
				Debug.Log ("Bad");
				GameManager.combo = 0;
				GameManager.score += 5;
				GameManager.bossCurrHealth -= 5;
				//badGoodPerfectText.GetComponent<Text> ().text = "Bad";
				DamageTextController.CreateDamageText("BAD", 4);
				if (enemyHealth.GetComponent<Slider> ().value > 0) {
					enemyHealth.GetComponent<Slider> ().value -= 5;
				}
				Destroy (coll.gameObject);
			}
			//great above
			else if ((coll.gameObject.transform.position.y >= -3.95f && coll.gameObject.transform.position.y < -3.75f) && hit) {
				Debug.Log ("Great");
				GameManager.combo += 1;
				GameManager.score += 10;
				GameManager.bossCurrHealth -= 10;
				//badGoodPerfectText.GetComponent<Text> ().text = "Great";
				DamageTextController.CreateDamageText("GREAT", 4);
				if (enemyHealth.GetComponent<Slider> ().value > 0) {
					enemyHealth.GetComponent<Slider> ().value -= 10;
				}
				Destroy (coll.gameObject);
			}
			//perfect
			else if (coll.gameObject.transform.position.y >= -4.05f && coll.gameObject.transform.position.y < -3.95f && hit) {
				Debug.Log ("Perfect");
				GameManager.combo += 1;
				GameManager.score += 20;
				GameManager.bossCurrHealth -= 20;
				//badGoodPerfectText.GetComponent<Text> ().text = "Perfect";
				DamageTextController.CreateDamageText("PERFECT", 4);
				if (enemyHealth.GetComponent<Slider> ().value > 0) {
					enemyHealth.GetComponent<Slider> ().value -= 20;
				}
				Destroy (coll.gameObject);
			}
			//great below
			else if ((coll.gameObject.transform.position.y >= -4.25f && coll.gameObject.transform.position.y < -4.05f) && hit) {
				Debug.Log ("Great");
				GameManager.combo += 1;
				GameManager.score += 10;
				GameManager.bossCurrHealth -= 10;
				//badGoodPerfectText.GetComponent<Text> ().text = "Great";
				DamageTextController.CreateDamageText("GREAT", 4);
				if (enemyHealth.GetComponent<Slider> ().value > 0) {
					enemyHealth.GetComponent<Slider> ().value -= 10;
				}
				Destroy (coll.gameObject);
			} 
			//bad below
			else if ((coll.gameObject.transform.position.y > -5.0f && coll.gameObject.transform.position.y < -4.25f) && hit) {
				Debug.Log ("Bad");
				GameManager.combo = 0;
				GameManager.score += 5;
				GameManager.bossCurrHealth -= 5;
				//badGoodPerfectText.GetComponent<Text> ().text = "Bad";
				DamageTextController.CreateDamageText("BAD", 4);
				if (enemyHealth.GetComponent<Slider> ().value > 0) {
					enemyHealth.GetComponent<Slider> ().value -= 5;
				}
				Destroy (coll.gameObject);
			}
		}
	}
}
