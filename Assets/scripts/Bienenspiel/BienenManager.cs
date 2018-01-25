using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>  
///  Diese BienenManagement-Klasse steuert und verbindet alle anderen Klassen des Bienenspiels
/// </summary>
public class BienenManager {
    
    private static BienenManager manager;
    private List<Biene> BienenListe = new List<Biene>();
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

    internal bool GetReady()
    {
        bool result = false;
        if (BienenListe.Count == anzahlBienen)
        {
            result = true;
        }
        return result;
    }

    internal int GetFilling()
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
        if (!BienenListe.Contains(h))
        {
            BienenListe.Add(h);
        }
    }

  


    /// <summary>  
    /// Beendet das Bienenspiel und lädt wieder Startspiel
    /// </summary>
    public void FinishGame()
    {

        foreach (Biene b in BienenListe)
        {
           // b.ResetBee();
        }
        BienenListe.Clear();
        SceneManager.LoadScene("Startspiel");
    }



    #region Getter&Setter

    public void ResetManager()
    {
        foreach (Biene b in BienenListe)
        {
            b.ResetBee();
        }
        BienenListe.Clear();
    }

    #endregion

}
