using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>  
///  Diese Klasse steuert die  Funktion einer Nuss
/// </summary> 


public class Text : MonoBehaviour
{
    
    private ManagerKlasse Manager; //Erstellt eine Instanz der Manager-Klasse
    public Text txt;


    void Start()
    {
        this.Manager = ManagerKlasse.Instance;
        //GameObject.text
       
    }

    void Update()
    {
       // txt.GetComponent<UnityEngine.UI.Text>().text = "TEST";
    }
    
}
