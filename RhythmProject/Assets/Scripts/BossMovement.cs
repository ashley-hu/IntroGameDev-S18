using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * BossMovement class
 * - move the boss left and right across the screen
 * - fade boss out if boss health reaches 0
 * 
 * */
public class BossMovement : MonoBehaviour {

	RectTransform bossImage;
	float reverse;
	Image img;
	private float delayStart;
	private bool hasDelayed;
	//public Sprite rightImage;
	//public Sprite leftImage;

	// Use this for initialization
	void Start () {
		img = gameObject.GetComponent<Image> ();
		bossImage = gameObject.GetComponent<RectTransform> ();
		bossImage.localPosition = new Vector3 (-387, 254f, 0); //get boss's local position on canvas
		reverse = 1;
		delayStart = 4f;
		hasDelayed = false;
		//img.sprite = rightImage;
	}

	// Update is called once per frame
	void Update () {
		if (!hasDelayed && GameManager.bossCurrHealth > 0) {
			delayStart -= Time.deltaTime;
			bossImage.localPosition += Vector3.up * Time.deltaTime * 30 * reverse;
			if (delayStart < 0) {
				hasDelayed = true;
				bossImage.localPosition = new Vector3 (-387, 254f, 0);
			} else {
				Debug.Log (bossImage.localPosition.y);
				if (bossImage.localPosition.y > 260) {
					reverse = -1;
				} else if (bossImage.localPosition.y < 240) {
					reverse = 1;
				}
			}	
		}

		//if the boss is still alive, move the boss to the right and reverse if position is greater than 30, vice versa
		if (GameManager.bossCurrHealth > 0 && hasDelayed) {
			bossImage.localPosition += Vector3.right * Time.deltaTime * 10 * reverse;
			if (bossImage.localPosition.x > -380) { //move right then reverse
				reverse = -1;
				//img.sprite = leftImage;
			} else if (bossImage.localPosition.x < -420) { //move left then reverse 
				reverse = 1;
				//img.sprite = rightImage;
			}
//			if (bossImage.localPosition.y > 200) { //move right then reverse
//				reverse = -1;
//				//img.sprite = leftImage;
//			} else if (bossImage.localPosition.y < 235) { //move left then reverse 
//				reverse = 1;
//				//img.sprite = rightImage;
//			}

		} else if(hasDelayed && GameManager.bossCurrHealth <= 0){
			//fade the boss out if the boss health reaches 0
			Debug.Log("Fading");
			img.CrossFadeAlpha (0, 1, false);
		}
	}

}
