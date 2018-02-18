using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SpawnNote : MonoBehaviour {
	
	public GameObject note;
	private List<List<GameObject>> arrayOfMeasures = new List<List<GameObject>>();
	private float[] arrayOfColumn = new float[4];
	private int x;

	private AudioClip songOne;
	private TextAsset songData;
	private string textSongData;
	public AudioSource songSource;
	string[] lines;
	string[] rows;
	public static float speed;
	private bool rid;

	private float bpm = 120;
	private float timeDurationOfBeat;
	private float currentBeat;
	private float songPosition;
	private double initTime;
	private bool hasSpawned = false;

	private float spawnHeight = 5.5f;

	//create static variable
	// set value to 1 , 2, etc. 
	// update static value in song selection
	// if 1, load this, 
	// if 2, load that

	public static int fileNumber;

	void Awake() {
		songSource = GetComponent<AudioSource>();

		if (fileNumber == 1) {
			songData = Resources.Load ("sampletext") as TextAsset;
			songOne = Resources.Load<AudioClip> ("demo");
		}
		if (fileNumber == 2) {
			Debug.Log ("Load 2nd files");
		}

		//check if u can load data from the scene 
	}

	// Use this for initialization
	void Start () {
		arrayOfColumn [0] = -1.65f;
		arrayOfColumn [1] = -0.55f;
		arrayOfColumn [2] = 0.55f;
		arrayOfColumn [3] = 1.65f;

		if (songData && songOne != null) {
			textSongData = songData.text;
			ParseSongFile (textSongData);

			songSource.clip = songOne;

			initTime = AudioSettings.dspTime;
			songSource.PlayScheduled (initTime + 4.0f);

			speed = (spawnHeight + 3.5f) / 4.0f;
			timeDurationOfBeat = bpm / 60;
			currentBeat = 2;
		}
	}

	void ParseSongFile(string textFile){
		lines = textFile.Split('\n');
		for (int i = 0; i < lines.Length; i++) {
			rows = lines[i].Split(',');
			List<GameObject> arrayOfNotes = new List<GameObject>();
			for(int a = 0; a < rows.Length; a++){
				if (rows [a].Contains ("1")) {
					GameObject newNote = Instantiate (note, new Vector3 (arrayOfColumn [a], 5.5f, 0), transform.rotation);
					newNote.GetComponent<Note> ().move = false;
					if (a == 0) {
						newNote.GetComponent<SpriteRenderer>().color = Color.red;
					} else if (a == 1) {
						newNote.GetComponent<SpriteRenderer>().color = Color.blue;
					} else if (a == 2) {
						newNote.GetComponent<SpriteRenderer>().color = Color.yellow;
					} else {
						newNote.GetComponent<SpriteRenderer>().color = Color.green;
					}
					arrayOfNotes.Add (newNote);
				}
			}
			arrayOfMeasures.Add (arrayOfNotes);
		}
	}
	
	// Update is called once per frame
	void Update () {
		songPosition = (float)(AudioSettings.dspTime - initTime + 4.0f);
		//Debug.Log ("SongPosition: " + songPosition + "CurrentBeat and Time: " + (currentBeat + timeDurationOfBeat));
		if (songPosition > currentBeat + timeDurationOfBeat) {
			if (arrayOfMeasures.Count > 0) {
				List<GameObject> result = arrayOfMeasures [arrayOfMeasures.Count - 1];
				for (int i = 0; i < result.Count; i++){
					if (result [i].gameObject != null) {
						result [i].GetComponent<Note>().move = true;
					}
				}
				hasSpawned = true;
			}
			currentBeat += timeDurationOfBeat;
		}
		if (hasSpawned) {
			arrayOfMeasures.Remove (arrayOfMeasures [arrayOfMeasures.Count - 1]);
			hasSpawned = false;
		}
	}
}

//WHat needs to be done 
/*
 * be able to load the proper files from level select
 * make spawnnote generic, aka, the music source should be loaded elsewhere and the textfile
 * boss fight
 * how to load the proper boss with health -> prefab(?)
 * how do key presses from different keys do dmg, is this manageable -> get reference of boss's health by doing findwithtag
 * / */