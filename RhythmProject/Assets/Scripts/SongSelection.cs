using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * SongSelection class
 * - allows user to select song (currently only have 2 songs) 
 * - after selecting, game screen will load with the proper resources
 * - wait until the sound for button is done loading before loading new scene
 * - checks if condition for 2nd song is cleared before allowing users to click the button
 * 
 * Build Order
 * - 0 StartScreen
 * - 1 SelectMode
 * - 2 RhythmPrototype
 * - 3 GameOver
 * */

public class SongSelection : MonoBehaviour {

	public AudioClip clickSound;
	private AudioSource audioSource;
	private bool soundIsDone = false;
	public Button secondSongButton;
	private GameObject songTwoDescText;

	public void Awake () {
		audioSource = GetComponent<AudioSource>();
		songTwoDescText = GameObject.FindWithTag ("SongTwoDescription");
	}

	public void Update(){
		//check if player has cleared condition for second song 
		if (secondSongButton != null) {
			if (GameManager.fileNumber != 2) {
				if (secondSongButton.interactable == false && GameManager.slayedFirstBoss) {
					secondSongButton.interactable = true; //make button interactable
					Destroy (songTwoDescText); //destroy the text object 
				}
			} else {
				secondSongButton.interactable = true; //make button interactable
				Destroy (songTwoDescText); //destroy the text object 
			}
		}
	}

	//Start scene
	public void LoadSelectScene(){
		if (SceneManager.GetActiveScene ().buildIndex == 0) {
			if (!soundIsDone) { //wait until button sound is done playing before loading next scene
				StartCoroutine(DelayedLoad());
				soundIsDone = true; 
			}
		}
	}

	//Game scene will load and file number is set to 1
	//filenumber helps signal which resources to load in SpawnNote class
	public void LoadSongOne(){
		if (SceneManager.GetActiveScene ().buildIndex == 1) {
			Destroy (GameObject.Find("Audio")); //destroy the pre-game music object 
			GameManager.fileNumber = 1;
			if (!soundIsDone) { //wait until button sound is done playing before loading next scene
				StartCoroutine(DelayedLoadOne());
				soundIsDone = true;
			}
		}
	}

	//Game scene will load and file number is set to 2
	//filenumber helps signal which resources to load in SpawnNote class
	public void LoadSongTwo(){	
		if (SceneManager.GetActiveScene ().buildIndex == 1) {
			Destroy (GameObject.Find("Audio")); //destroy the pre-game music object
			GameManager.fileNumber = 2;
			if (!soundIsDone) { //wait until button sound is done playing before loading next scene
				StartCoroutine(DelayedLoadOne());
				soundIsDone = true;
			}
		}
	}

	//When game is done, return user to the Select Song screen
	public void ReturnToSelectScreen(){
		if (SceneManager.GetActiveScene ().buildIndex == 3) {
			GameManager.fileNumber = 0; //set to 0 so no resources are loaded
			if (!soundIsDone) { //wait until button sound is done playing before loading next scene
				StartCoroutine(DelayedLoad());
				soundIsDone = true;
			}
		}
	}
		
	IEnumerator DelayedLoad(){
		//Play the clip once
		audioSource.PlayOneShot (clickSound);
		//Wait until clip finish playing
		yield return new WaitForSeconds (clickSound.length);  
		SceneManager.LoadScene ("SelectMode");
	}


	IEnumerator DelayedLoadOne(){
		//Play the clip once
		audioSource.PlayOneShot (clickSound);
		//Wait until clip finish playing
		yield return new WaitForSeconds (clickSound.length);  
		SceneManager.LoadScene ("RhythmPrototype"); 
	}
}
