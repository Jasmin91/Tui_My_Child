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



    void Start()
    {
        this.Manager = ManagerKlasse.Instance;
    }

    /// <summary>  
    ///  Erkennt die Kollision eines Tieres mit der Hütte
    /// </summary> 
    void OnTriggerEnter2D(Collider2D col)
    {
        Manager.VisitHut(col.name); //Merkt sich im Manager, dass Tier gerade auf Hütte ist
      

        if (Manager.GetVistors().Count == Manager.PlayerCount) { //Schaut, ob alle Tiere auf der Hütte sind
            if (Manager.GetFoundAllFood()) { //Schaut, ob alles Essen gesammelt wurde
                finished = true;
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("hut_open", typeof(Sprite)) as Sprite;
                Manager.HideAnimals();
                Debug.Log("Spiel beendet!");
            }
            else
            {
                Debug.Log("Wir haben noch nicht genug zu essen!");
            }
        }
        else
        {
            Debug.Log(col.name + " wartet auf seine Freunde"+Manager.GetVistors().Count);
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        Manager.LeaveHut(col.name); //Merkt sich im Manager, dass Tier Hütte verlassen hat
    }

    void Update()
    {

    }
}
