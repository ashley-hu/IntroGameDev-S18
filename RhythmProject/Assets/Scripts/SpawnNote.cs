using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnNote : MonoBehaviour {
	
	public GameObject note;
	private List<List<GameObject>> arrayOfMeasures = new List<List<GameObject>>();
	private float[] arrayOfColumn = new float[4];
	private int x;

	private AudioClip songOne;
	private TextAsset songData;
	private string textSongData;
	public static AudioSource songSource;
	string[] lines;
	string[] rows;
	public static float speed;
	private bool rid;

	private float bpm = 120;
	private float timeDurationOfBeat;
	private float currentBeat;
	private float bossCurrBeat;
	private float songPosition;
	private double initTime;
	private bool hasSpawned = false;
	public static float bossDamage = 10;

	private float spawnHeight = 5.5f;

	public Slider playerHealthBar;
	public Slider bossHealthBar;

	public static bool endOfSong;

	void Awake() {
		songSource = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		arrayOfColumn [0] = -1.7f;
		arrayOfColumn [1] = -0.565f;
		arrayOfColumn [2] = 0.565f;
		arrayOfColumn [3] = 1.7f;

		if (GameManager.fileNumber == 1) {
			Debug.Log ("File 1");
			songData = Resources.Load ("sampletext") as TextAsset;
			songOne = Resources.Load<AudioClip> ("demo");
			bossHealthBar.maxValue = GameManager.bossFullHealth;
			bossHealthBar.value = GameManager.bossFullHealth;
			playerHealthBar.value = GameManager.playerFullHealth;
		}
		if (GameManager.fileNumber == 2) {
			Debug.Log ("Load 2nd files");
		}

		if (songData && songOne != null) {
			textSongData = songData.text;
			ParseSongFile (textSongData);
			endOfSong = false;
			songSource.clip = songOne;

			initTime = AudioSettings.dspTime;
			songSource.PlayScheduled (initTime + 4.0f);

			speed = (spawnHeight + 4.0f) / 4.0f;
			timeDurationOfBeat = bpm / 60 / 4;
			currentBeat = 2;
		}
	}

	void ParseSongFile(string textFile){
		lines = textFile.Split('\n');
		for (int i = 0; i < lines.Length; i++) {
			rows = lines[i].Split(',');
			List<GameObject> arrayOfNotes = new List<GameObject>();
			bool hasValue = false;
			for(int a = 0; a < rows.Length; a++){
				if (rows [a].Contains ("1")) {
					hasValue = true;
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
			if (!hasValue) {
				arrayOfNotes.Add (new GameObject());
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
		}

//		//Boss Attack
//		if(songPosition > (bossCurrBeat*8)+ timeDurationOfBeat){
//			//Do damage to Player
//			GameManager.playerCurrHealth -= bossDamage;
//			if (playerHealthBar.GetComponent<Slider> ().value > 0) {
//				playerHealthBar.GetComponent<Slider> ().value -= bossDamage;
//			} else {
//				songSource.Stop ();
//				endOfSong = true;
//			}
//			bossCurrBeat += timeDurationOfBeat;
//		}

		if (!songSource.isPlaying && endOfSong) {
			if (SceneManager.GetActiveScene ().buildIndex == 1) {
				endOfSong = false;
				SceneManager.LoadScene ("GameOver");
			}
		}
	}
}