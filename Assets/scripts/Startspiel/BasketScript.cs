using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>  
///  Diese Klasse steuert die Funktionen des Nuss-Korbs
/// </summary> 


public class BasketScript : MonoBehaviour
{
    
    private ManagerKlasse Manager; //Erstellt eine Instanz der Manager-Klasse
    /// <summary> Zeigt Text an </summary>
    public Text Ausgabe;


    void Start()
    {
        this.Manager = ManagerKlasse.Instance;
        Manager.setBasket(this);
        this.setPositionText();
    }

    
    void Update()
    {
        this.ShowScore(this.Manager.NutCounter + "");
    }

    public void ShowScore(string txt)
    {
        this.Ausgabe.text = txt;
    }

    public void UpdateBasket(float count)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Nuts/nut_"+count, typeof(Sprite)) as Sprite;
    }

    public void setPositionText()
    {
        Ausgabe.rectTransform.position = this.transform.position;
    }
}
