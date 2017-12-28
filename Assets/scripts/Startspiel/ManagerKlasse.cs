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
    private List<NutScript> NutList = new List<NutScript>(); //Speichert alle Nüsse
    private List<PortalController> PortalList = new List<PortalController>(); //Speichert alle Portale
    ICollection<KeyValuePair<string, Vector3>> AnimalPositionList = new Dictionary<string, Vector3>(); //Speichert die Position aller Tiere
    private List<string> DeletedNutList = new List<string>(); //Speichert alle Namen der gelöschten Nüsse
    private List<string> DeletedPortalList = new List<string>(); //Speichert alle Namen der bereits betretenen Portale
    private List<AnimalController> VisitingHut = new List<AnimalController>();
    //GUIText text = 
    public int PlayerCount = 4;
    public int NutCount = 20;



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

    public void getOldState()
    {
        Debug.Log("Versucht alten Stand wiederherzustellen");
        this.deleteCollectedNutsInit();
        this.deleteVisitedPortalsInit();
        this.setOldAnimalPosition();
        
    }
    //Fügt ein Tier dem Tier-Array hinzu
    public void addAnimal(AnimalController animal)
    {
        this.AnimalArray.Add(animal);
    }

    

    private void deleteVisitedPortalsInit()
    {

        foreach (string s in DeletedPortalList)
        {
            int i = 0;
            bool ready = false;
            while (!ready)
            {
                if (PortalList[i].name == s)
                {
                    Debug.Log(s + " soll gelöscht werden");
                   PortalList[i].destroyObject();
                    ready = true;
                }
                else if (i == PortalList.Count - 1)
                {
                    ready = true;
                }

                i++;
            }
        }
    }

    private void deleteCollectedNutsInit()
    {

        foreach(string s in DeletedNutList){
            int i = 0;
            bool ready = false;
            while (!ready)
            {
                    if (NutList[i].getName() == s)
                    {
                        NutList[i].destroyObject();
                        ready = true;
                    }
                    else if(i == NutList.Count-1) {
                          ready = true;
                        }

                    i++;
               
            }
        }
    }

    private void setOldAnimalPosition()
    {

        foreach (KeyValuePair<string, Vector3> ap in AnimalPositionList)
        {
            int i = 0;
            bool ready = false;
            while (!ready)
            {
                if (this.AnimalArray[i].name == ap.Key)
                {
                    AnimalArray[i].transform.position = ap.Value;
                    ready = true;
                }
                else if (i == AnimalArray.Count - 1)
                {
                    ready = true;
                }

                i++;

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
    }

    public void addPortal(PortalController portal)
    {
        this.PortalList.Add(portal);
    }

    public void addVisitedPortal(PortalController portal)
    {
        this.DeletedPortalList.Add(portal.name);
    }

    public void visitHut(string name)
    {
        Debug.Log("In visitingHut mit " + name);
        foreach (AnimalController animal in AnimalArray) {
            Debug.Log("Versuche " + animal.name + " in die Besuchsliste aufzunehmen");
            if (animal.name == name)
            {
                this.VisitingHut.Add(animal);
                Debug.Log(animal.name + " ist drin");
            }
        }
        //Debug.Log("visitingHut.Count:"+VisitingHut.Count+" ")
    }
    public void leaveHut(string name)
    {
        foreach (AnimalController animal in AnimalArray)
        {
            if (animal.name == name)
            {
                this.VisitingHut.Remove(animal);
            }
        }
    }

    public List<AnimalController> getVistors()
    {
        return VisitingHut;
    }

    public bool getFoundAllFood()
    {
        bool result = false;

        if (this.DeletedPortalList.Count == PlayerCount)
        {
            result = true;
        }

        return result;
    }

    public void removeNut(NutScript nut)
    {
        this.NutList.Remove(nut);


        this.DeletedNutList.Add(nut.getName());
    }

    public bool nutsComplete()
    {
        bool result = false;
        Debug.Log("Count:"+NutList.Count);
        if (this.NutList.Count == NutCount)
        {
            result = true;
        }
        return result;
    }

    public bool portalsComplete()
    {
        bool result = false;
        Debug.Log("Count:" + PortalList.Count);
        if (this.PortalList.Count == PlayerCount)
        {
            result = true;
        }
        return result;
    }

    public bool AnimalsComplete()
    {
        bool result = false;
        Debug.Log("Count:" + AnimalArray.Count);
        if (this.AnimalArray.Count == PlayerCount)
        {
            result = true;
        }
        return result;
    }
    

    public void clearScene()
    {
       
        fillAnimalPositionList();
        AnimalArray.Clear();
        NutList.Clear();
        PortalList.Clear();
        VisitingHut.Clear();
    }

    public void fillAnimalPositionList()
    {
        AnimalPositionList.Clear();
        foreach(AnimalController animal in AnimalArray)
        {
              AnimalPositionList.Add(new KeyValuePair<string, Vector3>(animal.name, animal.transform.position));
        }
    }
}
