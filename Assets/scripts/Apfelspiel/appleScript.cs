using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  
///  Diese Klasse steuert die  Funktion eines  Apfels
/// </summary> 

public class appleScript : MonoBehaviour
{
    
    private Management ms_Instance; //Erstellt eine Instanz der Manager-Klasse
    public float fallingSpeed = 0.23f; //Geschwindigkeit, mit der der Apfel fällt
    private bool fallen = false; //Boolscher Wert, ob Apfel bereits gefallen ist
    private bool red = false; //Boolscher Wert, ob Apfel bereits reif ist
    private bool grown = false; //Boolscher Wert, ob Apfel bereits gewachsen ist
    private float appleNumber = 0; //Nummer des zu ladenden Apfel-Bildes (ändert sich je nach Reifegrad)



    void Start()
    {
        this.ms_Instance = Management.Instance; 
        this.ms_Instance.addApple(this); //Füge Apfel dem Apfel-Array im Manager hinzu
        
    }

    void Update()
    {
    }

    public void SetFallen(bool fallen)
    {
        this.fallen = fallen;
    }

    public bool GetFallen()
    {
        return this.fallen;
    }

    void ShowApple()
    {
        gameObject.GetComponent<Renderer>().enabled = true; //Apfel-Objekt anzeigen
       
    }
    

    public void FallingApple()
    {
        if (red&&grown) {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = this.fallingSpeed; //Schwerkraft auf Fallgeschwindigkeit setzen
            this.SetFallen(true);
        }
      }

    //Lässt den Apfel wachsen
   public void GrowingApple(float rainDuration)
    {
        float oldSize = gameObject.transform.localScale.x; //Alte Größe des Apfels
       
        if (rainDuration <= 5 && rainDuration > 0)
        {
            float size = 0.1f + (0.02f * (rainDuration)); //Berechnen der neuen Größe
            if (size > oldSize && size <= 0.2f) //checkt, ob neue Größe wirklich größer, als die alte
            {
                if (size == 0.09f)
                {
                    size = 0.2f;
                }
                gameObject.transform.localScale = new Vector3(size, size, 1f); //Setzt die neue Größe/

            }
            Debug.Log("size:"+size);
            if (size >= 0.2f)
            {
                grown = true;
            }
        }
    }

    //Lässt den Apfel reifen
    public void RipingApple(float sunDuration)
    {
        if (appleNumber + 1 == sunDuration && sunDuration <= 5) //checkt, ob das nächste Bild auch wirklich einen Apfel zeigt, der reifer ist, als der vorhergehende
        {
            appleNumber = sunDuration;
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("apfel_" + appleNumber, typeof(Sprite)) as Sprite; //Setzt neues (reiferes) Bild
            Debug.Log("appleNumber:" + appleNumber);
            if(appleNumber == 5)
            {
                red = true;
            }
        }
    }
}
