using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  
///  Diese Klasse steuert die  Funktion einer Nuss
/// </summary> 

public class NutScript : MonoBehaviour
{
    
    private ManagerKlasse Manager; //Erstellt eine Instanz der Manager-Klasse
    private bool collected = false;



    void Start()
    {
        this.Manager = ManagerKlasse.Instance;
        this.Manager.addNut(this);
        
    }

    void Update()
    {
    }

    //Collider, der erkennt ob Tier und Nuss kollidieren
    void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.gameObject.name == "hase")
        {
            Destroy(this.gameObject); //Zerstören des gesammelten Nuss-Objekts
            Manager.collectNut(); //Zähler der gesammelten Nüsse im Manager wird erhöht
            Manager.removeNut(this);
        }
    }

}
