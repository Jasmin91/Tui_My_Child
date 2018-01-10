using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
				done &= RuebenController.ScreenArray [i] >= 1f;	
		}
        

		if (done) {

			time = Time.time;
			// ziehe die Rübe aus der Mitte
			GameObject carrot = GameObject.Find ("Ruebe" + count);
		//	GameObject carrot = GameObject.Find ("Ruebe1");
		//	GameObject carrot1 = GameObject.Find ("Ruebe2");
		//	GameObject carrot2 = GameObject.Find ("Ruebe3");
		//	GameObject carrot3 = GameObject.Find ("Ruebe4");

			GameObject carrot1 = GameObject.Find ("Ruebe1");
			GameObject carrot2 = GameObject.Find ("Ruebe2");
			GameObject carrot3 = GameObject.Find ("Ruebe3");
			GameObject carrot4 = GameObject.Find ("Ruebe4");
			//carrot.active = false; 
            //carrot.GetComponent<Renderer>().enabled = false;
			carrot.GetComponent<Renderer>().enabled = false;
		//	carrot1.GetComponent<Renderer>().enabled = false;
		//	carrot2.GetComponent<Renderer>().enabled = false;
		//	carrot3.GetComponent<Renderer>().enabled = false;
			
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
			//if(count >= 4 && !GameObject.Find("Ruebe1").activeSelf && !GameObject.Find("Ruebe2").activeSelf && !GameObject.Find("Ruebe3").activeSelf && !GameObject.Find("Ruebe4").activeSelf){
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

	


     /*        int fertig = 0;
             for (int i = 0; i < 4; i++) {
					GameObject carrot2 = GameObject.Find ("Ruebe" + i);
                if(carrot2!=null){
                    
                if(!carrot2.GetComponent<Renderer>().enabled){
                fertig++;
                  }

                     if(fertig==3){
                     Debug.Log("Spiel aus");
                    SceneManager.LoadScene("Startspiel");
                    }
                
                
                Debug.Log("Counter:"+fertig);
                }
             fertig = 0;
               
               
                }*/
                

	}        
}
