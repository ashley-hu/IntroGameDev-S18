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
	public static float playerCurrHealth;
	public static float playerFullHealth;
	public static float bossFullHealth;
	public static float bossCurrHealth;


	private GameObject bossHealthText;
	private GameObject playerHealthText;
	public static int fileNumber = 1;


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
		bossHealthText = GameObject.FindWithTag ("BossHealth");
		playerHealthText = GameObject.FindWithTag ("PlayerHealth");
	}
		
	// Use this for initialization
	void Start () {
		combo = 0;
		score = 0;

		if (fileNumber == 1) {
			bossFullHealth = 150;
			playerFullHealth = 100;
		} else if (fileNumber == 2) {
		}

		bossCurrHealth = bossFullHealth;
		playerCurrHealth = playerFullHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (combo != 0) {
			comboText.GetComponent<Text> ().text = "Combo "+ combo.ToString ();
		} else {
			comboText.GetComponent<Text> ().text = "";
		}
		scoreText.GetComponent<Text> ().text = score.ToString ();

		if (bossCurrHealth > 0) {
			bossHealthText.GetComponent<Text> ().text = bossCurrHealth.ToString () + "/" + bossFullHealth.ToString ();
		} else {
			bossHealthText.GetComponent<Text> ().text = 0 + "/" + bossFullHealth.ToString ();
		}

		if (playerCurrHealth > 0) {
			playerHealthText.GetComponent<Text> ().text = playerCurrHealth.ToString () + "/" + playerFullHealth.ToString ();
		} else {
			playerHealthText.GetComponent<Text> ().text = 0 + "/" + playerFullHealth.ToString ();
		}
	}


	public void Reset(){
		combo = 0;
		score = 0;
		comboText.GetComponent<Text> ().text = "";
		scoreText.GetComponent<Text> ().text = "";
		bossHealthText.GetComponent<Text> ().text = "";
		playerHealthText.GetComponent<Text> ().text = "";

	}
}
