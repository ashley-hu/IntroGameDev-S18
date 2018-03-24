using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingTextController : MonoBehaviour {

	private static RankingText rankingScoreText;
	private static GameObject bossParent;

	//initialize prefabs and game object
	public static void Initialize() {
		if (bossParent == null) {
			bossParent = GameObject.FindWithTag ("Canvas");
		}
		if (rankingScoreText == null) {
			rankingScoreText = Resources.Load<RankingText> ("TextDamageParent");
		}
	}

	//Instantiate prefabs
	//Checks value and instantiates the appropriate prefab
	public static void CreateDamageText(string txt, int value){
		RankingText instance = Instantiate (rankingScoreText);

		if (bossParent == null) {
			bossParent = GameObject.FindWithTag ("Canvas");
		}
		//Debug.Log (bossParent);
		//Set parent to Boss Image
		instance.transform.SetParent (bossParent.transform, false);
		//Move the text slightly higher and to the right relative to parent
		instance.transform.position = bossParent.transform.position;
	
		Debug.Log ("Boss pArent position " + bossParent.transform.position);
		Debug.Log ("Instance " + instance.transform.position);
		//set the text
		instance.SetText (txt);
	}
}