using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Klasse für die Steuerungn der Bienen
/// </summary>
public class Biene : MonoBehaviour {
    /// <summary>
    /// Sound für die Bienen wenn sie auf der Blume sind
    /// </summary>
    public AudioSource tickSource;
    /// <summary>
    /// Blume wird definiert
    /// </summary>
    public GameObject Blume;
    /// <summary>
    /// Glas wird definiert
    /// </summary>
    public GameObject glas;
    /// <summary>
    /// Manager aus der BienenManager-Klasse wird definiert
    /// </summary>
    private BienenManager manager;
    /// <summary>
    /// Abweichungswert für die Rotation wird definiert
    /// </summary>
    private float deviation = 10f;
    /// <summary>
    /// Counter für die Rotation nach Links
    /// </summary>
    public int counter_left = 0;
    /// <summary>
    /// Counter für die Rotation nach Rechts
    /// </summary>
    public int counter_right = 0;
    /// <summary>
    /// Bestimmt ob die Rotation zuletzt nach Links war
    /// </summary>
    public bool WarZuletztLinks = true;
    /// <summary>
    /// Bestimmt ob die Rotation zuletzt nach rechts war
    /// </summary>
    public bool WarZuletztRechts = true;
    /// <summary>
    /// Anzahl der Rotation für das Füllen vom Glas
    /// </summary>
    private int numberRotations = 4;
    /// <summary>
    /// Überprüft ob die Biene sich während der Rotation auf der Blume befindet
    /// </summary>
    public bool onFlower = false;
    /// <summary>
    /// Gibt den Rotationswinkel der Biene wieder
    /// </summary>
    public float ActRotation;
    /// <summary>
    /// Gibt dem Referenzwert der Rotation nach Links an
    /// </summary>
    float referenceLeft = 50;
    /// <summary>
    /// Gibt den Referenzwert der Rotation nach Rechts an
    /// </summary>
    float referenceRight = 320;
    /// <summary>
    /// Gibt den zweiten Referenzwert der Rotation nach Links an, falls die Biene Kopfüber auf der Blume positioniert wird
    /// </summary>
    float referenceLeft2 = 230;
    /// <summary>
    /// Gibt den zweiten Referenzwert der Rotation nach Rechts an, falls die Biene Kopfüber auf der Blume positioniert wird
    /// </summary>
    float referenceRight2 = 140;

    /// <summary>
    /// Lässt Sound nur 1x spielen
    /// </summary>
    private bool play = false;


    /// <summary>
    /// Bienen werden resettet
    /// Instanz des Managers wird erstellt
    /// Sound wir hervogerufen
    /// Glas wird auf dem Spielfel sichtbar gemacht
    /// Referenzachsen werden hervorgeholt
    /// </summary>
    public void Start()
    {
        this.ResetBee(); 
        manager = BienenManager.Instance; 
        tickSource = GetComponent<AudioSource>(); 
        glas.SetActive(true); 
        this.GetReferenceAxes(); 
    }

    /// <summary>
    /// Gespeicherte rotation wird aktualisiert und mit der Referenz abgeglichen
    /// </summary>
    void Update()
    {
        ActRotation = this.transform.eulerAngles.z; 
        if (onFlower)
        {
            CheckRotation(); 
        }
    }

