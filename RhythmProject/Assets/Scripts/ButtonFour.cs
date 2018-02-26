﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * ButtonFour class
 * - button is N
 * - get the rating (Bad, Good, Perfect) when button is pressed
 * - decrease boss health
 * - increase combo
 * - increase score
 * - create text animation for rating
 * 
 * */
public class ButtonFour : MonoBehaviour {

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
		//if key is pressed, change the alpha to signal difference 
		if (Input.GetKeyDown (KeyCode.N)) {
			buttonN.GetComponent<SpriteRenderer> ().color = new Color (0, 1.0f, 0, 0.5f);
			hit = true;
		}
		//if key is up, set solid color
		if (Input.GetKeyUp (KeyCode.N)) {
			buttonN.GetComponent<SpriteRenderer> ().color = Color.green;
			hit = false;
		}
	}

	//checks for collision with falling note 
	//if note is hit within a certain distance, a different rating will appear
	//combo, score, and boss' current health is determined here 
	// bad gets a score of +5 and -5 for boss health
	// great gets a score of +10 and =10 for boss health
	// perfect gets a score of +20 and -20 for boss health
	//after note is hit, it is destroyed 
	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Note") {
			//bad above
			if ((coll.gameObject.transform.position.y >= -3.75f && coll.gameObject.transform.position.y < -3.0f) && hit) {
				Debug.Log ("Bad");
				GameManager.combo = 0;
				GameManager.score += 5;
				GameManager.bossCurrHealth -= 5;
				badGoodPerfectText.GetComponent<Text> ().text = "";
				DamageTextController.CreateDamageText("BAD 5", 4);
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
				badGoodPerfectText.GetComponent<Text> ().text = "";
				DamageTextController.CreateDamageText("GREAT 10", 4);
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
				badGoodPerfectText.GetComponent<Text> ().text = "";
				DamageTextController.CreateDamageText("PERFECT 20", 4);
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
				badGoodPerfectText.GetComponent<Text> ().text = "";
				DamageTextController.CreateDamageText("GREAT 10", 4);
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
				badGoodPerfectText.GetComponent<Text> ().text = "";
				DamageTextController.CreateDamageText("BAD 5", 4);
				if (enemyHealth.GetComponent<Slider> ().value > 0) {
					enemyHealth.GetComponent<Slider> ().value -= 5;
				}
				Destroy (coll.gameObject);
			}
		}
	}
}