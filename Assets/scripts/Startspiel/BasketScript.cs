using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  
///  Diese Klasse steuert die Funktionen des Nuss-Korbs
/// </summary> 


public class BasketScript : MonoBehaviour
{
    
    private ManagerKlasse Manager; //Erstellt eine Instanz der Manager-Klasse



    void Start()
    {
        this.Manager = ManagerKlasse.Instance;
        Manager.setBasket(this);
    }

    
    void Update()
    {
       
    }

    void AddNut()
    {

    }

    public void UpdateBasket(float count)
    {
        Debug.Log("Tryiing to access / Nuts / nut_" + count);
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Nuts/nut_"+count, typeof(Sprite)) as Sprite;
    }
    
}
