using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * MaintainAudio class  
 * Continues to play music in background when transitioning
 * between scenes 
*/

public class MaintainAudio : MonoBehaviour {

	//Set instance to null
	public static MaintainAudio instance = null;
	//Create AudioSource background
	public AudioSource background;

	void Awake(){
		if (instance == null) {
			instance = this; //set instance to this
			background = GetComponent<AudioSource>(); //set background to AudioSource component
			background.loop = true;	//loop the music 
			playbackgroundMusic();	//play music 
			DontDestroyOnLoad(this.gameObject); //Do not destroy instance when switching scenes
		} else {
			Destroy(this.gameObject); //destroy instance 
		}
	}

	//Play music 
	public void playbackgroundMusic() {
		background.Play ();
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}