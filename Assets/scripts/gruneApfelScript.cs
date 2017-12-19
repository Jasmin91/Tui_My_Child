﻿using System;
using System.Collections.Generic;
using UnityEngine;


public class gruneApfelScript : MonoBehaviour
{
    private Management ms_Instance; //Erstellt eine Instanz der Manager-Klasse
    private bool appleVisible = true; //Boolsche Variable, ob Apfel angezeigt wird
    private float appleNumber = 0; //Nummer des zu ladenden Apfel-Bildes (ändert sich je nach Reifegrad)



    void Start()
    {
        this.ms_Instance = Management.Instance;
        gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 1f); //Verkleinert den Apfel ("unreif")
    }

    void Update()
    {
        
        if (this.ms_Instance.Regen.IsVisible)
        {
            this.GrowingApple(); //Lässt Apfel wachsen, wenn der Regen angezeigt wird
        }

        if (this.ms_Instance.Sonne.IsVisible)
        {
            this.RipingApple(); //Lässt den Apfel reifen, wenn die Sonne angezeigt wird
        }

        if (this.ms_Instance.getHarvestingReady()&&appleVisible)
        {
            this.HideApple(); //Lässt den Apfel verschwinden, wenn geerntet werden kann (und die roten Äpfel auftauchen)
        }
    }

    //Zeigt den Apfel
    void ShowgApple()
    {
        gameObject.GetComponent<Renderer>().enabled = true;
        appleVisible = true;
        
    }

    //Verbirgt den Apfel
    void HideApple()
    {
        
       gameObject.GetComponent<Renderer>().enabled = false;
       appleVisible = false;
        
    }

    //Lässt den Apfel reifen
    void RipingApple()
    {
        if (appleNumber+1 == ms_Instance.Sonne.sunDuration) //checkt, ob das nächste Bild auch wirklich einen Apfel zeigt, der reifer ist, als der vorhergehende
        {
            appleNumber = ms_Instance.Sonne.sunDuration;
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("apfel_" + appleNumber, typeof(Sprite)) as Sprite; //Setzt neues (reiferes) Bild
    }

    //Lässt den Apfel wachsen
    void GrowingApple()
    {
        
        float oldSize = gameObject.transform.localScale.x; //Alte Größe des Apfels

        if (ms_Instance.getRainDuration() < 5 && ms_Instance.getRainDuration() > 0)
        {
            float size = 0.1f + (0.1f - (0.02f * (ms_Instance.getRainDuration()))); //Berechnen der neuen Größe
            if (size > oldSize&&size<=0.2f) //checkt, ob neue Größe wirklich größer, als die alte
            {
                gameObject.transform.localScale = new Vector3(size, size, 1f); //Setzt die neue Größe/
            }
        }
    }
}
