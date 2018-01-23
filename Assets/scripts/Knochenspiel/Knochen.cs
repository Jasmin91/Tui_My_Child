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

    /// <summary>
    /// Hilfsbool, damit Sound nur 1x gespielt wird
    /// </summary>
    bool Play = false;


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
            if (!KnotList[0].GetVisited())
            {
                this.Move(KnotList[0]);
            }
            else if (!KnotList[1].GetVisited())
            {
                this.Move(KnotList[1]);
            }
            else if (!KnotList[2].GetVisited())
            {
                this.Move(KnotList[2]);
            }
            else if (!KnotList[3].GetVisited())
            {
                this.Move(KnotList[3]);
            }
            else if (!KnotList[4].GetVisited())
            {
                this.Move(KnotList[4]);
            }
            else if (!KnotList[5].GetVisited())
            {
                this.Move(KnotList[5]);
            }
            else if (!KnotList[6].GetVisited())
            {
                this.Move(KnotList[6]);
            }
            else if (!KnotList[7].GetVisited())
            {
                this.Move(KnotList[7]);
            }
            else if (!KnotList[8].GetVisited())
            {
                this.Move(KnotList[8]);
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
