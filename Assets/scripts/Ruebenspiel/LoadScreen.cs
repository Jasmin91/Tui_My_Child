using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour {

	[SerializeField]
	private Image fill;

	private float fillAmount;

	// Use this for initialization
	void Start () {
		UpdateBar ();
	}
	
	// Update is called once per frame
	void Update () {
		//fill.fillAmount füllt den Pfeil, der mit der Fiducial ID 0 kontrolliert wird
		//das wird hier durch 2 geteilt, damit die Pfeile nicht sofort gefüllt sind
		//macht man an dieser Stelle eine Anpassung, so muss man dies auch im Fiducial_Counter anpassen "done etc. >= 2f"
		fill.fillAmount = RuebenController.ScreenArray [0]/2;
	}

	//Methode für das Leeren der Pfeile zu Beginn des Spiels Start()
	private void UpdateBar(){
		fill.fillAmount = 0f;
	}
}
	