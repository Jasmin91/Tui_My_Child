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
    private List<Biene> honigListe = new List<Biene>();
    private int anzahlBienen = 3;


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
        if (honigListe.Count == anzahlBienen)
        {
            result = true;
        }
        return result;
    }

    internal int GetFilling()
    {
        int result = 0;
        if (honigListe.Count >= 0 && honigListe.Count <= anzahlBienen)
        {
            result = honigListe.Count;
        }
        return result;
    }

    public void HoneyReady(Biene h)
    {
        if (!honigListe.Contains(h))
        {
            honigListe.Add(h);
        }

        if (honigListe.Count == anzahlBienen)
        {
            Debug.Log("Alle Gläser sind voll");
        }
    }

  


    /// <summary>  
    /// Beendet das Bienenspiel und lädt wieder Startspiel
    /// </summary>
    public void FinishGame()
    {
        SceneManager.LoadScene("Startspiel");
    }



    #region Getter&Setter

    

    #endregion

}
