using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  
///  Diese Klasse steuert die  Funktion einer Nuss
/// </summary> 


public class Game : MonoBehaviour
{
    
    private ManagerKlasse Manager; //Erstellt eine Instanz der Manager-Klasse
    private bool updated = false;



    void Start()
    {
        this.Manager = ManagerKlasse.Instance;
       
        
    }

    void Update()
    {
        if (!updated)
        {
            Debug.Log("und ich versuche zu updaten");
            if (Manager.nutsComplete())
            {
                Manager.getOldState();
                updated = true;
            }
        }
    }
    

}
