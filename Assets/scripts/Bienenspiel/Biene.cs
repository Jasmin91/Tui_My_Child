using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biene : MonoBehaviour {
    public AudioSource tickSource;
    public GameObject Blume;
    public GameObject glas;
    private BienenManager manager;
    private float deviation = 0.1f;
    private int counter_left = 0;
    private int counter_right = 0;
    private bool WarZuletztLinks = true;
    private bool WarZuletztRechts = true;
    private int numberRotations = 3;
    private bool onFlower = false;
    

    // Use this for initialization
    public void Start()
    {
        manager = BienenManager.Instance;
        tickSource = GetComponent<AudioSource>();
        glas.SetActive(true);
    }


    void Update()
    {

        Debug.Log(this.name+"OnFlower:" + onFlower);
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
        Debug.Log(this.name + "Rotation:" + gameObject.transform.rotation);
        float x=  gameObject.transform.rotation.x;
        float y = gameObject.transform.rotation.y;
        float z = gameObject.transform.rotation.z;
    }

    private void CheckRotation()
    {

        Quaternion referenceLeft = new Quaternion(0.0f, 0.0f, 0.5f, 0.8f);
        Quaternion referenceRight = new Quaternion(0.0f, 0.0f, -0.3f, 0.9f);
        Quaternion ActRotation = gameObject.transform.rotation;


        Debug.Log(this.name+"---ZuletztLinks:"+WarZuletztLinks+"----ZuletztRechts"+WarZuletztRechts+ "counter_left<numberRotations"+ (counter_left < numberRotations) + "Equal("+ ActRotation + ", "+referenceLeft+")"+ Equal(gameObject.transform.rotation, referenceLeft));

        if (counter_left<numberRotations && Equal(ActRotation, referenceLeft) && WarZuletztRechts)
        {
            counter_left++;
            WarZuletztLinks = true;
            WarZuletztRechts = false;
        }

        if (counter_right < numberRotations && Equal(ActRotation, referenceRight) && WarZuletztLinks)
        {
            counter_right++;
            WarZuletztRechts = true;
            WarZuletztLinks = false;
        }

        Debug.Log(this.name + "---" + counter_left + " -- " + counter_right);


        int filling = counter_left;
        string name = "glas" + filling;
        Debug.Log(this.name+ "---" + name);

        glas.GetComponent<SpriteRenderer>().sprite = Resources.Load(name, typeof(Sprite)) as Sprite;


        if (counter_right == numberRotations && counter_left == numberRotations)
        {
            tickSource.Play();
            manager.HoneyReady(this);
        }
    }



    /// <summary>
    /// Vergleich aktuelle Rotation mit Zielrotation (hier nur z-Wert relevant)
    /// </summary>
    /// <param name="q">Abzugleichender, aktuellert Wert</param>
    /// <returns>Bool, ob übereinstimmend</returns>
    private bool Equal(Quaternion q, Quaternion reference)
    {
        bool result = false;
        if (Check(q.z, reference.z))
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
