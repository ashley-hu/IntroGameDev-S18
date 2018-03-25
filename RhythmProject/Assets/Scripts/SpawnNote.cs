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

	//private float spawnHeight = 5.5f; //set spawn height to 5.5 (above canvas)
	private float spawnHeight = 2.6f;

	public Slider playerHealthBar; 
	public Slider bossHealthBar;

	public static bool endOfSong; 
	private bool startSpawning;
//	private float alphaLevel;

	//get audio source component
	void Awake() {
		songSource = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		//lanes 
//		arrayOfColumn [0] = -1.7f; //red lane
//		arrayOfColumn [1] = -0.565f; //blue lane
//		arrayOfColumn [2] = 0.565f; //yellow lane
//		arrayOfColumn [3] = 1.7f; //green lane

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
			//alphaLevel = 0;
		}

//		Potential future implementation of 2nd song		
//		if (GameManager.fileNumber == 2) {
//			Debug.Log ("Load 2nd files");
//		}

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

			beginningTimer += Time.deltaTime;
			Debug.Log("beginning timer: "+ beginningTimer);
			if (beginningTimer <= 4.0f) {
				startSpawning = true;
				//StartSpawn();
				Debug.Log("Start spawning: ");
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
					//GameObject newNote = Instantiate (note, new Vector3 (arrayOfColumn [a], 5.5f, 0), transform.rotation);
					GameObject newNote = Instantiate (note, new Vector3 (-3.05f, spawnHeight, 0), transform.rotation);
					newNote.GetComponent<Note> ().move = false; //set move to false 
					newNote.GetComponent<Note>().column = a;

//					if (column == 0) {
//						destinationColumn = -1.7f;
//					} else if (column == 1) {
//						destinationColumn = -0.565f;
//					} else if (column == 2) {
//						destinationColumn = 0.565f;
//					} else if (column == 3) {
//						destinationColumn = 1.7f;
//					}
//					Debug.Log ("Here: " + column + " " + "There " + destinationColumn);

					if (a == 0) {
						newNote.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0); //if column is 0, set note color to red
						//newNote.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
						newNote.GetComponent<Note>().destinationColumn = -1.7f;
					} else if (a == 1) {
						newNote.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 0); //if column is 1, set note color to blue
						//newNote.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1); 
						newNote.GetComponent<Note>().destinationColumn = -0.565f;
					} else if (a == 2) {
						//newNote.GetComponent<SpriteRenderer>().color = new Color(1, 0.92f, 0.016f); 
						newNote.GetComponent<SpriteRenderer>().color = new Color(1, 0.92f, 0.016f, 0);; //if column is 2, set note color to yellow
						newNote.GetComponent<Note>().destinationColumn = 0.565f;
					} else {
						//newNote.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
						newNote.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0); //else, set note color to green
						newNote.GetComponent<Note>().destinationColumn = 1.7f;
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
			StartSpawn();
			//startSpawning = false;
		}
		songPosition = (float)(AudioSettings.dspTime - initTime - 2.0f);
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
		//	FireSpit.setFire = true;
//			Debug.Log ("FireSpit.setFire " + FireSpit.setFire);
//			Debug.Log ("Bruh");
		} else {
			//FireSpit.setFire = false;
		}
			
		if (!songSource.isPlaying && endOfSong) {
			if (SceneManager.GetActiveScene ().buildIndex == 1) {
				endOfSong = false;
				SceneManager.LoadScene ("GameOver");
			}
		}
	}

	public void StartSpawn(){
//		for (int i = 0; i < arrayOfMeasures.Count; i++) {
//			for (int k = 0; k < arrayOfMeasures [i].Count; k++) {
//				Debug.Log (" DJLAKWDWD: " + arrayOfMeasures [i] [k].transform.position);
//			}
//		}

		for (int i = 0; i < arrayOfMeasures.Count; i++) {
			for (int k = 0; k < arrayOfMeasures [i].Count; k++) {
				//Debug.Log (" DJLAKWDWD: " + arrayOfMeasures [i] [k].transform.position);
				if ( arrayOfMeasures [i] [k].gameObject != null) {
					if (arrayOfMeasures [i] [k].GetComponent<Note> () != null) {
						if (arrayOfMeasures [i] [k].GetComponent<Note> ().transform.position.x <= arrayOfMeasures [i][k].GetComponent<Note> ().destinationColumn) {
							//Debug.Log ("POSition " + arrayOfMeasures [i][k].GetComponent<Note> ().transform.position);
							//Debug.Log ("Second Pos: " + arrayOfMeasures [i][k].GetComponent<Note> ().destinationColumn);
							arrayOfMeasures [i][k].GetComponent<Note> ().transform.position = 
								Vector3.MoveTowards(arrayOfMeasures [i][k].GetComponent<Note> ().transform.position, new Vector3(arrayOfMeasures [i][k].GetComponent<Note> ().destinationColumn,2.6f,0), Time.deltaTime * speed);
						}
						if (arrayOfMeasures [i] [k].GetComponent<Note> ().GetComponent<SpriteRenderer> ().color.a < 1) {
							Color c = arrayOfMeasures [i] [k].GetComponent<Note> ().GetComponent<SpriteRenderer> ().color;
							c.a += 0.01f;
							arrayOfMeasures [i] [k].GetComponent<Note> ().GetComponent<SpriteRenderer> ().color = c;
						}

					}
				}

			}
		}

//		if (arrayOfMeasures.Count > 0) {
//				for (int i = 0; i < result.Count; i++) {
//					Debug.Log ("result " + result.Count);
//					//for (int i = 0; i < result.Count; i++) {
//					Debug.Log ("i : " + i);
//					if (result [i].gameObject != null) {
//						if (result [i].GetComponent<Note> () != null) {
//							if (result [i].GetComponent<Note> ().transform.position.x <= result [i].GetComponent<Note> ().destinationColumn) {
//								Debug.Log ("POSition " + result [i].GetComponent<Note> ().transform.position);
//								Debug.Log ("Second Pos: " + result [i].GetComponent<Note> ().destinationColumn);
//								result [i].GetComponent<Note> ().transform.position += transform.right * Time.deltaTime * SpawnNote.speed * 2;
//								//Debug.Log(result[i].GetComponent<Note>().transform.position);
//								//continue;
//							}
//						}
//					}
//			}
		}
//	}
}