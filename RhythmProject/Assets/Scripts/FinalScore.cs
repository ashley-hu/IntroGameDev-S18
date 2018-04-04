using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour {

	//30 notes 
	//max score u can get ( 30 * 20 = 600) 

	private GameObject letterScore;
	private GameObject maxCombo;
	private GameObject maxScore;
	private GameObject numOfBad;
	private GameObject numOfMiss;
	private GameObject numOfGood;
	private GameObject numOfPerf;
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
	}
	
	// Update is called once per frame
	void Update () {
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
			maxCombo.GetComponent<Text> ().text = "Combo: " + GameManager.combo;
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
			numOfPerf. GetComponent<Text> ().text = "Perfect: " + GameManager.totalPerfect;
		}
	}
}
