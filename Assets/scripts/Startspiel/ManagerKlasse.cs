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

    //Fügt ein Tier dem Tier-Array hinzu
    public void addAnimal(AnimalController animal)
    {
        this.AnimalArray.Add(animal);
    }



    public ManagerKlasse()
    {
        if (Manager != null)
        {
            Debug.LogError("Trying to create two instances of singleton.");
            return;
        }
            Manager = this;
        
    }


    public void addNut()
    {
        this.NutCounter++;
        Debug.Log("Gesammelte Nüsse: " + NutCounter);
    }

}
