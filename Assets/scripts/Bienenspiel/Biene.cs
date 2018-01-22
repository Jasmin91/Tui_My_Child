using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biene : MonoBehaviour {
    public AudioSource tickSource;
    public GameObject Blume;
    public GameObject glas;
    private BienenManager manager;
    private float deviation = 10f;
    public int counter_left = 0;
    public int counter_right = 0;
    public bool WarZuletztLinks = true;
    public bool WarZuletztRechts = true;
    private int numberRotations = 4;
    public bool onFlower = false;
    public float ActRotation;
    float referenceLeft = 50;
    float referenceRight = 320;

    /// <summary>
    /// Lässt Sound nur 1x spielen
    /// </summary>
    private bool play = false;
    

    // Use this for initialization
    public void Start()
    {
        manager = BienenManager.Instance;
        tickSource = GetComponent<AudioSource>();
        glas.SetActive(true);
        this.GetReferenceAxes();
    }


    void Update()
    {
        Debug.Log(this.transform.eulerAngles.z);
        ActRotation = this.transform.eulerAngles.z;
        if (onFlower)
        {
            CheckRotation();
        }
    }

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject==Blume)
        {
            
            onFlower = true;
        }
    }

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
    }

    private void CheckRotation()
    {
        
        GetRotation();
        if ((counter_left < numberRotations) && (Equal(ActRotation, referenceLeft)) && WarZuletztRechts)
            {
            Debug.Log("Links:"+counter_left + ":" + ActRotation + "ist gleich" + referenceLeft);
            counter_left++;
            WarZuletztLinks = true;
            WarZuletztRechts = false;
        }

        if ((counter_right < numberRotations) && (Equal(ActRotation, referenceRight)) && WarZuletztLinks)
            {
            Debug.Log("Rechts:"+counter_right+":"+ActRotation + "ist gleich" + referenceRight);
            counter_right++;
            WarZuletztRechts = true;
            WarZuletztLinks = false;
        }
        


        int filling = counter_left;
        string name = "glas" + filling;

        glas.GetComponent<SpriteRenderer>().sprite = Resources.Load(name, typeof(Sprite)) as Sprite;


        if (counter_left == numberRotations)
        {
            Debug.Log(numberRotations);
            if (!play)
            {
                tickSource.Play();
                play = true;
            }
            manager.HoneyReady(this);
        }
    }

    private void GetReferenceAxes() {


        switch (this.name)
        {
            case "bee":
                referenceLeft = 50;
                referenceRight = 320;
                break;
            case "bee1":
                referenceLeft = 140;
                referenceRight = 50;
                break;
            case "bee2":
                referenceLeft = 225;
                referenceRight = 140;
                break;
            case "bee3":
                referenceLeft = 320;
                referenceRight = 225;
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
