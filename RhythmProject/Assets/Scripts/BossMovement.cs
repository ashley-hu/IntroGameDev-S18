using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * BossMovement class
 * - move the boss up and down at the beginning
 * - move the boss left and right across the screen
 * - fade boss out if boss health reaches 0
 * 
 * */
public class BossMovement : MonoBehaviour {

	//declare variables
	RectTransform bossImage;
	float reverse;
	Image img;
	private float delayStart;
	private bool hasDelayed;
	public AudioClip audioSound;
	private AudioSource audioSource;
	private bool soundPlayed = false;

	// Use this for initialization
	void Start () {
		img = gameObject.GetComponent<Image> ();
		bossImage = gameObject.GetComponent<RectTransform> ();
		bossImage.localPosition = new Vector3 (-387, 254f, 0); //set boss's local position on canvas
		reverse = 1;
		delayStart = 4f;
		hasDelayed = false;
		audioSource = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		//make the boss move up and down as an angry effect before song starts 
		if (!hasDelayed && GameManager.bossCurrHealth > 0) {
			delayStart -= Time.deltaTime;
			bossImage.localPosition += Vector3.up * Time.deltaTime * 40 * reverse;
			if (delayStart < 0) {
				hasDelayed = true;
				bossImage.localPosition = new Vector3 (-387, 254f, 0); //set to position when song starts
			} else { //reverse direction to give the up and down effect
				if (bossImage.localPosition.y > 258) {
					reverse = -1;
				} else if (bossImage.localPosition.y < 250) {
					reverse = 1;
				}
			}	
		}

		//if the boss is still alive, move the boss to the right and reverse if position is greater than 30, vice versa
		if (GameManager.bossCurrHealth > 0 && hasDelayed) {
			bossImage.localPosition += Vector3.right * Time.deltaTime * 10 * reverse;
			if (bossImage.localPosition.x > -380) { //move right then reverse
				reverse = -1;
			} else if (bossImage.localPosition.x < -420) { //move left then reverse 
				reverse = 1;
			}
		} else if(hasDelayed && GameManager.bossCurrHealth <= 0){
			//fade the boss out if the boss health reaches 0
			if (!soundPlayed) {
				audioSource.PlayOneShot (audioSound); //play a noise when boss dies
				soundPlayed = true;
			}
			img.CrossFadeAlpha (0, 1, false); //fade out the boss
		}
	}

}
