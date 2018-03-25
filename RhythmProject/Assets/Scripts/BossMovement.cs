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

//	public static Animator anim;
//	public static bool moveLeft;
//	public static bool moveRight;
//
//	void Start(){
//		anim = GetComponent<Animator> ();
//		anim.SetBool ("isLeftHit", false);
//		anim.SetBool ("isRightHit", false);
//	}
//
//	void Update(){
//		float x = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
//		//Debug.Log (x);
//		if (x >= 0.265 && x <= 0.744) {
//			moveLeft = true;
//			moveRight = false;
//		} else {
//			moveRight = true;
//			moveLeft = false;
//		}
//	}


	RectTransform bossImage;
	float reverse;
	Image img;
	//public Sprite rightImage;
	//public Sprite leftImage;

	// Use this for initialization
	void Start () {
		img = gameObject.GetComponent<Image> ();
		bossImage = gameObject.GetComponent<RectTransform> ();
		bossImage.localPosition = new Vector3 (-387, 254f, 0); //get boss's local position on canvas
		reverse = 1;
		//img.sprite = rightImage;
	}

	// Update is called once per frame
	void Update () {
		//if the boss is still alive, move the boss to the right and reverse if position is greater than 30, vice versa
		if (GameManager.bossCurrHealth > 0) {
//			 bossImage.position -= Vector3.up * Time.deltaTime * 5 * reverse;
//			if (bossImage.localPosition.x > 50) { //move right then reverse
//				reverse = -1;
//				img.sprite = leftImage;
//			} else if (bossImage.localPosition.x < -50) { //move left then reverse 
//				reverse = 1;
//				img.sprite = rightImage;
//			}
			if (bossImage.localPosition.y > 200) { //move right then reverse
				reverse = -1;
				//img.sprite = leftImage;
			} else if (bossImage.localPosition.y < 235) { //move left then reverse 
				reverse = 1;
				//img.sprite = rightImage;
			}

		} else {
			//fade the boss out if the boss health reaches 0
			img.CrossFadeAlpha (0, 1, false);
		}
	}

}
