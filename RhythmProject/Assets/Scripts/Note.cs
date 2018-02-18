using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {

	public bool move;

	// Use this for initialization
	void Start () {
		move = false;
	}

	// Update is called once per frame
	void Update () {
		if (move) {
			transform.position -= transform.up * Time.deltaTime * SpawnNote.speed;
			if (transform.position.y < -4.5f) {
				Debug.Log ("Miss");
				Destroy (gameObject);
			}
		}
	}
}
