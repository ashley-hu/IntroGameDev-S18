using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public AudioClip audioSource;
	private int comboCount;
	private GameObject comboText;
	private GameObject scoreText;
	public static int combo;
	public static int score;

	void Awake(){
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}

		comboText = GameObject.FindWithTag ("Combo");
		scoreText = GameObject.FindWithTag ("Score");
	}



	// Use this for initialization
	void Start () {
		combo = 0;
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (combo != 0) {
			comboText.GetComponent<Text> ().text = combo.ToString ();
		} else {
			comboText.GetComponent<Text> ().text = "";
		}
		scoreText.GetComponent<Text> ().text = score.ToString ();
	}
}
