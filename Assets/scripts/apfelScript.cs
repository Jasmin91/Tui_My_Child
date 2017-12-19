using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  
///  Diese Klasse steuert die  Funktion eines (roten) Apfels
/// </summary> 

public class apfelScript : MonoBehaviour
{
    
    private Management ms_Instance; //Erstellt eine Instanz der Manager-Klasse
    private bool appleVisible = false; //Boolscher Wert, ob der Apfel sichtbar ist
    public float fallingSpeed = 0.23f; //Geschwindigkeit, mit der der Apfel fällt
    private bool fallen = false; //Boolscher Wert, ob Apfel bereits gefallen ist



    void Start()
    {
        this.ms_Instance = Management.Instance; 
        this.ms_Instance.addApple(this); //Füge Apfel dem Apfel-Array im Manager hinzu
        this.HideApple(); //Roter Apfel ist zu Beginn ausgeblendet
        
    }

    void Update()
    {
        if (this.ms_Instance.getHarvestingReady()) //Testen, ob geerntet werden kann
        {
            this.ShowApple(); //Apfel wird angezeigt
        }
        
    }

    public void SetFallen(bool fallen)
    {
        this.fallen = fallen;
    }

    public bool GetFallen()
    {
        return this.fallen;
    }

    void ShowApple()
    {
        gameObject.GetComponent<Renderer>().enabled = true; //Apfel-Objekt anzeigen
        appleVisible = true; 
    }

    void HideApple()
    {
        gameObject.GetComponent<Renderer>().enabled = false; //Apfel-Objekt ausblenden
        appleVisible = false;

    }

    public void FallingApple()
    {
            if (this.appleVisible) { 
            gameObject.GetComponent<Rigidbody2D>().gravityScale = this.fallingSpeed; //Schwerkraft auf Fallgeschwindigkeit setzen
            this.SetFallen(true);
        }

    }
}
