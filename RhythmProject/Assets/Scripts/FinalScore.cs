using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * FinalScore class
 * - shows the scores when the game is over
 * - scores include combo, letter score, number score, number of bad, great, miss and perfect
 * - text description is also shown
 * - text describes if you slain the dragon, if you died, or if you failed to slay the dragon
 * - letter score is calculated here
 *
 * */
public class FinalScore : MonoBehaviour {

	private GameObject letterScore;
	private GameObject maxCombo;
	private GameObject maxScore;
	private GameObject numOfBad;
	private GameObject numOfMiss;
	private GameObject numOfGood;
	private GameObject numOfPerf;
	private GameObject textDescription;
	private string finalScore;

	// Use this for initialization
	void Start () {
		letterScore = GameObject.FindWithTag ("LetterScore");
		maxCombo = GameObject.FindWithTag ("MaxCombo");
		maxScore = GameObject.FindWithTag ("MaxScore");
		numOfBad = GameObject.FindWithTag ("NumOfBad");
		numOfMiss = GameObject.FindWithTag ("NumOfMiss");
		numOfGood = GameObject.FindWithTag ("NumOfGood");
		numOfPerf = GameObject.FindWithTag ("NumOfPerf");
		textDescription = GameObject.FindWithTag ("Description");
	}
	
	// Update is called once per frame
	void Update () {
		//letter score is determined by the value of your score
		if (letterScore != null) {
			if (GameManager.score >= 600) {
				finalScore = "SSS";
			}
			else if(GameManager.score < 600 && GameManager.score >= 560){
				finalScore = "SS";
			}
			else if(GameManager.score < 560 && GameManager.score >= 500){
				finalScore = "S";
			}
			else if(GameManager.score < 500 && GameManager.score >= 420){
				finalScore = "A";
			}
			else if(GameManager.score < 420 && GameManager.score >= 340){
				finalScore = "B";
			}
			else if(GameManager.score < 340 && GameManager.score >= 260){
				finalScore = "C";
			}
			else if(GameManager.score < 260 && GameManager.score >= 180){
				finalScore = "D";
			}
			else if(GameManager.score < 180 && GameManager.score >= 100){
				finalScore = "E";
			}
			else{
				finalScore = "F";
			}
			letterScore.GetComponent<Text> ().text = finalScore;
		}

		if (maxCombo != null) {
			maxCombo.GetComponent<Text> ().text = "Combo: " + GameManager.biggestCombo;
		}

		if (maxScore != null) {
			maxScore.GetComponent<Text> ().text = "Score: " + GameManager.score;
		}

		if (numOfBad != null) {
			numOfBad.GetComponent<Text> ().text = "Bad: " + GameManager.totalBad;
		}

		if (numOfMiss != null) {
			numOfMiss.GetComponent<Text> ().text = "Miss: " + Note.missCounter;
		}

		if (numOfGood != null) {
			numOfGood.GetComponent<Text> ().text = "Great: " + GameManager.totalGreat;
		}

		if (numOfPerf != null) {
			numOfPerf.GetComponent<Text> ().text = "Perfect: " + GameManager.totalPerfect;
		}

		//Checks if player slain the dragon with full healh or died
		if (textDescription != null) {
			if (GameManager.playerCurrHealth > 0) {
				if (GameManager.bossCurrHealth <= 0) {
					textDescription.GetComponent<Text> ().text = "Congratulations\n You Slain The Dragon!!!";
				} else if (GameManager.bossCurrHealth > 0) {
					textDescription.GetComponent<Text> ().text = "Oh No!\n You Failed To Slay The Dragon!!!";
				}
			} else {
				textDescription.GetComponent<Text> ().text = "RIP\n You Died!!!";
			}
		}
	}
}
