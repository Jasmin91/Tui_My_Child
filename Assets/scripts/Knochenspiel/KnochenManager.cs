using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>  
///  Diese KnochenManagement-Klasse steuert und verbindet alle anderen Klassen des Knochen-Spiels
/// </summary>
public class KnochenManager {

    /// <summary>
    ///Erstellt eine Instanz der Manager-Klasse 
    /// </summary>
    private ManagerKlasse Manager; 

    /// <summary>
    ///Erstellt eine Instanz der KnochenManager-Klasse 
    /// </summary>
    private static KnochenManager km_Instance;

    /// <summary> Liste, die die Gelenke speichert </summary>
    private List<GelenkController> GelenkList = new List<GelenkController>();

    /// <summary>
    /// Bool, ob Spiel gelöst wurde
    /// </summary>
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


    /// <summary>
    /// Getter für die ManagerKlasse
    /// </summary>
    /// <returns>Die ManagerKlasse für das gesamte Spiel</returns>
    public ManagerKlasse GetManager()
    {
        return Manager;
    }

    /// <summary>
    /// Fügt Gelenk der Gelenk-Liste hinzu
    /// </summary>
    /// <param name="gc">Hinzuzufügendes Gelenk</param>
    public void AddGelenk(GelenkController gc)
    {
        this.GelenkList.Add(gc);
    }
    
    /// <summary>
    /// Methode resettet das Spiel, damit es später neu gestartet werden kann
    /// </summary>
    public void ResetGame()
    {
        GelenkList.Clear();
        GameSolved = false;
    }

    /// <summary>
    /// Methode regelt, was passiert, wenn Spiel beendet ist
    /// </summary>
    public void FinishGame()
    {
        this.ResetGame();
        SceneManager.LoadScene("Startspiel");
    }
    #region Getter&Setter


    /// <summary>
    /// Getter, ob Lösungsweg gefunden wurde
    /// </summary>
    /// <returns>Bool, ob Lösungsweg gefunden wurde</returns>
    public bool GetRightWayFound()
    {
        bool result = true;
        foreach (GelenkController gk in GelenkList)
        {
            if (!gk.GetRightRotation())
            {
                result = false;

            }
        }
        return result;
    }

    /// <summary>
    /// Getter, ob Spiel gelöst wurde
    /// </summary>
    /// <returns>Bool, ob Spiel gelöst wurde</returns>
    public bool GetGameSolved()
    {
        return this.GameSolved;
    }

    /// <summary>
    /// Setter, ob Spiel gelöst wurde
    /// </summary>
    /// <param name="var">Bool, ob Spiel gelöst wurde</param>
    public void SetGameSolved(bool var)
    {
        this.GameSolved = var;
    }
    #endregion
}

