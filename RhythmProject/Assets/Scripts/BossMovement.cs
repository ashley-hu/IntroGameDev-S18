using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Move the boss left and right across the screen
 * */
public class BossMovement : MonoBehaviour {

	RectTransform bossImage;
	float reverse;

	// Use this for initialization
	void Start () {
		bossImage = gameObject.GetComponent<RectTransform> ();
		bossImage.localPosition = new Vector3 (0, 110, 0);
		reverse = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.bossCurrHealth > 0) {
			bossImage.position += Vector3.right * Time.deltaTime * 5 * reverse;
			if (bossImage.localPosition.x > 30) {
				reverse = -1;
			} else if (bossImage.localPosition.x < -30) {
				reverse = 1;
			}
		}
	}
}
