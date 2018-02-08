﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SpawnNote : MonoBehaviour {
	
	public GameObject note;
	private float timer;
	private bool appeared;
	private List<GameObject> arrayOfNotes = new List<GameObject>();
	private float[] arrayOfColumn = new float[4];
	private int x;

	private AudioClip songOne;
	private TextAsset songData;
	private string textSongData;
	public AudioSource songSource;
	string[] lines;

	void Awake() {
		songSource = GetComponent<AudioSource>();
		songData = Resources.Load ("sampletext") as TextAsset;
		songOne = Resources.Load<AudioClip>("sample");
		//Debug.Log (songOne);
		//Debug.Log (songData);
	}

	// Use this for initialization
	void Start () {
		timer = 60.0f;
		appeared = false;

		arrayOfColumn [0] = -1.5f;
		arrayOfColumn [1] = -0.5f;
		arrayOfColumn [2] = 0.5f;
		arrayOfColumn [3] = 1.5f;
		textSongData = songData.text;
		ParseSongFile (textSongData);

		songSource.clip = songOne;
		songSource.Play ();
		Debug.Log (songSource);
	}

	void ParseSongFile(string x){
		lines = x.Split('\n');
	}
	
	// Update is called once per frame
	void Update () {
//		timer--;
//		if (timer <= 0f && appeared == false) {
//			x = Random.Range (0, 4);
//			GameObject newNote = Instantiate (note, new Vector3(arrayOfColumn[x],5,0), transform.rotation);
//			arrayOfNotes.Add (newNote);
//			appeared = true;
//			timer = 50.0f;
//		} else {
//			appeared = false;
//		}
//
//		if (arrayOfNotes.Count > 0) {
//			for(int i=arrayOfNotes.Count-1; i>=0; i--){
//				//Debug.Log (arrayOfNotes.Count);
//				//Debug.Log (arrayOfNotes [i]);
//				arrayOfNotes[i].transform.position -= arrayOfNotes[i].transform.up * Time.deltaTime * 5;
//				if (arrayOfNotes[i].transform.position.y < -4.5f) {
//					Destroy (arrayOfNotes[i]);
//					arrayOfNotes.RemoveAt (i);
//					//Debug.Log ("Miss");
//				}
//			}
//		}

		//if(songSource.timeSamples)
		for(int i=0;i < lines.Length; i++){
		}

	}
}
