using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//Sound: Music By http://instrumentalsfree.com

public class Fiducial_Counter : MonoBehaviour
{

    public static int count = 1;
    public static float time;

    //Soundkomponenten
    public AudioSource WinSound;
    public AudioSource Pling;
    bool play = false;
    bool playPling = false;

    //Variable für das Ende des Spiels
    bool finished = false;

    // Use this for initialization
    void Start()
    {
        time = Time.time;
        //Setzt die Szene zurück, wenn die erste, zweite oder dritte Runde schon fertig war und abgebrochen wurde
        count = 1;
        ResetScreenArray();
        play = false;
        playPling = false;

        //Sucht die richtigen Sounds raus
        WinSound = GameObject.Find("ArrowCanvas").GetComponent<AudioSource>();
        Pling = GameObject.Find("EndMusik").GetComponent<AudioSource>();
    }

    //Methode für das Zurücksetzen der Füllstände in der ersten Runde, wenn count = 1
    void ResetScreenArray()
    {
        for (int i = 0; i < 4; i++)
        {
            RuebenController.ScreenArray[i] = 0f;
        }
    }

    //Warten nach dem Abspielen des WinSounds
    IEnumerator Warten()
    {
        yield return new WaitForSeconds(1.5f);
        //Laden des Startspiels
        SceneManager.LoadScene("Startspiel");
    }

    // Update is called once per frame
    void Update()
    {
        //Logikvorbereitung für das eingefärbt lassen des Kreises dessen Pfeil schon voll ist

        //Finden aller Kreisobjekte
        /*	GameObject circle1 = GameObject.Find ("Kreis1_1");
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

		//GamebObject-String mit allen Kreisen
		GameObject[] circle = new GameObject[] {
			circle1, circle2, circle3, circle4, circle5, circle6, circle7, circle8, circle9,
			circle10, circle11, circle12, circle13, circle14, circle15, circle16
		}; 

		//Gameobject-Strings (folgende vier) mit den Kreisen einer jeweiligen Seite
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
		}; */

        //Wartezeit nachdem eine Rübe in der Mitte gezogen wurde
        if (Time.time - time > 2)
        {

            //Boolean zur Prüfung, ob alle Pfeile voll sind
            bool done = true;
            for (int i = 0; i < 4; i++)
            {
                // i steht für die IDs der Player (ID 0 - 3), 
                // jede Seite hat eine ID, ist der Pfeil auf der jeiligen Seite voll (>= 2f)
                // so ist die Runde "done" und die Rübe kann gezogen werden
                done &= RuebenController.ScreenArray[i] >= 2f;

                //Fortsetzung der Logik zum einfärben der Kreise (s. Z. 57)
                /*				if (RuebenController.ScreenArray [0] >= 2f) {
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
                                } */
            }

            //Wenn alle Pfeile voll sind..
            if (done)
            {

                time = Time.time;
                playPling = true;
                //finde die Rübe der jeweiligen Runde (zu Beginn: count=1)
                GameObject carrot = GameObject.Find("Ruebe" + count);


                //Sucht die Rüben
                GameObject carrot1 = GameObject.Find("Ruebe1");
                GameObject carrot2 = GameObject.Find("Ruebe2");
                GameObject carrot3 = GameObject.Find("Ruebe3");
                GameObject carrot4 = GameObject.Find("Ruebe4");


                //ziehe die Rübe aus der Mitte
                carrot.GetComponent<Renderer>().enabled = false;

                //Spielt sind Pling-Ton, wenn Rübe gezogen ist
                if (playPling == true && !carrot.GetComponent<Renderer>().enabled)
                {
                    Pling.Play();
                }

                //Speichert die gesuchten Rüben in Variablen
                if (!carrot.GetComponent<Renderer>().enabled && count == 1)
                {
                    carrot1 = GameObject.Find("Ruebe1");
                }
                if (!carrot.GetComponent<Renderer>().enabled && count == 2)
                {
                    carrot2 = GameObject.Find("Ruebe2");
                }
                if (!carrot.GetComponent<Renderer>().enabled && count == 3)
                {
                    carrot3 = GameObject.Find("Ruebe3");
                }
                if (!carrot.GetComponent<Renderer>().enabled && count == 4)
                {
                    carrot4 = GameObject.Find("Ruebe4");
                }


                //Damit die gespeicherten Variablen hier geprüft werden könnnen
                //Ist die vierte Runde beendet und alle vier Rüben gezogen (enabled), dann ist das Spiel fertig
                if (count >= 4 && !carrot1.GetComponent<Renderer>().enabled && !carrot2.GetComponent<Renderer>().enabled && !carrot3.GetComponent<Renderer>().enabled && !carrot4.GetComponent<Renderer>().enabled)
                {
                    //WinSound kann abgespielt werden
                    WinSound.Play();
                    //Variable für das Spielende wird auf Wahr
                    finished = true;
                }

                //Damit der WinSound noch richtig abgespielt werden kann, wird mit dem IEnumerator eine Pause eingelegt
                //Anschließend kommt man zurück ins Startspiel
                if (finished == true)
                {
                    //Methodenaufruf für IEnumerator
                    StartCoroutine("Warten");
                }

                //Inkrement für die nächste Runde
                count++;

                /*Verhindert das alle Rüben nacheinander gezogen werden
					Setzt den Füllstand nach jeder Runde zurück*/
                for (int i = 0; i < 4; i++)
                {
                    RuebenController.ScreenArray[i] = 0;

                }
                //Variable muss zurück gesetzt werden, damit der Ton nach jeder gezogenen Rübe kommt
                playPling = false;
            }
        }
    }
}