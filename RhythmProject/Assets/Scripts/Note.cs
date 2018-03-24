using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Note class
 * - have the note move down the screen
 * - if it has not been hit and passed below -5, is is considered a miss and destroyed
 * - if missed, bottom half of screen will flash red, and player will lose health based on boss damage in SpawnNote
 * */
public class Note : MonoBehaviour {

	public bool move;
	public float column;
	private GameObject missText;
	private GameObject healthSlider;
	private Image damageImage;
	private float alphaLevel;
	public float destinationColumn;

	// Use this for initialization
	void Start () {
		move = false; //initially set to false, will be set to true in SpawnNote 
		missText = GameObject.FindWithTag ("BadGoodPerfect");
		healthSlider = GameObject.FindWithTag ("PlayerHealthBar");
		damageImage = GameObject.FindWithTag ("DamageImage").GetComponent<Image>();
		alphaLevel = 0;

		//		arrayOfColumn [0] = -1.7f; //red lane
		//		arrayOfColumn [1] = -0.565f; //blue lane
		//		arrayOfColumn [2] = 0.565f; //yellow lane
		//		arrayOfColumn [3] = 1.7f; //green lane

//		if (column == 0) {
//			destinationColumn = -1.7f;
//		} else if (column == 1) {
//			destinationColumn = -0.565f;
//		} else if (column == 2) {
//			destinationColumn = 0.565f;
//		} else if (column == 3) {
//			destinationColumn = 1.7f;
//		}
//		Debug.Log ("Here: " + column + " " + "There " + destinationColumn);
	}

	// Update is called once per frame
	void Update () {
		if (move) { //If note is true, it will move down the screen at a particular speed 
			transform.position -= transform.up * Time.deltaTime * SpawnNote.speed;
			//Debug.Log ("Transform: " + transform.position.y);
//			if (alphaLevel <= 1 && transform.position.y < 2) {
//				alphaLevel += 0.1f;
//				Color c = gameObject.GetComponent<SpriteRenderer> ().color;
//				c.a = alphaLevel;
//				gameObject.GetComponent<SpriteRenderer> ().color = c;
//			}

			if (transform.position.y < -5.0f) {
				//Debug.Log ("Miss");
				damageImage.color = new Color (1f, 0f, 0f, 0.8f); //image color is set to red
				GameManager.combo = 0;
				missText.GetComponent<Text> ().text = "Miss";
				GameManager.playerCurrHealth -= SpawnNote.bossDamage; //health is subtracted by amount of boss damage
				if (healthSlider.GetComponent<Slider> ().value > 0) { //if playerHealh is greater than 0
					healthSlider.GetComponent<Slider> ().value -= SpawnNote.bossDamage; //subtract health from damage
				}
//				else{
//					SpawnNote.songSource.Stop (); //if player health reaches 0, stop the song
//					SpawnNote.endOfSong = true; //set endOfSong to true
//				}
				Destroy (gameObject); //destroy the game object 
			} else {
				//clear the image color 
				damageImage.color = Color.Lerp (damageImage.color, Color.clear, 20 * Time.deltaTime);
			}
		}
	}
}
