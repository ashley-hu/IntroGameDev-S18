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

	//0 - StartScreen
	//1 - SelectMode
	//2 - RhythmPrototype
	//3 - GameOver

	public void Awake () {
		audioSource = GetComponent<AudioSource>();
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
			GameManager.fileNumber = 1;
			if (!soundIsDone) {
				StartCoroutine(DelayedLoadOne());
				soundIsDone = true;
			}
		}
	}

//	Potential future implementation of 2nd song	
//	public void LoadSongTwo(){	
//		if (SceneManager.GetActiveScene ().buildIndex == 0) {
//			GameManager.fileNumber = 2;
//			SceneManager.LoadScene ("RhythmPrototype");
//		}
//	}

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
