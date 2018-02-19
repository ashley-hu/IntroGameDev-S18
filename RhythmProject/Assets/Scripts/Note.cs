using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour {

	public bool move;
	private GameObject missText;

	// Use this for initialization
	void Start () {
		move = false;
		missText = GameObject.FindWithTag ("BadGoodPerfect");
	}

	// Update is called once per frame
	void Update () {
		if (move) {
			transform.position -= transform.up * Time.deltaTime * SpawnNote.speed;
			if (transform.position.y < -5.0f) {
				Debug.Log ("Miss");
				missText.GetComponent<Text> ().text = "Miss";
				Destroy (gameObject);
			}
		}
	}
}
