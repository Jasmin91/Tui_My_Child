using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fiducial_Counter : MonoBehaviour {

	public static int count = 1;
	public int fiducialId = 0;
	public static float time;

	// Use this for initialization
	void Start () {
	
		time = Time.time;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - time > 5){
		bool done = true;
		for(int i = 0; i < 4 ;i++){
			done &= FiducialController_Ruebe.ScreenArray [i] >= 3f;	
		}

		if (done) {

			time = Time.time;
			// ziehe die Rübe aus der Mitte
			GameObject carrot = GameObject.Find ("Ruebe" + count);
			carrot.active = false; 
			count++;

			for (int i = 0; i < 4; i++) {
				FiducialController_Ruebe.ScreenArray [i] = 0;
			}
		}
	}
	}
}
