using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * SpawnNote class
 * - handles parsing text file to create note
 * - signals when notes should come down and at what speed they should be moving at
 * - handles which resources should be loaded (Potential future implementation for more songs) 
 * - determine the boss damage
 * - move to GameOver screen when song is done playing
 * */
public class SpawnNote : MonoBehaviour {

	//Declare variables
	public GameObject note;
	//List of game object lists for spawn of notes
	private List<List<GameObject>> arrayOfMeasures = new List<List<GameObject>>();
	//lanes for the notes to fall down 
	private float[] arrayOfColumn = new float[4];

	private AudioClip songOne;
	private TextAsset songData;
	private string textSongData;
	public static AudioSource songSource;
	string[] lines;
	string[] rows;
	public static float speed;

	private float bpm = 120; //set the bpm to 120
	private float timeDurationOfBeat;
	private float currentBeat;
	private float bossCurrBeat;
	private float songPosition;
	private double initTime;
	private bool hasSpawned = false; 
	public static float bossDamage = 10; //set boss damage to 10

	private float spawnHeight = 5.5f; //set spawn height to 5.5 (above canvas)

	public Slider playerHealthBar; 
	public Slider bossHealthBar;

	public static bool endOfSong; 

	//get audio source component
	void Awake() {
		songSource = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		//lanes 
		arrayOfColumn [0] = -1.7f; //red lane
		arrayOfColumn [1] = -0.565f; //blue lane
		arrayOfColumn [2] = 0.565f; //yellow lane
		arrayOfColumn [3] = 1.7f; //green lane

		//if filenumber is selected to be 1 from the GameManager 
		if (GameManager.fileNumber == 1) {
			Debug.Log ("File 1");
			//load the appropriate resources
			songData = Resources.Load ("sampletext") as TextAsset; 
			songOne = Resources.Load<AudioClip> ("demo-2");
			//set the sliders to the health of boss and player from GameManager
			bossHealthBar.maxValue = GameManager.bossFullHealth;
			bossHealthBar.value = GameManager.bossFullHealth;
			playerHealthBar.value = GameManager.playerFullHealth;
		}

//		Potential future implementation of 2nd song		
//		if (GameManager.fileNumber == 2) {
//			Debug.Log ("Load 2nd files");
//		}

		//if text and song is not null, parse the text file and parse song
		if (songData && songOne != null) {
			textSongData = songData.text;
			ParseSongFile (textSongData); //parse the text file for the notes 
			endOfSong = false;
			songSource.clip = songOne;

			initTime = AudioSettings.dspTime; //get the initial time of song
			songSource.PlayScheduled (initTime + 4.0f); //play after 4 seconds + the initial time

			speed = (spawnHeight + 4.0f) / 4.0f; //determine speed of notes
			timeDurationOfBeat = bpm / 60 / 4; //the number of notes per beat
			currentBeat = 2; //the current beat starts at 2 s
		}
	}

	//Parses text file
	void ParseSongFile(string textFile){
		lines = textFile.Split('\n'); //split the text by a new line and store in array
		for (int i = 0; i < lines.Length; i++) { //in each line
			rows = lines[i].Split(','); //split by comma
			List<GameObject> arrayOfNotes = new List<GameObject>();
			bool hasValue = false;
			for(int a = 0; a < rows.Length; a++){ //go through the rows arry
				if (rows [a].Contains ("1")) { //if it contain one, it has a note
					hasValue = true;
					//instantiate the note prefab at the proper column
					GameObject newNote = Instantiate (note, new Vector3 (arrayOfColumn [a], 5.5f, 0), transform.rotation);
					newNote.GetComponent<Note> ().move = false; //set move to false 
					if (a == 0) {
						newNote.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0); //if column is 0, set note color to red
					} else if (a == 1) {
						newNote.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 0); //if column is 1, set note color to blue
					} else if (a == 2) {
						newNote.GetComponent<SpriteRenderer>().color = new Color(1, 0.92f, 0.016f, 0);; //if column is 2, set note color to yellow
					} else {
						newNote.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0);; //else, set note color to green
					}
					arrayOfNotes.Add (newNote); //add note to list 
				}
			}
			if (!hasValue) {
				arrayOfNotes.Add (new GameObject()); //if there is no value, add empty game object 
			}
			arrayOfMeasures.Add (arrayOfNotes); //add list of notes to list 
		}
	}
	
	// Update is called once per frame
	void Update () {
		songPosition = (float)(AudioSettings.dspTime - initTime + 4.0f);
		//Debug.Log ("SongPosition: " + songPosition + "CurrentBeat and Time: " + (currentBeat + timeDurationOfBeat));
		if (songPosition > currentBeat + timeDurationOfBeat) {
			if (arrayOfMeasures.Count > 0) {
				List<GameObject> result = arrayOfMeasures [arrayOfMeasures.Count - 1];
				for (int i = 0; i < result.Count; i++) {
					if (result [i].gameObject != null) {
						if (result [i].GetComponent<Note> () != null) {
							result [i].GetComponent<Note> ().move = true;
						}
					}
				}
				hasSpawned = true;
			} else {
				endOfSong = true;
			}
			currentBeat += timeDurationOfBeat;
		}
		if (hasSpawned) {
			arrayOfMeasures.Remove (arrayOfMeasures [arrayOfMeasures.Count - 1]);
			hasSpawned = false;
			FireSpit.setFire = true;
//			Debug.Log ("FireSpit.setFire " + FireSpit.setFire);
//			Debug.Log ("Bruh");
		} else {
			FireSpit.setFire = false;
		}
			
		if (!songSource.isPlaying && endOfSong) {
			if (SceneManager.GetActiveScene ().buildIndex == 1) {
				endOfSong = false;
				SceneManager.LoadScene ("GameOver");
			}
		}
	}
}