//Author: Jasmin Profus

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>  
///  Diese Klasse repräsentiert einen Punkt eines Zurückzulegenden Weges
/// </summary> 


public class Point
{
    /// <summary>
    ///Erstellt eine Instanz der KnochenManager-Klasse
    /// </summary>
    private KnochenManager km_Instance; 
    public Vector3 position;
    private bool visited = false;
    

    /// <summary>
    /// Constructor, dem Position mitgegeben werden muss
    /// </summary>
    /// <param name="pos">Position des Punktes</param>
    public Point(Vector3 pos)
    {
        this.position = pos;
    }

    /// <summary>
    ///  Methode vergleicht, ob Position mit Position des Point übereinstimmt
    /// </summary>
    /// <param name="var">Zu überprüfende Position</param>
    /// <returns>Bool, ob übereinstmmt/erreicht wurde</returns>
    public bool Reached(Vector3 var)
    {
        bool result = false;

        if(this.position == var)
        {
            result = true;
            this.SetVisited(true);
        }

        return result;
    }

    #region Getter&Setter
    

    /// <summary>
    /// Getter für die Position
    /// </summary>
    /// <returns>Position des Point</returns>
    public Vector3 GetPosition()
    {
        return this.position;
    }

    /// <summary>
    /// Setter, ob Punkt bereits besucht wurde
    /// </summary>
    /// <param name="visit">Bool, ob Punkt bereits besucht wurde </param>
    public void SetVisited(bool visit)
    {
        this.visited = visit;
    }

    /// <summary>
    /// Getter, ob Punkt beeits besucht wurde
    /// </summary>
    /// <returns>Bool, ob Punkt bereits besucht wurde</returns>
    public bool GetVisited()
    {
        return this.visited;
    }
#endregion
}
