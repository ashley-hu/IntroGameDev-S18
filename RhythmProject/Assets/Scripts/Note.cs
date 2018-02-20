using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour {

	public bool move;
	private GameObject missText;
	private GameObject healthSlider;

	// Use this for initialization
	void Start () {
		move = false;
		missText = GameObject.FindWithTag ("BadGoodPerfect");
		healthSlider = GameObject.FindWithTag ("PlayerHealthBar");
	}

	// Update is called once per frame
	void Update () {
		if (move) {
			transform.position -= transform.up * Time.deltaTime * SpawnNote.speed;
			if (transform.position.y < -5.0f) {
				Debug.Log ("Miss");
				GameManager.combo = 0;
				missText.GetComponent<Text> ().text = "Miss";
				GameManager.playerCurrHealth -= SpawnNote.bossDamage;
				if (healthSlider.GetComponent<Slider> ().value > 0) {
					healthSlider.GetComponent<Slider> ().value -= SpawnNote.bossDamage;
				} else {
					SpawnNote.songSource.Stop ();
					SpawnNote.endOfSong = true;
				}
				Destroy (gameObject);
			}
		}
	}
}
