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
        Ausgabe.text = "";
        Ausgabe.rectTransform.sizeDelta=new Vector2(95,45);

        setPositionText(this.transform.position);
        animal.setSpeaker(this);

    }

    void Update()
    {
      //  this.setPosition(animal.transform.position);
        setPositionText(this.transform.position);
    }

    
    public void setPositionText(Vector3 pos)
    {
        //Debug.Log("Setze Position von " + this.name + " auf " + pos.x + "und" + pos.y);
        //Ausgabe.transform.position = pos;
        Ausgabe.rectTransform.position = pos;
    }


    public void setPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void Show()
    {
        this.GetComponent<Renderer>().enabled = true;
    }

    public void Hide()
    {
        this.GetComponent<Renderer>().enabled = false;
    }
    public void DisplayText(String s)
    {
        this.Show();
        this.Ausgabe.text = s;

    }
}
