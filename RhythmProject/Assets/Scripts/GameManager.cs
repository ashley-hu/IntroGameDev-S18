﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public AudioClip audioSource;

	void Awake(){
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadSongOne(){
		if (SceneManager.GetActiveScene ().buildIndex == 0) {

			SceneManager.LoadScene (""); 
			audioSource = Resources.Load<AudioClip>("sample");
			//load text file 
		}
	}

	public void LoadSongTwo(){
		if (SceneManager.GetActiveScene ().buildIndex == 0) {
			
		}
	}
}
