using System;
using System.Collections;
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
    private float showTime = 3;



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
        setPositiont();
        setRotationText();
    }

    
    public void setPositionText()
    {

        float x = this.transform.position.x;
        float y = this.transform.position.y;
        float z = Ausgabe.transform.position.z;

        Vector3 pos = new Vector3(x, y, z);
        Ausgabe.rectTransform.position = pos;
     }

    public Vector3 calcPosition()
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

    public void setPositiont()
    {
        this.transform.localPosition = calcPosition();
    }

    public void setRotationText()
    {
        Ausgabe.transform.localRotation = this.transform.rotation;
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

    public void DisplayText(String s, float x)
    {
        Debug.Log("DisplayText mit Zeit");
        this.Show();
        this.Ausgabe.text = s;
        this.showTime = x;
        StartCoroutine(this.showXSeconds());


    }

    private IEnumerator showXSeconds(float countdownValue = 0)
    {
        Debug.Log("Coundown gestartet!");
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
