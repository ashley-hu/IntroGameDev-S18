using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * SongSelection class
 * - allows user to select song (currently only have 1 song) 
 * - after selecting, game screen will load with the proper resources
 * */

public class SongSelection : MonoBehaviour {

	//0 - SelectMode
	//1 - RhythmPrototype
	//2 - GameOver

	//Game scene will load and file number is set to 1
	//filenumber helps signal which resources to load in SpawnNote class
	public void LoadSongOne(){
		if (SceneManager.GetActiveScene ().buildIndex == 0) {
			GameManager.fileNumber = 1;
			SceneManager.LoadScene ("RhythmPrototype"); 
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
		if (SceneManager.GetActiveScene ().buildIndex == 2) {
			GameManager.fileNumber = 0; //set to 0 so no resources are loaded
			SceneManager.LoadScene ("SelectMode");
		}
	}
}
