using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Note class
 * - have the note move down the screen
 * - if it has not been hit and passed below -5, is is considered a miss and destroyed
 * - if missed, bottom half of screen will flash red, and player will lose health based on boss damage in SpawnNote
 * 
 * */
public class Note : MonoBehaviour {

	public bool move;
	public float column;
	private GameObject missText;
	private GameObject healthSlider;
	private Image damageImage;
	public float destinationColumn;
	public static float missCounter;

	// Use this for initialization
	void Start () {
		move = false; //initially set to false, will be set to true in SpawnNote 
		missText = GameObject.FindWithTag ("BadGoodPerfect");
		healthSlider = GameObject.FindWithTag ("PlayerHealthBar");
		damageImage = GameObject.FindWithTag ("DamageImage").GetComponent<Image>();
		missCounter = 0;
	}

	// Update is called once per frame
	void Update () {
		if (move) { //If note is true, it will move down the screen at a particular speed 
			transform.position -= transform.up * Time.deltaTime * SpawnNote.speed;
			if (transform.position.y < -5.0f) {
				damageImage.color = new Color (1f, 0f, 0f, 0.8f); //image color is set to red
				GameManager.combo = 0;
				missCounter++;
				RankingTextController.CreateDamageText ("MISS", 1);
				GameManager.playerCurrHealth -= SpawnNote.bossDamage; //health is subtracted by amount of boss damage
				if (healthSlider.GetComponent<Slider> ().value > 0) { //if playerHealh is greater than 0
					healthSlider.GetComponent<Slider> ().value -= SpawnNote.bossDamage; //subtract health from damage
				}
				Destroy (gameObject); //destroy the game object 
			} else {
				//clear the image color 
				damageImage.color = Color.Lerp (damageImage.color, Color.clear, 20 * Time.deltaTime);
			}
		}
	}
}
