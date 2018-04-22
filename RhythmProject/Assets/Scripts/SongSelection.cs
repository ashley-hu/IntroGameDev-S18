using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * SongSelection class
 * - allows user to select song (currently only have 1 song) 
 * - after selecting, game screen will load with the proper resources
 * - wait until the sound for button is done loading before loading new scene
 * */

public class SongSelection : MonoBehaviour {

	public AudioClip clickSound;
	private AudioSource audioSource;
	private bool soundIsDone = false;
	public Button secondSongButton;
	private GameObject songTwoDescText;

	//0 - StartScreen
	//1 - SelectMode
	//2 - RhythmPrototype
	//3 - GameOver

	public void Awake () {
		audioSource = GetComponent<AudioSource>();
		songTwoDescText = GameObject.FindWithTag ("SongTwoDescription");
	}

	public void Update(){
		if (secondSongButton != null && GameManager.slayedFirstBoss != null) {
			Debug.Log("Hmm: " +GameManager.slayedFirstBoss);
			if (secondSongButton.interactable == false && GameManager.slayedFirstBoss) {
				secondSongButton.interactable = true;
				Destroy (songTwoDescText);
			}
		}
	}

	public void LoadSelectScene(){
		if (SceneManager.GetActiveScene ().buildIndex == 0) {
			if (!soundIsDone) {
				StartCoroutine(DelayedLoad());
				soundIsDone = true; 
			}
		}
	}

	//Game scene will load and file number is set to 1
	//filenumber helps signal which resources to load in SpawnNote class
	public void LoadSongOne(){
		if (SceneManager.GetActiveScene ().buildIndex == 1) {
			Destroy (GameObject.Find("Audio"));
			GameManager.fileNumber = 1;
			if (!soundIsDone) {
				StartCoroutine(DelayedLoadOne());
				soundIsDone = true;
			}
		}
	}

//	Potential future implementation of 2nd song	
	public void LoadSongTwo(){	
		if (SceneManager.GetActiveScene ().buildIndex == 1) {
			Destroy (GameObject.Find("Audio"));
			GameManager.fileNumber = 2;
			if (!soundIsDone) {
				StartCoroutine(DelayedLoadOne());
				soundIsDone = true;
			}
		}
	}

	//When game is done, return user to the Select Song screen
	public void ReturnToSelectScreen(){
		if (SceneManager.GetActiveScene ().buildIndex == 3) {
			GameManager.fileNumber = 0; //set to 0 so no resources are loaded
			if (!soundIsDone) {
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
