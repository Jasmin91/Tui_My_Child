using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>  
///  Diese Klasse steuert die Funktionen des Knochens
/// </summary> 


public class Knochen : MonoBehaviour
{

    private KnochenManager km_Instance; //Erstellt eine Instanz der KnochenManager-Klasse
    public float speed = 2f;
    /// <summary> Liste, die alle Punkte des Pfades speichert </summary>
    private List<Point> KnotList = new List<Point>();


    void Start()
    {
        this.km_Instance = KnochenManager.Instance;
        this.defineWay();
    }


    void Update()
    {
        if (km_Instance.getRightWayFound())
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

    private void move(Point pos)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pos.getPosition(), step);
        pos.Reached(this.transform.position);
    }
    
    private void defineWay()
    {
        float z = this.transform.position.z;
        KnotList.Add(new Point(new Vector3(-4.0f, 0.3f, z)));
        KnotList.Add(new Point(new Vector3(-0.8f, 0.3f, z)));
        KnotList.Add(new Point(new Vector3(-0.7f, -2.8f, z)));
        KnotList.Add(new Point(new Vector3(3.1f, -2.7f, z)));
        KnotList.Add(new Point(new Vector3(3.0f, -0.65f, z)));
        KnotList.Add(new Point(new Vector3(0.85f, -0.47f, z)));
        KnotList.Add(new Point(new Vector3(0.8f, 1.0f, z)));
        KnotList.Add(new Point(new Vector3(3.95f, 1.16f, z)));
        KnotList.Add(new Point(new Vector3(4.0f, 3.6f, z)));
    }
}
