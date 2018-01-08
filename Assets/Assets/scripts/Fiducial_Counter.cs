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
			//carrot.active = false; 
            carrot.GetComponent<Renderer>().enabled = false;
			count++;

			for (int i = 0; i < 4; i++) {
					RuebenController.ScreenArray [i] = 0;
			}
            }}

             int fertig = 0;
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
               
               
                }
                

		      }
        
}
