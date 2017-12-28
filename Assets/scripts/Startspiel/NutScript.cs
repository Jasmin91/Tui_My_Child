using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  
///  Diese Klasse steuert die  Funktion einer Nuss
/// </summary> 


public class NutScript : MonoBehaviour
{
    /// <summary>  
    ///  Instanz der Manager-Klasse
    /// </summary>
    private ManagerKlasse Manager;

    /// <summary>  
    ///  Speichert, ob Nuss bereits gesammelt wurde
    /// </summary>
    private bool collected = false;

    /// <summary>  
    ///  Speichert den Namen der Nuss
    /// </summary>
    private String namenut="noname";



    void Start()
    {
        this.Manager = ManagerKlasse.Instance;
        this.namenut = this.gameObject.name;
        this.Manager.AddNut(this);
        
    }

    void Update()
    {
    }


    /// <summary>  
    ///  Collider, der erkennt ob Tier und Nuss kollidieren
    /// </summary>
    void OnTriggerEnter2D(Collider2D col)
    {
        {
            this.collectNut();
        }
    }

    public void collectNut()
    {
        Debug.Log(namenut + " soll gelöscht werden");
        Destroy(this.gameObject); //Zerstören des gesammelten Nuss-Objekts
        Manager.CollectNut(); //Zähler der gesammelten Nüsse im Manager wird erhöht
        Manager.RemoveNut(this);
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
