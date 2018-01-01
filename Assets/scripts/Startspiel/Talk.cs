using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>  
///  Diese Klasse steuert die Ausgabe des Scores
/// </summary> 


public class Talk : MonoBehaviour
{
    /// <summary>  
    ///  Instanz der Manager-Klasse
    /// </summary>
    private ManagerKlasse Manager;

  

    /// <summary> Text der ausgegeben werden soll </summary>
    public Text Ausgabe;



    void Start()
    {
        this.Manager = ManagerKlasse.Instance;
        
        
    }

    void Update()
    {
        
    }

    
    public void setPositionText(Vector3 pos)
    {
        Ausgabe.transform.position = pos;
    }

    public void setPosition(Vector3 pos)
    {
        transform.position = pos;
    }
}
