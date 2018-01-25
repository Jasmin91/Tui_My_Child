using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen_3 : MonoBehaviour {

	[SerializeField]
	private Image fill3;

	private float fillAmount;

	// Use this for initialization
	void Start () {
		UpdateBar ();
	}

	// Update is called once per frame
	void Update () {
		//fill3.fillAmount füllt den Pfeil, der mit der Fiducial ID 3 kontrolliert wird
		fill3.fillAmount = RuebenController.ScreenArray [3]/2;
	}

	//Methode für das Leeren der Pfeile zu Beginn des Spiels Start()
	private void UpdateBar(){
		fill3.fillAmount = 0f;
	}
}
