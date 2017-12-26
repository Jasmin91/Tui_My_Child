using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  
///  Diese Management-Klasse steuert und verbindet alle anderen Klassen
/// </summary>
public class ApfelManager {

    private static ApfelManager ms_Instance; //Erstellt eine Instanz der Manager-Klasse
    public regenController Rain {get; set; } //Speichert das Regen-Objekt
    public sonneController Sun { get; set; } //Speichert das Sonnen-Objekt
    private bool windBlowing = false; //der Wind weht gerade
    private List<appleScript> appleArray = new List<appleScript>(); //Array, dass die Äpfel am Baum speichert


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



    public ApfelManager()
    {
        if (ms_Instance != null)
        {
            Debug.LogError("Trying to create two instances of singleton.");
            return;
        }
            ms_Instance = this;
        
    }


	public void setWindBlowing(bool wind){
        this.windBlowing = wind;

    }

    public bool getWindBlowing()
    {
        return this.windBlowing;
    }


    //Methode testet, ob Sonne und Regen-Fiducials lang genug gezeigt wurden und damit die Äpfel bereit zum Ernten sind
    public bool getHarvestingReady(){
        bool result = false;
		if (this.Sun.getSunReady()&&this.Rain.getRainReady()) {
            result = true;
        }
        return result;
	}


    //Fügt einen Apfel dem Apfel-Array hinzu
    public void addApple(appleScript apple)
    {
        this.appleArray.Add(apple);
    }

    //Methode, die das Fallen eines Apfels ermöglicht
    public void einApfelFaellt()
    {
       this.setWindBlowing(true);
       this.fallApple();
       this.setWindBlowing(false);//Wird wieder auf false gesetzt, damit Fiducial erneut gezeigt werden muss, damit der nächste Apfel fällt
    }

    //Lässt jeden Apfel im Array wachsen
    public void ApfelWachsenLassen(float rainDuration)
    {
        foreach (appleScript apfel in appleArray)
        {
            apfel.GrowingApple(rainDuration);
        }
    }

    //Lässt jeden Apfel im Array reifen
    public void ApfelReifenLassen(float sunDuration)
    {
        foreach (appleScript apfel in appleArray)
        {
            apfel.RipingApple(sunDuration);
        }
    }


    //Lässt jeden Apfel im Array fallen
    private void fallApple()
    {
        foreach (appleScript apfel in appleArray)
        {
            if (!apfel.GetFallen()) //Findet einen nicht gefallenen Apfel
            {
                apfel.FallingApple(); //lässt ihn fallen
                setWindBlowing(false); //Wird wieder auf false gesetzt, damit Fiducial erneut gezeigt werden muss, damit der nächste Apfel fällt
                break; //bricht die Schleife ab, damit nur ein Apfel fällt
            }

        }
    }
 

}