    /// <summary>
    /// Überprüft ob Biene auf der Blume ist
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject==Blume)
        {
            
            onFlower = true;
        }
    }
    /// <summary>
    /// Überprüft ob Biene runter von der Blume ist
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Blume)
        {
            onFlower = false;
        }
    }

    private void GetRotation()
    {
        float x=  gameObject.transform.rotation.x;
        float y = gameObject.transform.rotation.y;
        float z = gameObject.transform.rotation.z;

        //Zum bestimmen der Rotation
    }
    /// <summary>
    /// Vergleicht die Rotationsanzahl nach links oder rechts mit der Anzahl der Rotation die erlaubt sind und ob die Rotation im Referenzbereich ist
    /// Erhöht den Counter der Rotation nach Links bzw nach Rechts
    /// </summary>
    private void CheckRotation()
    {
        
        GetRotation();
        if ((counter_left < numberRotations) && (Equal(ActRotation, referenceLeft)|| Equal(ActRotation, referenceLeft2)) && WarZuletztRechts)
            {
            counter_left++;
            WarZuletztLinks = true;
            WarZuletztRechts = false;
        }

        if ((counter_right < numberRotations) && (Equal(ActRotation, referenceRight)|| Equal(ActRotation, referenceRight2)) && WarZuletztLinks)
            {
            counter_right++;
            WarZuletztRechts = true;
            WarZuletztLinks = false;
        }

        /// <summary>
        /// Generiert Dateiname 
        /// </summary>
        int filling = counter_left;
        string name = "glas" + filling; 
        ///<summary>
        ///Holt Datei aus Ressourcen
        ///</summary>
        glas.GetComponent<SpriteRenderer>().sprite = Resources.Load(name, typeof(Sprite)) as Sprite;

        // <summary>
        /// Füllt die Biene ihr Glas mit Honig, gibt sie dem Manager bescheid, dass der Vorgang erfolgreich abgeschlossen ist
        /// </summary>
        if (counter_left == numberRotations)
        {
            if (!play)
            {
                tickSource.Play();
                play = true;
            }
            manager.HoneyReady(this); 
        }
    }

    /// <summary>
    /// Setzt Referenzachsen je nach Biene
    /// </summary>
    private void GetReferenceAxes() {


        switch (this.name)
        {
            case "bee":
                referenceLeft = 50;
                referenceRight = 320;
                referenceLeft2 = 230;
                referenceRight2 = 140;
                break;
            case "bee1":
                referenceLeft = 140;
                referenceRight = 50;
                referenceLeft2 = 320;
                referenceRight2 = 230;
                break;
            case "bee2":
                referenceLeft = 230;
                referenceRight = 140;
                referenceLeft2 = 50;
                referenceRight2 = 320;
                break;
            case "bee3":
                referenceLeft = 320;
                referenceRight = 230;
                referenceLeft2 = 140;
                referenceRight2 = 50;
                break;

        }
    }

    /// <summary>
    /// Setzt Biene zurück für neue Runde
    /// </summary>
    public void ResetBee()
    {
        counter_right = 0;
        counter_left = 0;
        WarZuletztLinks = true;
        WarZuletztRechts = true;
    }



    /// <summary>
    /// Vergleich aktuelle Rotation mit Zielrotation (hier nur z-Wert relevant)
    /// </summary>
    /// <param name="q">Abzugleichender, aktuellert Wert</param>
    /// <returns>Bool, ob übereinstimmend</returns>
    private bool Equal(float q, float reference)
    {
        bool result = false;
        if (Check(q, reference))
        {
            result = true;
        }
        return result;
    }

    /// <summary>
    /// Prüft, ob Wert ungefähr dem Zielwert entspricht
    /// </summary>
    /// <param name="value">Zu überprüfender Wert</param>
    /// <param name="reference">Zielwert</param>
    /// <returns></returns>
    private bool Check(float value, float reference)
    {
        bool result = false;


        if (IsBetween(value, CalcUpperBound(reference), CalcLowerBound(reference)))
        {
            result = true;
        }
        return result;
    }

    /// <summary>
    /// Prüft ob Wert zwischen zwei Werten lieg
    /// </summary>
    /// <param name="value">Zu überprüfender Wert</param>
    /// <param name="upper">Oberer Grenzwert</param>
    /// <param name="lower">Unterer Grenzwert</param>
    /// <returns></returns>
    private bool IsBetween(float value, float upper, float lower)
    {
        bool result = false;

        if (value <= upper && value >= lower)
        {
            result = true;
        }

        return result;
    }

    /// <summary>
    /// Berechnet oberen Grenzwert
    /// </summary>
    /// <param name="value">Ursprungswert</param>
    /// <returns>Berechnete Obergrenze</returns>
    private float CalcUpperBound(float value)
    {
        float add = deviation;
        float result = value + add; ;
        return result;
    }

    /// <summary>
    /// Berechnet unteren Grenzwert
    /// </summary>
    /// <param name="value">Ursprungswert</param>
    /// <returns>Berechnete Untergrenze</returns>
    private float CalcLowerBound(float value)
    {
        float sub = deviation;
        float result = value - sub; ;
        return result;
    }

}
