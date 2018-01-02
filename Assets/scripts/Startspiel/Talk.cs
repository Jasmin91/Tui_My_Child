using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>  
///  Diese Klasse steuert die Ausgabe des Scores
/// </summary> 


public class Talk : MonoBehaviour
{
    /// <summary>  
    ///  Instanz der Manager-Klasse
    /// </summary>
    private ManagerKlasse Manager;

    public AnimalController animal;

  

    /// <summary> Text der ausgegeben werden soll </summary>
    public Text Ausgabe;



    void Start()
    {
        this.Manager = ManagerKlasse.Instance;
        this.Hide();
        Ausgabe.rectTransform.sizeDelta=new Vector2(95,45);
        animal.setSpeaker(this);

    }

    void Update()
    {
        setPositionText();
        setRotationText();
    }

    
    public void setPositionText()
    {
        Ausgabe.rectTransform.position = this.transform.position;
     }


    public void setRotationText()
    {
        Ausgabe.transform.localRotation = animal.transform.rotation;
    }

    public void Show()
    {
        this.GetComponent<Renderer>().enabled = true;
    }

    public void Hide()
    {
        this.GetComponent<Renderer>().enabled = false;
        this.Ausgabe.text = "";
    }
    public void DisplayText(String s)
    {
        this.Show();
        this.Ausgabe.text = s;

    }
}
