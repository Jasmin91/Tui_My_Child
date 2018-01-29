using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>  
///  Diese BienenManagement-Klasse steuert und verbindet alle anderen Klassen des Bienenspiels
/// </summary>
public class BienenManager {
    
    private static BienenManager manager; //Speichert sich selbst
    private List<Biene> BienenListe = new List<Biene>(); //Speichert alle Bienen
    private int anzahlBienen = 4; 


    public static BienenManager Instance
	{
		get
		{
            if (manager == null)
			{
                manager = new BienenManager();
			}
            return manager;
		}
              
	}


    /// <summary>  
    ///  Constructor
    /// </summary>
    public BienenManager()
    {
        if (manager != null)
        {
            Debug.Log("Trying to create two instances of singleton.");
            return;
        }
            manager = this;

    }

    internal bool GetReady() //Wenn sich alle fertigen Bienen eingetragen haben ist Spiel aus
    {
        bool result = false;
        if (BienenListe.Count == anzahlBienen)
        {
            result = true;
        }
        return result;
    }

    internal int GetFilling() //Gibt Glas die Anzahl der fertigen Bienen bzw Höhe des Füllstandes
    {
        int result = 0;
        if (BienenListe.Count >= 0 && BienenListe.Count <= anzahlBienen)
        {
            result = BienenListe.Count;
        }
        return result;
    }

    public void HoneyReady(Biene h)
    {
        if (!BienenListe.Contains(h)) //Überprüft das Biene noch nicht in Liste ist
        {
            BienenListe.Add(h); //Füt Biene hinzu
        }
    }

  
    /// <summary>  
    /// Beendet das Bienenspiel und lädt wieder Startspiel
    /// </summary>
    public void FinishGame()
    {
        BienenListe.Clear();
        SceneManager.LoadScene("Startspiel");
    }


    public void ResetManager()
    {
        foreach (Biene b in BienenListe)
        {
            b.ResetBee(); //Alle Bienen in Liste werden resetten
        }
        BienenListe.Clear(); //Leeren der Liste
    }
    
}
