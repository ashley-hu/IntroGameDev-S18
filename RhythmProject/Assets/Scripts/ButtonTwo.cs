using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * ButtonTwo class
 * - button is V
 * - get the rating (Bad, Good, Perfect) when button is pressed
 * - decrease boss health
 * - increase combo
 * - increase score
 * - create text animation for rating
 * 
 * */
public class ButtonTwo : MonoBehaviour {

	public ParticleSystem particles;

	private GameObject buttonV;
	private GameObject enemyHealth;
	private GameObject badGoodPerfectText;
	private bool hit;
	private Image bossIm;
	public AudioClip audioClip1;
	public AudioClip audioClip2;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		if (buttonV == null) {
			buttonV = GameObject.FindWithTag ("V");
		}
		hit = false;
		enemyHealth = GameObject.FindWithTag ("Health");
		badGoodPerfectText = GameObject.FindWithTag ("BadGoodPerfect");
		bossIm = GameObject.FindWithTag ("BossParent").GetComponent<Image>();
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		//if key is pressed, change the alpha to signal difference 
		if (Input.GetKeyDown (KeyCode.V)) {
			buttonV.GetComponent<SpriteRenderer> ().color = new Color(0, 0, 1.0F, 0.5F);
			hit = true;
			audioSource.PlayOneShot (audioClip2);
		} 
		//if key is up, set solid color
		if (Input.GetKeyUp (KeyCode.V)) {
			buttonV.GetComponent<SpriteRenderer> ().color = Color.blue;
			hit = false;
		}
		bossIm.color = Color.Lerp (bossIm.color, Color.white, Time.deltaTime * 0.7f);
	}

	//checks for collision with falling note 
	//if note is hit within a certain distance, a different rating will appear
	//combo, score, and boss' current health is determined here 
	// bad gets a score of +5 and -5 for boss health
	// great gets a score of +10 and =10 for boss health
	// perfect gets a score of +20 and -20 for boss health
	//after note is hit, it is destroyed 
	// create a particle effect when note collides
	// show Damage text on boss and ranking text in center of screen
	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Note") {
			//bad above
			if ((coll.gameObject.transform.position.y >= -3.75f && coll.gameObject.transform.position.y < -3.0f) && hit) {
				Debug.Log ("Bad");
				particles.Play ();
				audioSource.PlayOneShot (audioClip1);
				GameManager.combo = 0;
				GameManager.score += 5;
				GameManager.bossCurrHealth -= 5;
				badGoodPerfectText.GetComponent<Text> ().text = "";
				bossIm.color = coll.gameObject.GetComponent<SpriteRenderer> ().color;
				if (GameManager.bossCurrHealth > 0) {
					DamageTextController.CreateDamageText ("5", 2);
				}
				RankingTextController.CreateDamageText ("BAD", 1);
				if (enemyHealth.GetComponent<Slider> ().value > 0) {
					enemyHealth.GetComponent<Slider> ().value -= 5;
				}
				GameManager.totalBad++;
				Destroy (coll.gameObject);
			}
			//great above
			else if ((coll.gameObject.transform.position.y >= -3.95f && coll.gameObject.transform.position.y < -3.75f) && hit) {
				Debug.Log ("Great");
				particles.Play ();
				audioSource.PlayOneShot (audioClip1);
				GameManager.combo += 1;
				GameManager.score += 10;
				GameManager.bossCurrHealth -= 10;
				badGoodPerfectText.GetComponent<Text> ().text = "";
				bossIm.color = coll.gameObject.GetComponent<SpriteRenderer> ().color;
				if (GameManager.bossCurrHealth > 0) {
					DamageTextController.CreateDamageText ("10", 2);
				}
				RankingTextController.CreateDamageText ("GREAT", 1);
				if (enemyHealth.GetComponent<Slider> ().value > 0) {
					enemyHealth.GetComponent<Slider> ().value -= 10;
				}
				GameManager.totalGreat++;
				Destroy (coll.gameObject);
			}
			//perfect
			else if (coll.gameObject.transform.position.y >= -4.05f && coll.gameObject.transform.position.y < -3.95f && hit) {
				Debug.Log ("Perfect");
				particles.Play ();
				audioSource.PlayOneShot (audioClip1);
				GameManager.combo += 1;
				GameManager.score += 20;
				GameManager.bossCurrHealth -= 20;
				badGoodPerfectText.GetComponent<Text> ().text = "";
				bossIm.color = coll.gameObject.GetComponent<SpriteRenderer> ().color;
				if (GameManager.bossCurrHealth > 0) {
					DamageTextController.CreateDamageText ("20", 2);
				}
				RankingTextController.CreateDamageText ("PERFECT", 1);
				if (enemyHealth.GetComponent<Slider> ().value > 0) {
					enemyHealth.GetComponent<Slider> ().value -= 20;
				}
				GameManager.totalPerfect++;
				Destroy (coll.gameObject);
			}
			//great below
			else if ((coll.gameObject.transform.position.y >= -4.25f && coll.gameObject.transform.position.y < -4.05f) && hit) {
				Debug.Log ("Great");
				particles.Play ();
				audioSource.PlayOneShot (audioClip1);
				GameManager.combo += 1;
				GameManager.score += 10;
				GameManager.bossCurrHealth -= 10;
				badGoodPerfectText.GetComponent<Text> ().text = "";
				bossIm.color = coll.gameObject.GetComponent<SpriteRenderer> ().color;
				if (GameManager.bossCurrHealth > 0) {
					DamageTextController.CreateDamageText ("10", 2);
				}
				RankingTextController.CreateDamageText ("GREAT", 1);
				if (enemyHealth.GetComponent<Slider> ().value > 0) {
					enemyHealth.GetComponent<Slider> ().value -= 10;
				}
				GameManager.totalGreat++;
				Destroy (coll.gameObject);
			} 
			//bad below
			else if ((coll.gameObject.transform.position.y > -5.0f && coll.gameObject.transform.position.y < -4.25f) && hit) {
				Debug.Log ("Bad");
				particles.Play ();
				audioSource.PlayOneShot (audioClip1);
				GameManager.combo = 0;
				GameManager.score += 5;
				GameManager.bossCurrHealth -= 5;
				badGoodPerfectText.GetComponent<Text> ().text = "";
				bossIm.color = coll.gameObject.GetComponent<SpriteRenderer> ().color;
				if (GameManager.bossCurrHealth > 0) {
					DamageTextController.CreateDamageText ("5", 2);
				}
				RankingTextController.CreateDamageText ("BAD", 1);
				if (enemyHealth.GetComponent<Slider> ().value > 0) {
					enemyHealth.GetComponent<Slider> ().value -= 5;
				}
				GameManager.totalBad++;
				Destroy (coll.gameObject);
			}
		}
	}
}
