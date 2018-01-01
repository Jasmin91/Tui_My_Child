using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  
///  Diese Klasse steuert das Upaten des Startspiels nach einem Minispiel
/// </summary> 


public class Game : MonoBehaviour
{
    
    private ManagerKlasse Manager; //Erstellt eine Instanz der Manager-Klasse
    private bool updated = false; //Wurde bereits geupdated



    void Start()
    {
        this.Manager = ManagerKlasse.Instance;
    }

    /// <summary>  
    ///  Die Methode leitet das Updaten ein, wenn das Startspiel gestartet wurde
    /// </summary> 
    void Update()
    {

        Manager.LoadingComplete();
        if (!updated)
        {
            Debug.Log("Versucht zu updaten");
            if (Manager.LoadingComplete()) //Checkt, ob das Spiel komplett geladen wurde
            {
                Manager.GetOldState(); //versucht den alten Stand zu laden
                updated = true;
            }

        }
    }
    
}
