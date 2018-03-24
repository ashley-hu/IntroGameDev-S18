using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpit : MonoBehaviour {

	public Animator anim;
	public static bool setFire;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		setFire = false;
		//anim.SetBool ("spitFire", setFire);
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool ("spitFire", setFire);
		//Debug.Log ("setFire "+ setFire);
	}
}
