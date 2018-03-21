using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpit : MonoBehaviour {

	public Animator anim;
	public bool setFire;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		setFire = false;
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool ("spitFire", setFire);
	}
}
