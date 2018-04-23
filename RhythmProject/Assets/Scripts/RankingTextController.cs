using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * RankingTextController class
 * - controller for animation damage to boss 
 * - instantiates proper prefab
 * - set position relative to the boss
 * - sets text using method from RankingText class
 * 
 * Received help from youtube tutorial: https://www.youtube.com/watch?v=fbUOG7f3jq8 
 * */
public class RankingTextController : MonoBehaviour {

	private static RankingText rankingScoreText;
	private static GameObject bossParent;

	//initialize prefabs and game object
	public static void Initialize() {
		if (bossParent == null) {
			bossParent = GameObject.FindWithTag ("Canvas2");
		}
		if (rankingScoreText == null) {
			rankingScoreText = Resources.Load<RankingText> ("PopupTextParent");
		}
	}

	//Instantiate prefab
	public static void CreateDamageText(string txt, int value){
		RankingText instance = Instantiate (rankingScoreText);
		if (bossParent == null) {
			bossParent = GameObject.FindWithTag ("Canvas2");
		}
		//set the parent
		instance.transform.SetParent (bossParent.transform , false);
		//Move the text slightly higher and to the right relative to parent
		instance.transform.position = bossParent.transform.position;
		instance.SetText (txt);
	}
}