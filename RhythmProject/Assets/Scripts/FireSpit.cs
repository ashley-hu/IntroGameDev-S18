using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpit : MonoBehaviour {

	public Animator anim;
	public static bool setFire;
	private bool isMoving;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		setFire = true;
		isMoving = false;
		//anim.SetBool ("spitFire", setFire);
	}
	
	// Update is called once per frame
	void Update () {
		if (!isMoving) {
			anim.SetBool ("spitFire", setFire);
		}
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("spitFire"))	{
			isMoving = true;
		}
		//anim.SetBool ("spitFire", setFire);
		//Debug.Log ("setFire "+ setFire);
	}
}
