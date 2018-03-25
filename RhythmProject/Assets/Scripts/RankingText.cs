using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
		Debug.Log ("Ranking Text: "+rankingText);       
	}

	//Set text of the animation and font size
	public void SetText(string txt){
		rankingText.fontSize = 60;
		rankingText.text = txt;
		Debug.Log ("TEXT: " + txt);
	}
}
