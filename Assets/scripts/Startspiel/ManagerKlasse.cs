using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

/// <summary>  
///  Diese Management-Klasse steuert und verbindet alle anderen Klassen
/// </summary>
public class ManagerKlasse {

    private static ManagerKlasse Manager; //Erstellt eine Instanz der Manager-Klasse
    private List<AnimalController> AnimalArray = new List<AnimalController>(); //Array, dass die Tiere speichert
    private int NutCounter; //Zählt die gesammelten Nüsse
    private List<NutScript> NutList = new List<NutScript>(); //Speichert alle Namen aller Nüsse
    private List<string> DeletedNutList = new List<string>(); //Speichert alle Namen der gelöschten Nüsse
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
        this.SL = new SaveLoad();
        
        this.SceneState = new State();
        Debug.Log("Sooft bin ich hier drin!");

    }

    public void getOldState()
    {
        Debug.Log("Versucht alten Stand wiederherzustellen");
        this.deleteCollectedNutsInit();
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

    private void deleteCollectedNutsInit()
    {
        DeletedNutList.Add("nut1");
        DeletedNutList.Add("nut2");
        DeletedNutList.Add("nut3");
        DeletedNutList.Add("nut4");
        DeletedNutList.Add("nut5");
        DeletedNutList.Add("nut6");
        DeletedNutList.Add("nut7");
        DeletedNutList.Add("nut8");

        Debug.Log("Losgehts!"+DeletedNutList.Count);
        foreach(string s in DeletedNutList){
            Debug.Log("Das ist eine gelöschte Nuss" +s );
            foreach (NutScript ns in NutList)
            {
                Debug.Log("Ich überprüfe " + ns.getName());
                if (ns.getName() == s)
                {
                    Debug.Log("Ich versuche " + s + " zu löschen");
                    ns.collectNut();
                    
                }
                break;
            }
        }
    }

    public void collectNut()
    {
        this.NutCounter++;
        
        Debug.Log("Gesammelte Nüsse: " + NutCounter);
    }
    public void addNut(NutScript nut)
    {
        this.NutList.Add(nut);
        SceneState.updateNutNames(DeletedNutList);
    }

    public void removeNut(NutScript nut)
    {
        this.NutList.Remove(nut);
        this.DeletedNutList.Add(nut.getName());
        //SceneState.updateNutNames(DeletedNutList);
    }

    public bool nutsComplete()
    {
        bool result = false;
        if (this.NutList.Count == 20)
        {
            result = true;
        }
        return result;
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
