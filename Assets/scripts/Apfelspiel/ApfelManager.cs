using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>  
///  Diese ApfelManagement-Klasse steuert und verbindet alle anderen Klassen des Apfelspiels
/// </summary>
public class ApfelManager {

    /// <summary>  
    ///  Erstellt eine Instanz der Manager-Klasse
    /// </summary> 
    private ManagerKlasse Manager;

    /// <summary>  
    ///  Erstellt eine Instanz der ApfelManager-Klasse
    /// </summary> 
    private static ApfelManager ms_Instance; 

    /// <summary>  
    ///  Speichert das Regen-Objekt
    /// </summary> 
    public regenController Rain {get; set; } 

    /// <summary>  
    ///  Speichert das Sonnen-Objekt
    /// </summary> 
    public sonneController Sun { get; set; } 

    /// <summary>  
    ///  der Wind weht gerade
    /// </summary> 
    private bool windBlowing = false; 

    /// <summary>  
    ///  Liste, die die Äpfel am Baum speichert
    /// </summary> 
    private List<appleScript> appleList = new List<appleScript>(); 


    public static ApfelManager Instance
	{
		get
		{
            if (ms_Instance == null)
			{
                ms_Instance = new ApfelManager();
			}
            return ms_Instance;
		}
                
	}


    /// <summary>  
    ///  Constructor
    /// </summary>
    public ApfelManager()
    {
        if (ms_Instance != null)
        {
            Debug.LogError("Trying to create two instances of singleton.");
            return;
        }
            ms_Instance = this;
        this.Manager = ManagerKlasse.Instance;

    }



    /// <summary>  
    /// Fügt einen Apfel dem Apfel-Array hinzu
    /// </summary>  
    /// <param name="apple">Dem Array hinzuzufügender Apfel</param>
    public void AddApple(appleScript apple)
    {
        this.appleList.Add(apple);
    }

    /// <summary>  
    /// Methode, die das Fallen eines Apfels ermöglicht
    /// </summary> 
        public void EinApfelFaellt()
    {
       this.SetWindBlowing(true);
       this.FallApple();
       this.SetWindBlowing(false); //Wird wieder auf false gesetzt, damit Fiducial erneut gezeigt werden muss, damit der nächste Apfel fällt
    }

    /// <summary>  
    ///Lässt jeden Apfel im Array wachsen
    /// </summary>
    /// <param name="rainDuration">Dauer, die es bereits regnet</param>
    public void ApfelWachsenLassen(float rainDuration)
    {
        foreach (appleScript apfel in appleList)
        {
            apfel.GrowingApple(rainDuration);
        }
    }


    /// <summary>  
    ///Lässt jeden Apfel im Array reifen
    /// </summary>
    /// <param name="sunDuration">Dauer, die Sonne bereits scheint</param>
    public void ApfelReifenLassen(float sunDuration)
    {
        foreach (appleScript apfel in appleList)
        {
            apfel.RipingApple(sunDuration);
        }
    }



    /// <summary>  
    ///Lässt jeden Apfel im Array fallen
    /// </summary>
    private void FallApple()
    {
        foreach (appleScript apfel in appleList)
        {
            if (!apfel.GetFallen()) //Findet einen nicht gefallenen Apfel
            {
                apfel.FallableApple(); //lässt ihn fallen
                SetWindBlowing(false); //Wird wieder auf false gesetzt, damit Fiducial erneut gezeigt werden muss, damit der nächste Apfel fällt
                break; //bricht die Schleife ab, damit nur ein Apfel fällt
            }

        }
    }

    /// <summary>  
    /// Beendet das Apfelspiel und lädt wieder Startspiel
    /// </summary>
    public void FinishGame()
    {
        appleList.Clear();
        SceneManager.LoadScene("Startspiel");
    }

    #region Getter&Setter

    /// <summary>  
    ///  Setter gut bool wind
    /// </summary>
    /// <param name="wind">Bool, ob Wind gerade weht.</param>
    public void SetWindBlowing(bool wind)
    {
        this.windBlowing = wind;

    }

    /// <summary>  
    ///  Getter gut bool wind
    /// </summary>
    /// <returns>Bool, ob Wind gerade weht</returns>
    public bool GetWindBlowing()
    {
        return this.windBlowing;
    }

    /// <summary>  
    ///  Getter für Manager
    /// </summary>
    public ManagerKlasse GetManager()
    {
        return Manager;
    }

    /// <summary>  
    ///  Methode testet, ob Sonne und Regen-Fiducials lang genug gezeigt wurden und damit die Äpfel bereit zum Ernten sind
    /// </summary>
    /// <return> bool, ob Äpfel bereit für Ernte sind </return>
    public bool GetHarvestingReady()
    {
        bool result = false;
        if (this.Sun.GetSunReady() && this.Rain.GetRainReady())
        {
            result = true;
        }
        return result;
    }

    #endregion

}
