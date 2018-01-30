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
    private static ApfelManager Ms_Instance; 

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
    private bool WindBlowing = false; 

    /// <summary>  
    ///  Liste, die die Äpfel am Baum speichert
    /// </summary> 
    private List<AppleScript> AppleList = new List<AppleScript>(); 


    public static ApfelManager Instance
	{
		get
		{
            if (Ms_Instance == null)
			{
                Ms_Instance = new ApfelManager();
			}
            return Ms_Instance;
		}
                
	}


    /// <summary>  
    ///  Constructor
    /// </summary>
    public ApfelManager()
    {
        if (Ms_Instance != null)
        {
            Debug.LogError("Trying to create two instances of singleton.");
            return;
        }
            Ms_Instance = this;
        this.Manager = ManagerKlasse.Instance;

    }



    /// <summary>  
    /// Fügt einen Apfel dem Apfel-Array hinzu
    /// </summary>  
    /// <param name="apple">Dem Array hinzuzufügender Apfel</param>
    public void AddApple(AppleScript apple)
    {

        if (AppleList.Count == 4)
        {
            AppleList.Clear();
        }
        this.AppleList.Add(apple);
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
    ///Lässt jeden Apfel im Array fallen
    /// </summary>
    private void FallApple()
    {
        foreach (AppleScript apfel in AppleList)
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
        AppleList.Clear();
        SceneManager.LoadScene("Startspiel");
    }

    #region Getter&Setter

    /// <summary>  
    ///  Setter gut bool wind
    /// </summary>
    /// <param name="wind">Bool, ob Wind gerade weht.</param>
    public void SetWindBlowing(bool wind)
    {
        this.WindBlowing = wind;

    }

    /// <summary>  
    ///  Getter gut bool wind
    /// </summary>
    /// <returns>Bool, ob Wind gerade weht</returns>
    public bool GetWindBlowing()
    {
        return this.WindBlowing;
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
