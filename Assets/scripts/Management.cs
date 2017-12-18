using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Management {

    private static Management ms_Instance;
    public regenController Regen {get; set; }
    public sonneController Sonne { get; set; }
    private bool rainReady;
    private bool sunReady;
    private bool windBlowing;
    private float rainDuration = 4;
    private List<apfelScript> apfelArray = new List <apfelScript>();
    float currCountdownValue;

    public static Management Instance
	{
		get
		{
            Debug.Log("Management wird instanziiert");
            if (ms_Instance == null)
			{
                ms_Instance = new Management();
			}
            Debug.Log("Management Instance neu erstellt:" + ms_Instance);
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
        getHarvestingReady();

       
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

    public bool getHarvestingReady(){
        bool result = false;
		if (getSunReady()&&getRainReady()) {
            result = true;
        }

        return result;
	}

    public void addApple(apfelScript apple)
    {
        this.apfelArray.Add(apple);
        Debug.Log("Fügen wir einen Apfel hinzu?");
        int i = 0;
        foreach (apfelScript apfel in apfelArray)
        {
            Debug.Log(i +". Apfel wird hinzugefügt : " + apfel);
        }
        
    }

    public void einApfelFaellt()
    {
       this.setWindBlowing(true);
       this.fallApple();
       this.setWindBlowing(false);
    }
    private void fallApple()
    {
        Debug.Log("Fällt ein Apfel?");

        foreach (apfelScript apfel in apfelArray)
        {
            Debug.Log("In der foreach Schleife");
            if (!apfel.getFallen())
            {
                Debug.Log("Aha! Ein nicht gefallener Apfel. Runter damit.");
                apfel.FallingApple();
                setWindBlowing(false);
                break;
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
