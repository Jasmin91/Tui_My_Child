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
    private KnochenManager km_Instance; 

    /// <summary>
    /// Geschwindigkeit, mit der Knochen zum Hund schwebt
    /// </summary>
    public float speed = 2f;

    /// <summary> 
    /// Liste, die alle Punkte des Pfades speichert 
    /// </summary>
    private List<Point> KnotList = new List<Point>();


    void Start()
    {
        this.km_Instance = KnochenManager.Instance;
        this.defineWay();
    }


    void Update()
    {
        //Bewegt Knochen zu jedem einzelnen Punkt
        if (km_Instance.GetRightWayFound())
        {
            if (!KnotList[0].getVisited())
            {
                this.move(KnotList[0]);
            }
            else if (!KnotList[1].getVisited())
            {
                this.move(KnotList[1]);
            }
            else if (!KnotList[2].getVisited())
            {
                this.move(KnotList[2]);
            }
            else if (!KnotList[3].getVisited())
            {
                this.move(KnotList[3]);
            }
            else if (!KnotList[4].getVisited())
            {
                this.move(KnotList[4]);
            }
            else if (!KnotList[5].getVisited())
            {
                this.move(KnotList[5]);
            }
            else if (!KnotList[6].getVisited())
            {
                this.move(KnotList[6]);
            }
            else if (!KnotList[7].getVisited())
            {
                this.move(KnotList[7]);
            }
            else if (!KnotList[8].getVisited())
            {
                this.move(KnotList[8]);
            }
            else
            {
                this.km_Instance.setGameSolved(true);
            }
        }

    }

    /// <summary>
    /// Bewegt den Hund
    /// </summary>
    /// <param name="pos">Position, zu der Hund bewegt werden soll</param>
    private void move(Point pos)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pos.getPosition(), step);
        pos.Reached(this.transform.position);
    }
    
    /// <summary>
    /// Definition des Weges, den der Knochen zurücklegen soll
    /// </summary>
    private void defineWay()
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
