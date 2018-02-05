using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNote : MonoBehaviour {
	
	public GameObject note;
	private float timer;
	private bool appeared;
	private List<GameObject> arrayOfNotes = new List<GameObject>();

	// Use this for initialization
	void Start () {
		timer = 50.0f;
		appeared = false;
	}
	
	// Update is called once per frame
	void Update () {
		timer--;
		//Debug.Log (timer);
		if (timer <= 0f && appeared == false) {
			GameObject newNote = Instantiate (note, new Vector3(0,5,0), transform.rotation);
			arrayOfNotes.Add (newNote);
			appeared = true;
			timer = 50.0f;
		} else {
			appeared = false;
		}

		if (arrayOfNotes.Count > 0) {
			foreach(GameObject item in arrayOfNotes)
			{
				item.transform.position -= transform.up * Time.deltaTime * 1;
			}
		}
	}
}
