using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * GameManager class
 * - Manages the current score, the combo count, the text health of boss and player
 * - resets the score, count, and text health when loaded
 * 
 * */

public class GameManager : MonoBehaviour {

	//declare the variables 
	private int comboCount;
	private GameObject comboText;
	private GameObject scoreText;
	public static int combo;
	public static int score;
	public static float playerCurrHealth;
	public static float playerFullHealth;
	public static float bossFullHealth;
	public static float bossCurrHealth;


	private GameObject bossHealthText;
	private GameObject playerHealthText;
	public static int fileNumber;

	//get the referenced game objects
	void Awake(){
		comboText = GameObject.FindWithTag ("Combo");
		scoreText = GameObject.FindWithTag ("Score");
		bossHealthText = GameObject.FindWithTag ("BossHealth");
		playerHealthText = GameObject.FindWithTag ("PlayerHealth");
	}
		
	// Use this for initialization
	void Start () {
		DamageTextController.Initialize ();

		//if number is 1, set the playerhealth and bosshealth to 100 and 150 respectively
		if (fileNumber == 1) {
			bossFullHealth = 150; //set bossHealth to 150
			playerFullHealth = 100; //set playerHealth to 100
		}

//		Potential future implementation
//		if (fileNumber == 2) {
//			bossFullHealth = 200;
//			playerFullHealth = 150;
//
//		}

		//set the current health to full health
		bossCurrHealth = bossFullHealth;
		playerCurrHealth = playerFullHealth;
		Reset (); //reset all variables and text
	}
	
	// Update is called once per frame
	void Update () {
		if (comboText != null) {
			if (combo != 0) {
				comboText.GetComponent<Text> ().text = "Combo " + combo.ToString ();
			} else {
				comboText.GetComponent<Text> ().text = "";
			}
		}

		if (scoreText != null) {
			scoreText.GetComponent<Text> ().text = score.ToString ();
		}

		if (bossHealthText != null) {
			if (bossCurrHealth > 0) {
				bossHealthText.GetComponent<Text> ().text = bossCurrHealth.ToString () + "/" + bossFullHealth.ToString ();
			} else {
				bossHealthText.GetComponent<Text> ().text = "DEFEATED";
			}
		}

		if (playerHealthText != null) {
			if (playerCurrHealth > 0) {
				playerHealthText.GetComponent<Text> ().text = playerCurrHealth.ToString () + "/" + playerFullHealth.ToString ();
			} else {
				SpawnNote.songSource.Stop (); //if player health reaches 0, stop the song
				SpawnNote.endOfSong = true; //set endOfSong to true
				//playerHealthText.GetComponent<Text> ().text = 0 + "/" + playerFullHealth.ToString ();
			}
		}
	}

	//Reset variables and text
	public void Reset(){
		combo = 0;
		score = 0;
		comboText.GetComponent<Text> ().text = "";
		scoreText.GetComponent<Text> ().text = "";
		bossHealthText.GetComponent<Text> ().text = "";
		playerHealthText.GetComponent<Text> ().text = "";

	}
}
