using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>  
///  Diese Klasse steuert die Ausgabe des Scores
/// </summary> 


public class ScoreScript : MonoBehaviour
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
        
        
    }

    void Update()
    {
        this.ShowScore(this.Manager.NutCounter+"");
    }

    
    public void ShowScore(string txt)
    {
        this.Ausgabe.text = "Nüsse: " + txt;
    }

}
