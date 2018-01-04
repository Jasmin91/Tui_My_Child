using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  
///  Diese Management-Klasse steuert und verbindet alle anderen Klassen
/// </summary>
public class KnochenManager {

    private ManagerKlasse Manager; //Erstellt eine Instanz der Manager-Klasse
    private static KnochenManager km_Instance; //Erstellt eine Instanz der KnochenManager-Klasse
    /// <summary> Liste, die die Gelenke speichert </summary>
    private List<GelenkController> GelenkList = new List<GelenkController>();


    public static KnochenManager Instance
	{
		get
		{
            if (km_Instance == null)
			{
                km_Instance = new KnochenManager();
			}
            return km_Instance;
		}
                
	}



    public KnochenManager()
    {
        if (km_Instance != null)
        {
            Debug.LogError("Trying to create two instances of singleton.");
            return;
        }
        km_Instance = this;
        this.Manager = ManagerKlasse.Instance;

    }



    public ManagerKlasse getManager()
    {
        return Manager;
    }

    public void addGelenk(GelenkController gc)
    {
        this.GelenkList.Add(gc);
    }

    public bool getRightWay()
    {
        bool result = true;
        string s = "";
        foreach (GelenkController gk in GelenkList)
        {
            if (!gk.getRightRotation())
            {
                result = false;
                
            }
            s += "--" +gk.name + ": " + result;
        }
        Debug.Log("Ausgabe:"+s);
        return result;
    }
   
 

}
