using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    


    /// <summary>  
    ///  Collider, der erkennt ob Tier und Nuss kollidieren
    /// </summary>
    void OnTriggerEnter2D(Collider2D col)
    {
        {
            this.CollectNut();
        }
    }

    /// <summary>
    /// Sammelt Nuss: Löscht Nuss-Objekt und erhöht Nusszähler
    /// </summary>
    public void CollectNut()
    {
        Destroy(this.gameObject); //Zerstören des gesammelten Nuss-Objekts
        Manager.CollectNut(); //Zähler der gesammelten Nüsse im Manager wird erhöht
        Manager.RemoveNut(this);
    }

    /// <summary>
    /// Löscht das Nuss-Objekt
    /// </summary>
    public void DestroyObject()
    {
        Destroy(this.gameObject); //Zerstören des gesammelten Nuss-Objekts
    }

    /// <summary>
    /// Getter für den Namen der Nuss
    /// </summary>
    /// <returns>Nussname</returns>
    public String getName()
    {
        return this.namenut;
    }
    

}
