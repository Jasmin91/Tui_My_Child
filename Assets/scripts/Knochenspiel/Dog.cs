using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>  
///  Diese Klasse steuert die Funktionen des Knochens
/// </summary> 


public class Dog : MonoBehaviour
{

    private KnochenManager km_Instance; //Erstellt eine Instanz der KnochenManager-Klasse
    public float speed = 1.5f;
    public float height = 0.25f;
    /// <summary> Liste, die alle Punkte des Pfades speichert </summary>
    private List<Point> KnotList = new List<Point>();


    void Start()
    {
        this.km_Instance = KnochenManager.Instance;
        this.defineWay();
    }


    void Update()
    {
        if (km_Instance.getGameSolved())
        {
            if (!KnotList[0].getVisited())
            {
                this.move(KnotList[0]);
            }
            else if (!KnotList[1].getVisited())
            {
                this.move(KnotList[1]);
            }
            else
            {
                this.km_Instance.FinishGame();
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
        float x = this.transform.position.x;
        float y = this.transform.position.y+this.height;
        float z = this.transform.position.z;
        KnotList.Add(new Point(new Vector3(x, y, z)));
        KnotList.Add(new Point(this.transform.position));
    }
}
