using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNote : MonoBehaviour {
	
	public GameObject note;
	private float timer;
	private bool appeared;
	private List<GameObject> arrayOfNotes = new List<GameObject>();
	private float[] arrayOfColumn = new float[4];
	private int x;

	// Use this for initialization
	void Start () {
		timer = 60.0f;
		appeared = false;

		arrayOfColumn [0] = -1.5f;
		arrayOfColumn [1] = -0.5f;
		arrayOfColumn [2] = 0.5f;
		arrayOfColumn [3] = 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
		timer--;
		if (timer <= 0f && appeared == false) {
			x = Random.Range (0, 4);
			GameObject newNote = Instantiate (note, new Vector3(arrayOfColumn[x],5,0), transform.rotation);
			arrayOfNotes.Add (newNote);
			appeared = true;
			timer = 50.0f;
		} else {
			appeared = false;
		}

		if (arrayOfNotes.Count > 0) {
			for(int i=arrayOfNotes.Count-1; i>=0; i--){
				//Debug.Log (arrayOfNotes.Count);
				//Debug.Log (arrayOfNotes [i]);
				arrayOfNotes[i].transform.position -= arrayOfNotes[i].transform.up * Time.deltaTime * 5;
				if (arrayOfNotes[i].transform.position.y < -4.5f) {
					Destroy (arrayOfNotes[i]);
					arrayOfNotes.RemoveAt (i);
				}
			}
		}
	}
}
