using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  
///  Diese Management-Klasse steuert und verbindet alle anderen Klassen
/// </summary>
public class ManagerKlasse {

    private static ManagerKlasse Manager; //Erstellt eine Instanz der Manager-Klasse
    private List<AnimalController> AnimalArray = new List<AnimalController>(); //Array, dass die Tiere speichert
    private int NutCounter; //Zählt die gesammelten Nüsse
    private List<NutScript> NutList = new List<NutScript>(); //Speichert alle Nüsse
    private State SceneState;
    private SaveLoad SL;

public static ManagerKlasse Instance
	{
		get
		{
            if (Manager == null)
			{
                Manager = new ManagerKlasse();
			}
            return Manager;
		}
                
	}


    public ManagerKlasse()
    {
        if (Manager != null)
        {
            Debug.LogError("Trying to create two instances of singleton.");
            return;
        }
        Manager = this;
        Debug.Log("Sooft bin ich hier drin!");

    }

    //Fügt ein Tier dem Tier-Array hinzu
    public void addAnimal(AnimalController animal)
    {
        this.AnimalArray.Add(animal);
    }


    public void addSceneState(State state)
    {
        this.SceneState = state;
    }

    public void addSaveLoad(SaveLoad sl)
    {
        this.SL = sl;
    }


    public void collectNut()
    {
        this.NutCounter++;
        Debug.Log("Gesammelte Nüsse: " + NutCounter);
    }
    public void addNut(NutScript nut)
    {
        this.NutList.Add(nut);
    }

    public void removeNut(NutScript nut)
    {
        this.NutList.Remove(nut);
    }

    public State getSavedState()
    {

        return this.SceneState;
    }

    public SaveLoad getSaveLoad()
    {

        return this.SL;
    }

    public void Save()
    {
        this.SL.Save(SceneState);
    }
}
