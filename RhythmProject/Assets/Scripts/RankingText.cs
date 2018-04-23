using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * RankingText class
 * - get current clip from animator
 * - destroy when animation is done
 * - set the text for animation
 * - RankingTextController class uses this
 * 
 * Received some help from youtube tutorial: https://www.youtube.com/watch?v=fbUOG7f3jq8
 * */
public class RankingText : MonoBehaviour {

	public Animator animator;
	private Text rankingText;

	// Use this for initialization
	void OnEnable () {
		//Get current clip from animator 
		AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
		//destroy when clip is done
		Destroy (gameObject, clipInfo [0].clip.length);
		//get reference to text from animator 
		rankingText = animator.GetComponent<Text> ();      
	}

	//Set text of the animation and font size
	public void SetText(string txt){
		rankingText.fontSize = 60;
		rankingText.text = txt;
	}
}
