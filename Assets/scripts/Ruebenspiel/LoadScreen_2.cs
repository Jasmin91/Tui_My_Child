using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen_2 : MonoBehaviour {

	[SerializeField]
	private Image fill2;

	private float fillAmount;

	// Use this for initialization
	void Start () {
		UpdateBar ();
	}

	// Update is called once per frame
	void Update () {
		//fill2.fillAmount füllt den Pfeil, der mit der Fiducial ID 2 kontrolliert wird
		fill2.fillAmount = RuebenController.ScreenArray [2]/2;
	}

	//Methode für das Leeren der Pfeile zu Beginn des Spiels Start()
	private void UpdateBar(){
		fill2.fillAmount = 0f;
	}
}
