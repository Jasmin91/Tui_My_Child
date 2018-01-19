using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fiducial_Counter : MonoBehaviour {

	public static int count = 1;
	public int fiducialId = 0;
	public static float time;
	Color color;


	// Use this for initialization
	void Start () {
	
		time = Time.time;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - time > 2){
		bool done = true;
		for(int i = 0; i < 4 ;i++){
				done &= RuebenController.ScreenArray [i] >= 3f;	
		}
        

		if (done) {

			time = Time.time;
			// ziehe die Rübe aus der Mitte
			GameObject carrot = GameObject.Find ("Ruebe" + count);

			GameObject carrot1 = GameObject.Find ("Ruebe1");
			GameObject carrot2 = GameObject.Find ("Ruebe2");
			GameObject carrot3 = GameObject.Find ("Ruebe3");
			GameObject carrot4 = GameObject.Find ("Ruebe4");
	
			

			carrot.GetComponent<Renderer>().enabled = false;

			
			if (carrot.GetComponent<Renderer> ().enabled = false && count == 0) {
					carrot1 = GameObject.Find ("Ruebe1");
			}
			if (carrot.GetComponent<Renderer> ().enabled = false && count == 1) {
					carrot2 = GameObject.Find ("Ruebe2");
			}
			if (carrot.GetComponent<Renderer> ().enabled = false && count == 2) {
					carrot3 = GameObject.Find ("Ruebe3");
			}
			if (carrot.GetComponent<Renderer> ().enabled = false && count == 3) {
					carrot4 = GameObject.Find ("Ruebe4");
			}

			if(count >= 4 && !carrot1.GetComponent<Renderer>().enabled && !carrot2.GetComponent<Renderer>().enabled && !carrot3.GetComponent<Renderer>().enabled && !carrot4.GetComponent<Renderer>().enabled){
					// lade Start-Bildschirm
				SceneManager.LoadScene("Startspiel");				
			}
			count++;
			for (int i = 0; i < 4; i++) {
					RuebenController.ScreenArray [i] = 0;
			}
          }
		}
	}        
}
