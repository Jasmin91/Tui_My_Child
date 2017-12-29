using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>  
///  Diese Klasse steuert die Ausgabe des Scores
/// </summary> 


public class Konsole : MonoBehaviour
{
    /// <summary>  
    ///  Instanz der Manager-Klasse
    /// </summary>
    private ManagerKlasse Manager;

  

    /// <summary> Zeigt Text an </summary>
    public Text Ausgabe;



    void Start()
    {
        this.Manager = ManagerKlasse.Instance;
        this.Manager.setConsole(this);
        
        
    }

    void Update()
    {
    }

    
    public void AddText(string txt)
    {
        this.Ausgabe.text = this.Ausgabe.text +"\n"+ txt;
    }

}
