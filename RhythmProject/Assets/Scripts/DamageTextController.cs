using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class DamageTextController : MonoBehaviour{

	private static DamageText fireDmgText;
	private static DamageText waterDmgText;
	private static DamageText lightningDmgText;
	private static DamageText grassDmgText;
	private static GameObject bossParent;

	// Use this for initialization
	static DamageTextController() {
		bossParent = GameObject.FindWithTag ("BossParent");
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
		instance.transform.SetParent (bossParent.transform, false);
		instance.transform.position = bossParent.transform.position + new Vector3(10, 10, 0);
		instance.SetText (txt);
	}
}
