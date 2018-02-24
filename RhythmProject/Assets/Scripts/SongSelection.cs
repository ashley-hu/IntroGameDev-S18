using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SongSelection : MonoBehaviour {

	//0 - SelectMode
	//1 - RhythmPrototype
	//2 - GameOver

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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

	public void ReturnToSelectScreen(){
		if (SceneManager.GetActiveScene ().buildIndex == 2) {
			GameManager.fileNumber = 0;
			SceneManager.LoadScene ("SelectMode");
		}
	}
}
