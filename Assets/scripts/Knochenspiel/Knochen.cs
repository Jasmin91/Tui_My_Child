using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>  
///  Diese Klasse steuert die Funktionen des Knochens
/// </summary> 


public class Knochen : MonoBehaviour
{
    /// <summary>
    ///Erstellt eine Instanz der KnochenManager-Klasse 
    /// </summary>
    private KnochenManager Km_Instance; 

    /// <summary>
    /// Geschwindigkeit, mit der Knochen zum Hund schwebt
    /// </summary>
    public float Speed = 2f;

    /// <summary> 
    /// Liste, die alle Punkte des Pfades speichert 
    /// </summary>
    private List<Point> KnotList = new List<Point>();

    /// <summary>
    /// Sound nach erfolgreichem Beenden des Spiels
    /// </summary>
    public AudioSource WinSound;
    //https://www.youtube.com/watch?v=hvs34cmWu8w (Abrufdatum: 29.01.18)

    /// <summary>
    /// Hilfsbool, damit Sound nur 1x gespielt wird
    /// </summary>
    bool Play = false;

    public bool WalkedThrough = false;

    Point next;


    void Start()
    {
        this.Km_Instance = KnochenManager.Instance;
        this.DefineWay();
    }


    void Update()
    {
        //Bewegt Knochen zu jedem einzelnen Punkt
        if (Km_Instance.GetRightWayFound())
        {
            if (!Play)
            {
                WinSound.Play();
                Play = true;
            }



            if (KnotList.Count > 0)
            {
                next = KnotList[0];
                if (!next.GetVisited())
                {
                    this.Move(next);
                }
                else
                {
                    KnotList.RemoveAt(0);
                }
            }
            else
            {
                this.Km_Instance.SetGameSolved(true);
            }


        }
    }

    /// <summary>
    /// Bewegt den Knochen
    /// </summary>
    /// <param name="pos">Position, zu der Hund bewegt werden soll</param>
    private void Move(Point pos)
    {
        float step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pos.GetPosition(), step);
        pos.Reached(this.transform.position);
    }
    
    /// <summary>
    /// Definition des Weges, den der Knochen zurücklegen soll
    /// </summary>
    private void DefineWay()
    {
        float z = this.transform.position.z;
       
        
        KnotList.Add(new Point(new Vector3(-21f, 2.8f, z)));
        KnotList.Add(new Point(new Vector3(-4.1f, 2.8f, z)));
        KnotList.Add(new Point(new Vector3(-4.1f, -14f, z)));
        KnotList.Add(new Point(new Vector3(17.8f, -14f, z)));
        KnotList.Add(new Point(new Vector3(17.8f, -1.6f, z)));
        KnotList.Add(new Point(new Vector3(5.1f, -1.6f, z)));
        KnotList.Add(new Point(new Vector3(5.1f, 6.3f, z)));
        KnotList.Add(new Point(new Vector3(22.4f, 6.3f, z)));
        KnotList.Add(new Point(new Vector3(22.4f, 19.2f, z)));
    
        
    }
}
