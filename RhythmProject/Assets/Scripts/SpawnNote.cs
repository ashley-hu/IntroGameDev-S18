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
	private float speed;
	private bool rid;

	void Awake() {
		songSource = GetComponent<AudioSource>();
		songData = Resources.Load ("sampletext") as TextAsset;
		songOne = Resources.Load<AudioClip>("demo");
	}

	// Use this for initialization
	void Start () {
		arrayOfColumn [0] = -1.5f;
		arrayOfColumn [1] = -0.5f;
		arrayOfColumn [2] = 0.5f;
		arrayOfColumn [3] = 1.5f;
		textSongData = songData.text;
		ParseSongFile (textSongData);

		songSource.clip = songOne;

		double initTime = AudioSettings.dspTime;
		songSource.PlayScheduled(initTime + 4.0f);
		speed = (5.5f + 3.5f + songSource.timeSamples) / 4.0f;
		rid = false;
	}

	void ParseSongFile(string textFile){
		lines = textFile.Split('\n');
		for (int i = 0; i < lines.Length; i++) {
			rows = lines[i].Split(',');
			List<GameObject> arrayOfNotes = new List<GameObject>();
			for(int a = 0; a < rows.Length; a++){
				if (rows [a].Contains ("1")) {
					GameObject newNote = Instantiate (note, new Vector3 (arrayOfColumn [a], 5.5f, 0), transform.rotation);
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
		if (arrayOfMeasures.Count > 0) {
			List<GameObject> result = arrayOfMeasures [arrayOfMeasures.Count - 1];
			for (int i = 0; i < result.Count; i++){
				result[i].transform.position -= result [i].transform.up * Time.deltaTime * speed;
				if (result [i].transform.position.y < -4.5f) {
					Destroy (result[i]);
					rid = true;

				}
			}
			if (rid) {
				arrayOfMeasures.Remove (result);
				rid = false;
			}
		}
	}
}
