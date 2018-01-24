using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {


	//Soundkomponenten
	public AudioSource WinSound;
//	public AudioSource PlingSound;


	// Use this for initialization
	void Start () {
		WinSound = GetComponent<AudioSource>();
	//	PlingSound = GameObject.Find ("Musik").GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {

		if (Fiducial_Counter.play == true) {
			WinSound.Play ();
		}

	/*	if (Fiducial_Counter.playPling == true) {
			PlingSound.Play ();
		} */

	}
}
