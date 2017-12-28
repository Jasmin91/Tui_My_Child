using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  
///  Diese Klasse steuert die  Funktion einer Nuss
/// </summary> 


public class HutScript : MonoBehaviour
{
    
    private ManagerKlasse Manager; //Erstellt eine Instanz der Manager-Klasse
    private bool finished = false;



    void Start()
    {
        this.Manager = ManagerKlasse.Instance;
       
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Manager.visitHut(col.name);

        if (Manager.getVistors().Count == Manager.PlayerCount) {
            if (Manager.getFoundAllFood()) {
                finished = true;
                Debug.Log("Spiel beendet!");
            }
            else
            {
                Debug.Log("Wir haben noch nicht genug zu essen!");
            }
        }
        else
        {
            Debug.Log(col.name + " wartet auf seine Freunde"+Manager.getVistors().Count);
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        Manager.leaveHut(col.name);
    }

    void Update()
    {

    }
}
