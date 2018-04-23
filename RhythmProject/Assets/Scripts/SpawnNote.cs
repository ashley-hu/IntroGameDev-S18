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
 * - handles which resources should be loaded
 * - determine the boss damage
 * - move to GameOver screen when song is done playing
 * 
 * */
public class SpawnNote : MonoBehaviour {

	//Declare variables
	public GameObject note;
	//List of game object lists for spawn of notes
	private List<List<GameObject>> arrayOfMeasures = new List<List<GameObject>>();

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

	//private float spawnHeight = 5.5f; //set spawn height to 5.5 (above canvas)
	private float spawnHeight = 2.6f;

	public Slider playerHealthBar; 
	public Slider bossHealthBar;

	public static bool endOfSong; 
	private bool startSpawning;

	private Animator fireAnimationClip;

	//get audio source component
	void Awake() {
		songSource = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		//if filenumber is selected to be 1 from the GameManager 
		if (GameManager.fileNumber == 1) {
			//load the appropriate resources
			songData = Resources.Load ("songOne") as TextAsset; 
			songOne = Resources.Load<AudioClip> ("demo-2");
			//set the sliders to the health of boss and player from GameManager
			bossHealthBar.maxValue = GameManager.bossFullHealth;
			bossHealthBar.value = GameManager.bossFullHealth;
			playerHealthBar.value = GameManager.playerFullHealth;
		}
			
		//load appropriate resources if filenumber is 2
		if (GameManager.fileNumber == 2) {
			songData = Resources.Load ("songTwo") as TextAsset; 
			songOne = Resources.Load<AudioClip> ("demo-3");
			//set the sliders to the health of boss and player from GameManager
			bossHealthBar.maxValue = GameManager.bossFullHealth;
			bossHealthBar.value = GameManager.bossFullHealth;
			playerHealthBar.value = GameManager.playerFullHealth;
		}

		//if text and song is not null, parse the text file and parse song
		if (songData && songOne != null) {
			float beginningTimer = 0f;
			textSongData = songData.text;
			ParseSongFile (textSongData); //parse the text file for the notes 
			endOfSong = false;
			songSource.clip = songOne;

			initTime = AudioSettings.dspTime; //get the initial time of song
			//songSource.PlayScheduled (initTime + 4.0f); //play after 4 seconds + the initial time
			songSource.PlayScheduled (initTime + 8.0f);

			beginningTimer += Time.deltaTime; //in the first 4 seconds, spawn the notes from the dragon's mouth
			if (beginningTimer <= 4.0f) {
				startSpawning = true;
			}

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
					GameObject newNote = Instantiate (note, new Vector3 (-3.05f, spawnHeight, 0), transform.rotation);
					newNote.GetComponent<Note> ().move = false; //set move to false 
					newNote.GetComponent<Note>().column = a; //set the column to which it will spawn
					if (a == 0) {
						newNote.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0); //if column is 0, set note color to red
						newNote.GetComponent<Note>().destinationColumn = -1.7f; //set the destination column
						newNote.GetComponent<Transform> ().localScale = new Vector3 (0.1f, 0.1f, 0.1f); //set the size to be small
					} else if (a == 1) {
						newNote.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 0); //if column is 1, set note color to blue
						newNote.GetComponent<Note>().destinationColumn = -0.565f; //set the destination column
						newNote.GetComponent<Transform> ().localScale = new Vector3 (0.1f, 0.1f, 0.1f); //set the size to be small
					} else if (a == 2) {
						newNote.GetComponent<SpriteRenderer>().color = new Color(1, 0.92f, 0.016f, 0); //if column is 2, set note color to yellow
						newNote.GetComponent<Note>().destinationColumn = 0.565f; //set the destination column
						newNote.GetComponent<Transform> ().localScale = new Vector3 (0.1f, 0.1f, 0.1f); //set the size to be small
					} else {
						newNote.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0); //else, set note color to green
						newNote.GetComponent<Note>().destinationColumn = 1.7f; //set the destination column
						newNote.GetComponent<Transform> ().localScale = new Vector3 (0.1f, 0.1f, 0.1f); //set the size to be small
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
		if (startSpawning){
			StartSpawn(); //spawn the notes out of the dragon's mouth
		}
		//determine the position in the song
		songPosition = (float)(AudioSettings.dspTime - initTime - 2.0f);
		if (songPosition > currentBeat + timeDurationOfBeat) {
			if (arrayOfMeasures.Count > 0) {
				List<GameObject> result = arrayOfMeasures [arrayOfMeasures.Count - 1]; //get the next note
				for (int i = 0; i < result.Count; i++) { //move the note after every certain beat 
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
			currentBeat += timeDurationOfBeat; //increment beat to determine next beat
		}
		if (hasSpawned) {
			arrayOfMeasures.Remove (arrayOfMeasures [arrayOfMeasures.Count - 1]); //remove the notes from the array
			hasSpawned = false;
		}
		if (!songSource.isPlaying && endOfSong) { //if end of song is reached and song is done playing
			if (SceneManager.GetActiveScene ().buildIndex == 2) {
				endOfSong = false;
				SceneManager.LoadScene ("GameOver"); //load the game over scene when song is done playing
			}
		}
	}

	//Shoot the balls out of dragon's mouth at start of game
	//The ball will increase in size, move to position, and turn from transparent to solid color
	public void StartSpawn(){
		for (int i = 0; i < arrayOfMeasures.Count; i++) {
			for (int k = 0; k < arrayOfMeasures [i].Count; k++) {
				if ( arrayOfMeasures [i] [k].gameObject != null) {
					if (arrayOfMeasures [i] [k].GetComponent<Note> () != null) {
						//move to position
						if (arrayOfMeasures [i] [k].GetComponent<Note> ().transform.position.x <= arrayOfMeasures [i][k].GetComponent<Note> ().destinationColumn) {
							arrayOfMeasures [i][k].GetComponent<Note> ().transform.position = 
								Vector3.MoveTowards(arrayOfMeasures [i][k].GetComponent<Note> ().transform.position, new Vector3(arrayOfMeasures [i][k].GetComponent<Note> ().destinationColumn,2.6f,0), Time.deltaTime * speed * 1.5f);
						}
						//change alpha
						if (arrayOfMeasures [i] [k].GetComponent<Note> ().GetComponent<SpriteRenderer> ().color.a < 1) {
							Color c = arrayOfMeasures [i] [k].GetComponent<Note> ().GetComponent<SpriteRenderer> ().color;
							c.a += 0.01f;
							arrayOfMeasures [i] [k].GetComponent<Note> ().GetComponent<SpriteRenderer> ().color = c;
						}
						//increase in size
						if (arrayOfMeasures [i] [k].GetComponent<Note> ().GetComponent<Transform> ().localScale.x <= 1 &&
							arrayOfMeasures [i] [k].GetComponent<Note> ().GetComponent<Transform> ().localScale.y <= 1 &&
							arrayOfMeasures [i] [k].GetComponent<Note> ().GetComponent<Transform> ().localScale.z <= 1) {
							arrayOfMeasures [i] [k].GetComponent<Note> ().GetComponent<Transform> ().localScale += new Vector3(0.01F, 0.01F, 0.01F);
						}
					}
				}
			}
		}
	}
}