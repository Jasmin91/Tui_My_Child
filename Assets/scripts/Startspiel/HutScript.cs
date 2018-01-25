using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>  
///  Diese Klasse steuert die Funktionen der Hütte und erkennt das Ende des Spiels
/// </summary> 


public class HutScript : MonoBehaviour
{
    /// <summary>
    ///Erstellt eine Instanz der Manager-Klasse
    /// </summary>
    private ManagerKlasse Manager;

    /// <summary>
    ///Spiel beendet 
    /// </summary>
    private bool finished = false; 

    /// <summary>
    /// Geschwindigkeit, mit der Ballon fliegen soll
    /// </summary>
    public float BaloonSpeed = -0.01f;

    /// <summary>
    /// Zugehöriges Ballon-Objekt
    /// </summary>
    public GameObject baloon;

    /// <summary>
    /// Wartezeit, bevor Hütte sich öffnet
    /// </summary>
    public float Countdown = 1.5f;

    /// <summary>
    /// Sound, wenn sich Tür öffnet
    /// </summary>
    public AudioSource DoorSound;

    /// <summary>
    /// Besucherzähler
    /// </summary>
    int VisitorCounter = 0;


    void Start()
    {
        VisitorCounter=0;
        this.Manager = ManagerKlasse.Instance;
        baloon.GetComponent<Renderer>().enabled = false;
    }
    void Update()
    {

        Debug.Log("Besucher:" + VisitorCounter);
        if(Manager.GetFoundAllFood()){

            Countdown -= Time.deltaTime;
            
            if (Countdown <= 0.0f)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("hut_open", typeof(Sprite)) as Sprite;
                DoorSound.Play();
                baloon.GetComponent<Renderer>().enabled = true;
                baloon.GetComponent<Rigidbody2D>().gravityScale = BaloonSpeed;
            }
            
        }
    }

    /// <summary>  
    ///  Erkennt die Kollision eines Tieres mit der Hütte
    /// </summary> 
    void OnTriggerEnter2D(Collider2D col)
    {
        //Manager.VisitHut(col.name); //Merkt sich im Manager, dass Tier gerade auf Hütte ist
        AnimalController animal = Manager.GetAnimalByName(col.name);
        VisitorCounter++;
        if (col.name == "horse" && col.name == "dog") ;

        if (VisitorCounter == Manager.PlayerCount) { //Schaut, ob alle Tiere auf der Hütte sind
            if (Manager.GetFoundAllFood()) { //Schaut, ob alles Essen gesammelt wurde
                finished = true; //Spiel ist fertig
                Manager.Reset(); //Manger wird resettet
                SceneManager.LoadScene("Closing"); //Endszene wird geladen
               
            }
            else
            {
                String s = "Wir haben noch nicht genug zu essen!";
                Manager.LetAnimalSaySomething(col.name, s, 4); //Lässt kollidierendes Tier 4 Sek etwas sagen
            }
        }
        else
        {
            String s = "";
            if (animal.GetHasFood())
            {
                s += "Ich warte noch auf meine Freunde!";
                animal.Speak(s, 4); //Lässt kollidierendes Tier 4 Sek etwas sagen
            }
            else
            {
                s += "Ich habe noch nichts zu essen!";
                animal.Speak(s); //Lässt kollidierendes Tier etwas sagen, bis es die Hütte wieder verlässt
            }
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
       // Manager.LeaveHut(col.name); //Merkt sich im Manager, dass Tier Hütte verlassen hat
        Manager.LetAnimalBeQuiet(col.name);
        VisitorCounter--;
    }
    
}
