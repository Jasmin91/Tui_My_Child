using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>  
///  Diese Klasse steuert das Upaten des Startspiels nach einem Minispiel
/// </summary> 


public class Point
{
    
    private KnochenManager km_Instance; //Erstellt eine Instanz der KnochenManager-Klasse
    public Vector3 position;
    private bool visited = false;
    
    public Point(Vector3 pos)
    {
        this.position = pos;
    }

    public void setVisited(bool visit)
    {
        this.visited = visit;
    }

    public bool getVisited()
    {
        return this.visited;
    }

    public bool Reached(Vector3 var)
    {
        bool result = false;

        if(this.position == var)
        {
            result = true;
            this.setVisited(true);
        }

        return result;
    }

    public Vector3 getPosition()
    {
        return this.position;
    }
}
