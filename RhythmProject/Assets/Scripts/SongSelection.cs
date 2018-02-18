using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SongSelection : MonoBehaviour {

	public AudioClip audioSource;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadSongOne(){
		if (SceneManager.GetActiveScene ().buildIndex == 0) {

			SceneManager.LoadScene ("RhythmPrototype"); 
			SpawnNote.fileNumber = 1;
		}
	}

	public void LoadSongTwo(){	
		if (SceneManager.GetActiveScene ().buildIndex == 0) {
			SceneManager.LoadScene ("RhythmPrototype");
			SpawnNote.fileNumber = 2;
		}
	}
}
