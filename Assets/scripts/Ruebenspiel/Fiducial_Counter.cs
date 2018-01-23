using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//Sound: Music By http://instrumentalsfree.com

public class Fiducial_Counter : MonoBehaviour {

	public static int count = 1;
	//public int fiducialId = 0;
	public static float time;


	//public AudioSource sound;


	// Use this for initialization
	void Start () {
		time = Time.time;
		count = 1;
		ResetScreenArray ();
	}

	void ResetScreenArray(){
		for (int i = 0; i < 4; i++) {
			RuebenController.ScreenArray [i] = 0f;
		}
	}
	
	// Update is called once per frame
	void Update () {

		GameObject circle1 = GameObject.Find ("Kreis1_1");
		GameObject circle2 = GameObject.Find ("Kreis2_1");
		GameObject circle3 = GameObject.Find ("Kreis3_1");
		GameObject circle4 = GameObject.Find ("Kreis4_1");
		GameObject circle5 = GameObject.Find ("Kreis1_2");
		GameObject circle6 = GameObject.Find ("Kreis2_2");
		GameObject circle7 = GameObject.Find ("Kreis3_2");
		GameObject circle8 = GameObject.Find ("Kreis4_2");
		GameObject circle9 = GameObject.Find ("Kreis1_3");
		GameObject circle10 = GameObject.Find ("Kreis2_3");
		GameObject circle11 = GameObject.Find ("Kreis3_3");
		GameObject circle12 = GameObject.Find ("Kreis4_3");
		GameObject circle13 = GameObject.Find ("Kreis1_4");
		GameObject circle14 = GameObject.Find ("Kreis2_4");
		GameObject circle15 = GameObject.Find ("Kreis3_4");
		GameObject circle16 = GameObject.Find ("Kreis4_4");

	/*	GameObject[] circle = new GameObject[] {
			circle1, circle2, circle3, circle4, circle5, circle6, circle7, circle8, circle9,
			circle10, circle11, circle12, circle13, circle14, circle15, circle16
		}; */


		GameObject[] kreis = new GameObject[]{ 
			circle1, circle2, circle3, circle4
		};

		GameObject[] kreis1 = new GameObject [] {
			circle5, circle6, circle7, circle8	
		};

		GameObject[] kreis2 = new GameObject[] { 
			circle9, circle10, circle11, circle12
		};

		GameObject[] kreis3 = new GameObject[]{
			circle13, circle14, circle15, circle16
		}; 

		if(Time.time - time > 2){
		bool done = true;
		for(int i = 0; i < 4 ;i++){
				done &= RuebenController.ScreenArray [i] >= 2f;

				if (RuebenController.ScreenArray [0] >= 2f) {
					for (int x = 1; x <= count; x++){
						SpriteRenderer renderer = kreis [x-1].GetComponent<SpriteRenderer> ();
						renderer.color = new Color (0.133f, 0.545f, 0.133f);
						continue;
					} 
				}

				if (RuebenController.ScreenArray [1] >= 2f) {
					for (int x = 1; x <= count; x++){
						SpriteRenderer renderer = kreis1 [x-1].GetComponent<SpriteRenderer> ();
						renderer.color = new Color (0.133f, 0.545f, 0.133f);
						continue;
					} 
				}

				if (RuebenController.ScreenArray [2] >= 2f) {
					for (int x = 1; x <= count; x++){
						SpriteRenderer renderer = kreis2 [x-1].GetComponent<SpriteRenderer> ();
						renderer.color = new Color (0.133f, 0.545f, 0.133f);
						continue;
					} 
				}

				if (RuebenController.ScreenArray [3] >= 2f) {
					for (int x = 1; x <= count; x++){
						SpriteRenderer renderer = kreis3 [x-1].GetComponent<SpriteRenderer> ();
						renderer.color = new Color (0.133f, 0.545f, 0.133f);
						continue;
					} 
				} 	


		/*			for (int x = 1; x <= count; x++) {
					if (RuebenController.ScreenArray [0] >= 2f) {
						SpriteRenderer renderer = kreis [x-1].GetComponent<SpriteRenderer> ();
						renderer.color = new Color (0.133f, 0.545f, 0.133f);
						continue;	
					}

					if (RuebenController.ScreenArray [1] >= 2f) {
						SpriteRenderer renderer = kreis1 [x-1].GetComponent<SpriteRenderer> ();
						renderer.color = new Color (0.133f, 0.545f, 0.133f);
						continue;	
					}
					if (RuebenController.ScreenArray [2] >= 2f) {
						SpriteRenderer renderer = kreis2 [x-1].GetComponent<SpriteRenderer> ();
						renderer.color = new Color (0.133f, 0.545f, 0.133f);
						continue;	
					}
					if (RuebenController.ScreenArray [3] >= 2f) {
						SpriteRenderer renderer = kreis3 [x-1].GetComponent<SpriteRenderer> ();
						renderer.color = new Color (0.133f, 0.545f, 0.133f);
						continue;	
					}

				} */

/*				if (RuebenController.ScreenArray [0] >= 2f && count == 1) {
					SpriteRenderer renderer = circle [0].GetComponent<SpriteRenderer> ();
					renderer.color = new Color (0.133f, 0.545f, 0.133f);
				}
				if (RuebenController.ScreenArray [1] >= 2f && count == 1) {
					SpriteRenderer renderer = circle [4].GetComponent<SpriteRenderer> ();
					renderer.color = new Color (0.133f, 0.545f, 0.133f);
				}
				if (RuebenController.ScreenArray [2] >= 2f && count == 1) {
					SpriteRenderer renderer = circle [8].GetComponent<SpriteRenderer> ();
					renderer.color = new Color (0.133f, 0.545f, 0.133f);
				}
				if (RuebenController.ScreenArray [3] >= 2f && count == 1) {
					SpriteRenderer renderer = circle [12].GetComponent<SpriteRenderer> ();
					renderer.color = new Color (0.133f, 0.545f, 0.133f);
				}
				if (RuebenController.ScreenArray [0] >= 2f && count == 2) {
					SpriteRenderer renderer = circle [1].GetComponent<SpriteRenderer> ();
					renderer.color = new Color (0.133f, 0.545f, 0.133f);
				}
				if (RuebenController.ScreenArray [1] >= 2f && count == 2) {
					SpriteRenderer renderer = circle [5].GetComponent<SpriteRenderer> ();
					renderer.color = new Color (0.133f, 0.545f, 0.133f);
				}
				if (RuebenController.ScreenArray [2] >= 2f && count == 2) {
					SpriteRenderer renderer = circle [9].GetComponent<SpriteRenderer> ();
					renderer.color = new Color (0.133f, 0.545f, 0.133f);
				}
				if (RuebenController.ScreenArray [3] >= 2f && count == 2) {
					SpriteRenderer renderer = circle [13].GetComponent<SpriteRenderer> ();
					renderer.color = new Color (0.133f, 0.545f, 0.133f);
				}
				if (RuebenController.ScreenArray [0] >= 2f && count == 3) {
					SpriteRenderer renderer = circle [2].GetComponent<SpriteRenderer> ();
					renderer.color = new Color (0.133f, 0.545f, 0.133f);
				}
				if (RuebenController.ScreenArray [1] >= 2f && count == 3) {
					SpriteRenderer renderer = circle [6].GetComponent<SpriteRenderer> ();
					renderer.color = new Color (0.133f, 0.545f, 0.133f);
				}
				if (RuebenController.ScreenArray [2] >= 2f && count == 3) {
					SpriteRenderer renderer = circle [10].GetComponent<SpriteRenderer> ();
					renderer.color = new Color (0.133f, 0.545f, 0.133f);
				}
				if (RuebenController.ScreenArray [3] >= 2f && count == 3) {
					SpriteRenderer renderer = circle [14].GetComponent<SpriteRenderer> ();
					renderer.color = new Color (0.133f, 0.545f, 0.133f);
				}
				if (RuebenController.ScreenArray [0] >= 2f && count == 4) {
					SpriteRenderer renderer = circle [3].GetComponent<SpriteRenderer> ();
					renderer.color = new Color (0.133f, 0.545f, 0.133f);
				}
				if (RuebenController.ScreenArray [1] >= 2f && count == 4) {
					SpriteRenderer renderer = circle [7].GetComponent<SpriteRenderer> ();
					renderer.color = new Color (0.133f, 0.545f, 0.133f);
				}
				if (RuebenController.ScreenArray [2] >= 2f && count == 4) {
					SpriteRenderer renderer = circle [11].GetComponent<SpriteRenderer> ();
					renderer.color = new Color (0.133f, 0.545f, 0.133f);
				}
				if (RuebenController.ScreenArray [3] >= 2f && count == 4) {
					SpriteRenderer renderer = circle [15].GetComponent<SpriteRenderer> ();
					renderer.color = new Color (0.133f, 0.545f, 0.133f);
				}  */
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

			
			if (carrot.GetComponent<Renderer> ().enabled == false && count == 1) {
					carrot1 = GameObject.Find ("Ruebe1");
			}
			if (carrot.GetComponent<Renderer> ().enabled == false && count == 2) {
					carrot2 = GameObject.Find ("Ruebe2");
			}
			if (carrot.GetComponent<Renderer> ().enabled == false && count == 3) {
					carrot3 = GameObject.Find ("Ruebe3");
			}
			if (carrot.GetComponent<Renderer> ().enabled == false && count == 4) {
					carrot4 = GameObject.Find ("Ruebe4");
			}


			if(count >= 4 && !carrot1.GetComponent<Renderer>().enabled && !carrot2.GetComponent<Renderer>().enabled && !carrot3.GetComponent<Renderer>().enabled && !carrot4.GetComponent<Renderer>().enabled){
					// lade Start-Bildschirm
				SceneManager.LoadScene("Startspiel");
	
			}
			count++;
//			Debug.Log ("Inkrement" + count);
			for (int i = 0; i < 4; i++) {
					RuebenController.ScreenArray [i] = 0;
			}
          }
		}
	}        
}
