using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>  
///  Diese Klasse steuert die Funktionen des Nuss-Korbs
/// </summary> 


public class BasketScript : MonoBehaviour
{
    /// <summary>
    ///Erstellt eine Instanz der Manager-Klasse 
    /// </summary>
    private ManagerKlasse Manager; 

    /// <summary> 
    /// Zeigt Text (Anzal der Nüsse) an 
    /// </summary>
    public Text Ausgabe;


    void Start()
    {
        this.Manager = ManagerKlasse.Instance;
        Manager.SetBasket(this);
        this.SetPositionText();
        this.UpdateBasket(Manager.NutCounter);
    }

    
    void Update()
    {
        this.ShowScore(this.Manager.NutCounter + "");
    }

    /// <summary>
    /// Gibt den Score als Text aus
    /// </summary>
    /// <param name="txt"></param>
    public void ShowScore(string txt)
    {
        this.Ausgabe.text = txt;
    }

    /// <summary>
    /// Updated das Bild des Korbes, passend zur Anzahl der Nüsse
    /// </summary>
    /// <param name="count">Anzahl der anzuzeigenden Nüsse</param>
    public void UpdateBasket(float count)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Nuts/nut_"+count, typeof(Sprite)) as Sprite;
    }
    
}
