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
    private String namenut="noname";



    void Start()
    {
        this.Manager = ManagerKlasse.Instance;
        this.namenut = this.gameObject.name;
        this.Manager.addNut(this);
        //Debug.Log("Eine neue Nuss ist geboren und ich heiße " + namenut);
        
    }

    void Update()
    {
    }

    //Collider, der erkennt ob Tier und Nuss kollidieren
    void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.gameObject.name == "hase")
        {
            this.collectNut();
        }
    }

    public void collectNut()
    {
        Debug.Log(namenut + " soll gelöscht werden");
        Destroy(this.gameObject); //Zerstören des gesammelten Nuss-Objekts
        Manager.collectNut(); //Zähler der gesammelten Nüsse im Manager wird erhöht
      
            Manager.removeNut(this);
        
    }

    public void destroyObject()
    {
        Destroy(this.gameObject); //Zerstören des gesammelten Nuss-Objekts
    }
    public String getName()
    {
        return this.namenut;
    }

}
