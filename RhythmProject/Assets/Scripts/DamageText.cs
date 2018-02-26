using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * DamageText class
 * - get current clip from animator
 * - destory when animation is done
 * - set the text for animation
 * 
 * Received some help from youtube tutorial: https://www.youtube.com/watch?v=fbUOG7f3jq8
 * */
public class DamageText : MonoBehaviour {

	public Animator animator;
	private Text damageText;

	// Use this for initialization
	void OnEnable () {
		//Get current clip from animator 
		AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
		//destroy when clip is done
		Destroy (gameObject, clipInfo [0].clip.length);
		//get reference to text from animator 
		damageText = animator.GetComponent<Text> ();
	}

	//Set text of the animation and font size
	public void SetText(string txt){
		damageText.fontSize = 30;
		damageText.text = txt;
	}
}
