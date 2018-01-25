using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen_1 : MonoBehaviour {

	[SerializeField]
	private Image fill1;

	private float fillAmount;

	// Use this for initialization
	void Start () {
		UpdateBar ();
	}

	// Update is called once per frame
	void Update () {
		//fill1.fillAmount füllt den Pfeil, der mit der Fiducial ID 1 kontrolliert wird
		fill1.fillAmount = RuebenController.ScreenArray [1]/2;
	}

	//Methode für das Leeren der Pfeile zu Beginn des Spiels Start()
	private void UpdateBar(){
		fill1.fillAmount = 0f;
	}
}
