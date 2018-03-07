using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * DamageTextController class
 * - instantiates proper prefab
 * - set position relative to the boss
 * 
 * Received help from youtube tutorial: https://www.youtube.com/watch?v=fbUOG7f3jq8 
 * */
public class DamageTextController : MonoBehaviour{

	private static DamageText fireDmgText;
	private static DamageText waterDmgText;
	private static DamageText lightningDmgText;
	private static DamageText grassDmgText;
	private static GameObject bossParent;

	//initialize prefabs and game object
	public static void Initialize() {
		if (bossParent == null) {
			bossParent = GameObject.FindWithTag ("BossParent");
		}
		if (fireDmgText == null) {
			fireDmgText = Resources.Load<DamageText> ("FireDamage");
		}
		if (waterDmgText == null) {
			waterDmgText = Resources.Load<DamageText> ("WaterDamage");
		}
		if (lightningDmgText == null) {
			lightningDmgText = Resources.Load<DamageText> ("LightningDamage");
		}
		if (grassDmgText == null) {
			grassDmgText = Resources.Load<DamageText> ("GrassDamage");
		}
	}
		
	//Instantiate prefabs
	//Checks value and instantiates the appropriate prefab
	public static void CreateDamageText(string txt, int value){
		DamageText instance;
		if (value == 1) {
			instance = Instantiate (fireDmgText);
		} else if (value == 2) {
			instance = Instantiate (waterDmgText);
		} else if (value == 3) {
			instance = Instantiate (lightningDmgText);
		} else {
			instance = Instantiate (grassDmgText);
		}

		if (bossParent == null) {
			bossParent = GameObject.FindWithTag ("BossParent");
		}
		//Debug.Log (bossParent);
		//Set parent to Boss Image
		instance.transform.SetParent (bossParent.transform, false);
		//Move the text slightly higher and to the right relative to parent
		instance.transform.position = bossParent.transform.position;
		//Debug.Log ("Boss pArent position " + bossParent.transform.position);
		//Debug.Log ("Instance " + instance.transform.position);
		//set the text
		instance.SetText (txt);
		Debug.Log ("TEXT WAS SET");
	}
}
