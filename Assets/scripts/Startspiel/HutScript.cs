using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  
///  Diese Klasse steuert die Funktionen der Hütte und erkennt das Ende des Spiels
/// </summary> 


public class HutScript : MonoBehaviour
{
    
    private ManagerKlasse Manager; //Erstellt eine Instanz der Manager-Klasse
    private bool finished = false; //Spiel beendet
    public float BaloonSpeed = -0.01f;
    public GameObject baloon;




    void Start()
    {
        this.Manager = ManagerKlasse.Instance;
        baloon.GetComponent<Renderer>().enabled = false;
    }

    /// <summary>  
    ///  Erkennt die Kollision eines Tieres mit der Hütte
    /// </summary> 
    void OnTriggerEnter2D(Collider2D col)
    {
        Manager.VisitHut(col.name); //Merkt sich im Manager, dass Tier gerade auf Hütte ist
        AnimalController animal = Manager.getAnimalByName(col.name);

        if (Manager.GetVistors().Count == Manager.PlayerCount) { //Schaut, ob alle Tiere auf der Hütte sind
            if (Manager.GetFoundAllFood()) { //Schaut, ob alles Essen gesammelt wurde
                finished = true;
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("hut_open", typeof(Sprite)) as Sprite;
                Manager.HideAnimals();
                baloon.GetComponent<Renderer>().enabled = true;
                baloon.GetComponent<Rigidbody2D>().gravityScale = BaloonSpeed;
                Debug.Log("Spiel beendet!");
            }
            else
            {
                String s = "Wir haben noch nicht genug zu essen!";
                Debug.Log(s);
                Manager.LetAnimalSaySomething(col.name, s, 2);
            }
        }
        else
        {
            String s = "Ich warte noch auf meine Freunde!";
            Debug.Log(col.name + " wartet auf seine Freunde"+Manager.GetVistors().Count);
            Manager.LetAnimalSaySomething(col.name, s, 2);
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        Manager.LeaveHut(col.name); //Merkt sich im Manager, dass Tier Hütte verlassen hat
        Manager.LetAnimalBeQuiet(col.name);
    }

    void Update()
    {

    }
}
