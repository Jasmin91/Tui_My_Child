using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  
///  Diese Management-Klasse steuert und verbindet alle anderen Klassen
/// </summary>
public class Management {

    private static Management ms_Instance; //Erstellt eine Instanz der Manager-Klasse
    public regenController Regen {get; set; } //Speichert das Regen-Objekt
    public sonneController Sonne { get; set; } //Speichert das Sonnen-Objekt
    private bool rainReady; //es hat genug geregnet
    private bool sunReady; //die Sonne hat genug geschienen
    private bool windBlowing; //der Wind weht gerade
    private float rainDuration = 4;
    private List<apfelScript> apfelArray = new List <apfelScript>(); //Array, dass die (roten) Äpfel am Baum speichert
    float currCountdownValue;

    public static Management Instance
	{
		get
		{
            if (ms_Instance == null)
			{
                ms_Instance = new Management();
			}
            return ms_Instance;
		}
                
	}



    public Management()
    {
        if (ms_Instance != null)
        {
            Debug.LogError("Trying to create two instances of singleton.");
            return;
        }
            ms_Instance = this;
        
    }

    void Start () {
        this.setWindBlowing(false);
        this.setRainReady(false);
        this.setSunReady(false);
    }

    // Update is called once per frame
    public void Update()
    {
      //  getHarvestingReady();
        
    }


	public void setWindBlowing(bool wind){
        this.windBlowing = wind;

    }

    public bool getWindBlowing()
    {
        return this.windBlowing;
    }

    public void setRainReady(bool rain)
    {
        this.rainReady = rain;

    }

    public bool getRainReady()
    {
        return this.rainReady;
    }

    public void setSunReady(bool sun)
    {
        this.sunReady = sun;

    }

    public bool getSunReady()
    {
        return this.sunReady;
    }

    //Methode testet, ob Sonne und Regen-Fiducials lang genug gezeigt wurden und damit die Äpfel bereit zum Ernten sind
    public bool getHarvestingReady(){
        bool result = false;
		if (getSunReady()&&getRainReady()) {
            result = true;
        }
        return result;
	}

    //Fügt einen Apfel dem Apfel-Array hinzu
    public void addApple(apfelScript apple)
    {
        this.apfelArray.Add(apple);      
    }

    //Methode, die das Fallen eines Apfels ermöglicht
    public void einApfelFaellt()
    {
       this.setWindBlowing(true);
       this.fallApple();
       this.setWindBlowing(false);//Wird wieder auf false gesetzt, damit Fiducial erneut gezeigt werden muss, damit der nächste Apfel fällt
    }

    //Lässt den Apfel fallen
    private void fallApple()
    {
        foreach (apfelScript apfel in apfelArray)
        {
            if (!apfel.GetFallen()) //Findet einen nicht gefallenen Apfel
            {
                apfel.FallingApple(); //lässt ihn fallen
                setWindBlowing(false); //Wird wieder auf false gesetzt, damit Fiducial erneut gezeigt werden muss, damit der nächste Apfel fällt
                break; //bricht die Schleife ab, damit nur ein Apfel fällt
            }
            
        }
    }

    public float getRainDuration()
    {
        return this.rainDuration;
    }

    public void setRainDuration(float duration)
    {
        this.rainDuration = duration;
    }


}
