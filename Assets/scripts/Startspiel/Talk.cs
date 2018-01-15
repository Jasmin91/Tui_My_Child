using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>  
///  Diese Klasse steuert ein Sprechblasen-Objekt
/// </summary> 


public class Talk : MonoBehaviour
{
    /// <summary>  
    ///  Instanz der Manager-Klasse
    /// </summary>
    private ManagerKlasse Manager;

    /// <summary>
    /// Zugehöriges Tier
    /// </summary>
    public AnimalController animal;
    
    /// <summary> Text der ausgegeben werden soll </summary>
    public Text Ausgabe;

    /// <summary>
    /// Dauer die Sprechblase angezeigt werden soll
    /// </summary>
    private float showTime = 4;



    void Start()
    {
        this.Manager = ManagerKlasse.Instance;
        this.Hide();
        Ausgabe.rectTransform.sizeDelta=new Vector2(125,65);
        animal.SetSpeaker(this);

    }

    void Update()
    {
        SetPositionText();
        SetPositiont();
        SetRotationText();
    }


    /// <summary>
    /// Setzt die Position der Sprechblase
    /// </summary>
    public void SetPositiont()
    {
        this.transform.localPosition = CalcPosition();
    }


    /// <summary>
    /// Berechnet die Position der Sprechblase
    /// </summary>
    /// <returns></returns>
    private Vector3 CalcPosition()
    {
        float trans = 1.5f;

        float x = animal.transform.position.x;
        float y = animal.transform.position.y;
        float z = this.transform.position.z;

        switch (animal.name)
        {
            
            case "dog":
                {
                    x = x + trans;
                    break;
                }
            case "horse":
                {
                    x = x - trans;
                    break;
                }
            case "bear":
                {
                    y = y + trans;
                    break;
                }
            case "rabbit":
                {
                    y = y - trans;
                    break;
                }
        }

    Vector3 result = new Vector3(x, y, z);
        return result;
}

    /// <summary>
    /// Setzt die Position des Textes
    /// </summary>
    public void SetPositionText()
    {

        float x = this.transform.position.x;
        float y = this.transform.position.y;
        float z = Ausgabe.transform.position.z;

        Vector3 pos = new Vector3(x, y, z);
        Ausgabe.rectTransform.position = pos;
    }

    /// <summary>
    /// Setzt die Rotation des Textes
    /// </summary>
    public void SetRotationText()
    {
        Ausgabe.transform.localRotation = this.transform.rotation;
    }

    /// <summary>
    /// Zeigt die Sprechblase
    /// </summary>
    public void Show()
    {
        this.GetComponent<Renderer>().enabled = true;
    }

    /// <summary>
    /// Blendet die Sprechblase aus
    /// </summary>
    public void Hide()
    {
        this.GetComponent<Renderer>().enabled = false;
        this.Ausgabe.text = "";
    }

    /// <summary>
    /// Zeigt den Text
    /// </summary>
    /// <param name="s">Anzuzeigender Text</param>
    public void DisplayText(String s)
    {
        this.Show();
        this.Ausgabe.text = s;

    }

    /// <summary>
    /// Zeigt den Text eine bestimmte Zeit
    /// </summary>
    /// <param name="s">Anzuzeigender Text</param>
    /// <param name="x">Dauer der Anzeige</param>
    public void DisplayText(String s, float x)
    {
        Debug.Log("DisplayText mit Zeit");
        this.Show();
        this.Ausgabe.text = s;
        this.showTime = x;
        StartCoroutine(this.ShowXSeconds());
        
    }

    /// <summary>
    /// Timer steuert, wie lang Sprechblase angezeigt wird
    /// </summary>
    /// <param name="countdownValue"></param>
    /// <returns></returns>
    private IEnumerator ShowXSeconds(float countdownValue = 0)
    {
      //  Debug.Log("Coundown gestartet!");
        while (countdownValue < showTime)
        {
            yield return new WaitForSeconds(1.0f);
            countdownValue++;
            if (countdownValue == showTime)
            {
                this.Hide();
            }
        }


    }
}
