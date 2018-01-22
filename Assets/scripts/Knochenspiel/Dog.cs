using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>  
///  Diese Klasse steuert die Funktionen des Hundes
/// </summary> 
public class Dog : MonoBehaviour
{

    /// <summary>
    ///Erstellt eine Instanz der KnochenManager-Klasse 
    /// </summary>
    private KnochenManager km_Instance; 

    /// <summary>
    /// Geschwindigkeit, mit der Hund springt
    /// </summary>
    public float speed = 1.5f;

    /// <summary>
    /// Höhe, die Hund nach erfolgreicher Lösung springt.
    /// </summary>
    public float height = 0.25f;

    /// <summary> 
    /// Liste, die alle Punkte des Knochen-Pfades speichert 
    /// </summary>
    private List<Point> KnotList = new List<Point>();

    public AudioSource DogSound;


    void Start()
    {
        this.km_Instance = KnochenManager.Instance;
        this.defineWay();
    }


    void Update()
    {
        if (km_Instance.getGameSolved())
            
        {
            DogSound.Play();
            if (!KnotList[0].getVisited())
            {
                this.Move(KnotList[0]);
            }
            else if (!KnotList[1].getVisited())
            {
                this.Move(KnotList[1]);
            }
            else
            {
                this.km_Instance.FinishGame();
            }
        }

    }

    /// <summary>
    /// Bewegt den Hund
    /// </summary>
    /// <param name="pos">Position, zu der Hund springen soll</param>
    private void Move(Point pos)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pos.getPosition(), step);
        pos.Reached(this.transform.position);
    }
    
    /// <summary>
    /// Berechne Pfad, dem entlang der Hund springt
    /// </summary>
    private void defineWay()
    {
        float x = this.transform.position.x;
        float y = this.transform.position.y+this.height;
        float z = this.transform.position.z;
        KnotList.Add(new Point(new Vector3(x, y, z)));
        KnotList.Add(new Point(this.transform.position));
    }
}
