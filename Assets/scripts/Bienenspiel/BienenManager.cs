using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>  
///  Diese BienenManagement-Klasse steuert und verbindet alle anderen Klassen des Bienenspiels
/// </summary>
public class BienenManager {
    /// <summary>
    /// Der Manager für die BienenManager-Klasse wird definiert
    /// </summary>
    private static BienenManager manager; 
    /// <summary>
    /// Liste in der die Bienen gespeichert werden wird erstellt
    /// </summary>
    private List<Biene> BienenListe = new List<Biene>(); 
    private int anzahlBienen = 4; 

    /// <summary>
    /// Überprüft ob Manager vorhanden ist und erzeugt diesen falls nicht vorhanden
    /// </summary>
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
    /// <summary>
    /// Wenn die Anzahl der Bienen identsich mit der eingetragenen Anzahl der Bienen ist wird das Spiel beendet 
    /// </summary>
    /// <returns></returns>
    internal bool GetReady() 
    {
        bool result = false;
        if (BienenListe.Count == anzahlBienen)
        {
            result = true;
        }
        return result;
    }
    /// <summary>
    /// Überträgt dem Glas die Anzahl der fertigen Bienen und bestimmt somit den Füllstand für das Glas 
    /// </summary>
    /// <returns></returns>
    internal int GetFilling() 
    {
        int result = 0;
        if (BienenListe.Count >= 0 && BienenListe.Count <= anzahlBienen)
        {
            result = BienenListe.Count;
        }
        return result;
    }
    /// <summary>
    /// Wenn eine Biene nicht in der Liste ist, wird diese hinzugefügt
    /// </summary>
    /// <param name="h"></param>
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
        BienenListe.Clear();
        SceneManager.LoadScene("Startspiel");
    }

    /// <summary>
    /// Für das nächste Spiel werden die Bienen resettet und die Liste der Bienen wird gelöscht
    /// </summary>
    public void ResetManager()
    {
        foreach (Biene b in BienenListe)
        {
            b.ResetBee(); 
        }
        BienenListe.Clear(); 
    }
    
}
