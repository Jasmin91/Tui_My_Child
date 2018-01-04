using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>  
///  Diese Management-Klasse steuert und verbindet alle anderen Klassen
/// </summary>
public class KnochenManager {

    private ManagerKlasse Manager; //Erstellt eine Instanz der Manager-Klasse
    private static KnochenManager km_Instance; //Erstellt eine Instanz der KnochenManager-Klasse
    /// <summary> Liste, die die Gelenke speichert </summary>
    private List<GelenkController> GelenkList = new List<GelenkController>();
    private bool GameSolved = false;


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

    public bool getRightWayFound()
    {
        bool result = true;
        foreach (GelenkController gk in GelenkList)
        {
            if (!gk.getRightRotation())
            {
                result = false;
                
            }
        }
        return result;
    }
    
    public bool getGameSolved()
    {
        return this.GameSolved;
    }

    public void setGameSolved(bool var)
    {
        this.GameSolved = var;
    }

    public void FinishGame()
    {
        SceneManager.LoadScene("Startspiel");
    }


}
