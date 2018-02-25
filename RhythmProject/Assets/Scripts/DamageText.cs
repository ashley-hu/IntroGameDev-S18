using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour {

	public Animator animator;
	private Text damageText;

	// Use this for initialization
	void OnEnable () {
		//Get current clip from animator 
		AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
		Destroy (gameObject, clipInfo [0].clip.length);
		damageText = animator.GetComponent<Text> ();
	}

	public void SetText(string txt){
		damageText.fontSize = 25;
		damageText.text = txt;
	}
}
