using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

/// <summary>  
///  Diese Management-Klasse steuert und verbindet alle anderen Klassen
/// </summary>
public class ManagerKlasse {

    /// <summary> Instanz der Manager-Klasse </summary>
    private static ManagerKlasse Manager; //Erstellt eine Instanz der Manager-Klasse

    /// <summary> Liste, die die Tiere speichert </summary>
    private List<AnimalController> AnimalList = new List<AnimalController>();
    
    /// <summary> Liste, die alle Nüsse speichert </summary>
    private List<NutScript> NutList = new List<NutScript>(); 

    /// <summary> Liste, die alle Portale speichert </summary>
    private List<PortalController> PortalList = new List<PortalController>(); 

    /// <summary> Dictionary, das die Position aller Tiere speichert </summary>
    ICollection<KeyValuePair<string, Vector3>> AnimalPositionList = new Dictionary<string, Vector3>(); 

    /// <summary> Liste, die alle bereits gelöschten Nüsse speichert </summary>
    private List<string> DeletedNutList = new List<string>();

    /// <summary> Liste, die alle bereits betretenen Portale speichert </summary>
    private List<string> DeletedPortalList = new List<string>(); 

    /// <summary> Liste, die alle Tiere speichert, die gerade auf der Hütte sind </summary>
    private List<AnimalController> VisitingHut = new List<AnimalController>(); 

    /// <summary> Speichert die Anzahl der Spieler </summary>
    public int PlayerCount = 4; 

    /// <summary> Speichert die Anzahl der Nüsse im Spiel </summary>
    public int NutCount = 20;

    /// <summary> Speichert die Anzahl der gesammelten Nüsse </summary>
    public int NutCounter = 0;

    public Konsole console;

  



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

    }

    /// <summary>  
    ///  Diese Methode stellt den alten Stand des Startspiels wieder her
    /// </summary> 
    public void GetOldState()
    {
        //console.AddText("Versuche zu Updaten");
        this.DeleteCollectedNutsInit();
        this.DeleteVisitedPortalsInit();
        this.SetOldAnimalPosition();
    }

    /// <summary>  
    ///  Fügt ein Tier dem Tier-Array hinzu
    /// </summary> 
    public void AddAnimal(AnimalController animal)
    {
        this.AnimalList.Add(animal);
    }

    /// <summary>  
    ///  Löscht alle bereits von den richtigen Tieren besuchten Portale
    /// </summary> 
    private void DeleteVisitedPortalsInit()
    {

        foreach (string s in DeletedPortalList) 
        {
            int i = 0;
            bool ready = false;
            while (!ready)
            {
                if (PortalList[i].name == s)
                {
                   PortalList[i].destroyObject(); //Löscht Portal, das in DeletedPortalList und PortalList zu finden ist
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

    /// <summary>  
    ///  Löscht alle bereits gesammelten Nüsse
    /// </summary> 
    private void DeleteCollectedNutsInit()
    {

        foreach(string s in DeletedNutList){
            int i = 0;
            bool ready = false;
            while (!ready)
            {
                    if (NutList[i].getName() == s)
                    {
                        NutList[i].destroyObject(); //Löscht Nuss, die in DeletedNutList und NutList zu finden ist
                    ready = true;
                    }
                    else if(i == NutList.Count-1) {
                          ready = true;
                        }

                    i++;
               
            }
        }
    }

    /// <summary>  
    ///  Setzt die Tiere auf ihre alte Position, welche in AnimalPositionList gespeichert ist
    /// </summary> 
    private void SetOldAnimalPosition()
    {

        foreach (KeyValuePair<string, Vector3> ap in AnimalPositionList)
        {
            int i = 0;
            bool ready = false;
            while (!ready)
            {
                if (this.AnimalList[i].name == ap.Key)
                {
                    AnimalList[i].transform.position = ap.Value;
                    ready = true;
                }
                else if (i == AnimalList.Count - 1)
                {
                    ready = true;
                }

                i++;

            }
        }
    }

    /// <summary>  
    ///  Methode erhöht den NutCounter
    /// </summary> 
    public void CollectNut()
    {
        this.NutCounter++;
    }

    /// <summary>  
    ///  Fügt eine Nuss zu Beginn des Spiels der NutList hinzu
    /// </summary> 
    public void AddNut(NutScript nut)
    {
        Debug.Log(nut.name + " wird gezählt");
        this.NutList.Add(nut);
    }

    /// <summary>  
    ///  Fügt ein Portal zu Beginn des Spiels der PortalList hinzu
    /// </summary> 
    public void AddPortal(PortalController portal)
    {
        this.PortalList.Add(portal);
    }

    /// <summary>  
    ///  Fügt ein Portal der Liste hinzu, wenn es vom richtigen Tier besucht wurde
    /// </summary> 
    public void AddVisitedPortal(PortalController portal)
    {
        this.DeletedPortalList.Add(portal.name);
    }

    /// <summary>  
    ///  Methode, die anhand des Tiernamens das Tier in der entsprechenden Liste speichert, wenn es sich auf der Hütte befindet
    /// </summary> 
    public void VisitHut(string name)
    {
        foreach (AnimalController animal in AnimalList) {
            if (animal.name == name)
            {
                this.VisitingHut.Add(animal);
            }
        }
    }
    /// <summary>  
    ///  Methode, die anhand des Tiernamens das Tier aus der entsprechenden Liste löscht, wenn es sich nicht mehr auf der Hütte befindet
    /// </summary> 
    public void LeaveHut(string name)
    {
        foreach (AnimalController animal in AnimalList)
        {
            if (animal.name == name)
            {
                this.VisitingHut.Remove(animal);
            }
        }
    }

    /// <summary>  
    ///  Methode gibt die Liste aller Tiere zurück, die aktuell auf der Hütte sind
    /// </summary> 
    public List<AnimalController> GetVistors()
    {
        return VisitingHut;
    }

    /// <summary>  
    ///  Methode gibt an, ob alle Tiere ihr Essen gefunden haben
    /// </summary> 
    public bool GetFoundAllFood()
    {
        bool result = false;

        if (this.DeletedPortalList.Count == PlayerCount)
        {
            result = true;
        }

        return result;
    }

    /// <summary>  
    ///  Methode löscht die Nuss aus der Nussliste und fügt sie der Liste für gelöschte Nüsse hinzu
    /// </summary> 
    public void RemoveNut(NutScript nut)
    {
        this.NutList.Remove(nut);
        this.DeletedNutList.Add(nut.getName());
    }

    /// <summary>  
    ///  Methode gibt an, ob alle Nüsse zu Beginn des Startspiels geladen wurden
    /// </summary> 
    public bool NutsComplete()
    {
        bool result = false;
        Debug.Log("Count:"+NutList.Count);
        if (this.NutList.Count == NutCount)
        {
            result = true;
        }
        return result;
    }

    /// <summary>  
    ///  Methode gibt an, ob alle Portale zu Beginn des Startspiels geladen wurden
    /// </summary> 
    public bool PortalsComplete()
    {
        bool result = false;
        Debug.Log("Count:" + PortalList.Count);
        if (this.PortalList.Count == PlayerCount)
        {
            result = true;
        }
        return result;
    }

    /// <summary>  
    ///  Methode gibt an, ob alle Tiere zu Beginn des Startspiels geladen wurden
    /// </summary> 
    public bool AnimalsComplete()
    {
        bool result = false;
        Debug.Log("Count:" + AnimalList.Count);
        if (this.AnimalList.Count == PlayerCount)
        {
            result = true;
        }
        return result;
    }


    /// <summary>  
    ///  Leert die befüllten Listen und speichert die aktuellen Positionen der Tiere
    /// </summary> 
    public void ClearScene()
    {
       
        FillAnimalPositionList();
        AnimalList.Clear();
        NutList.Clear();
        PortalList.Clear();
        VisitingHut.Clear();
    }

    /// <summary>  
    ///  Methode füllt die Liste mit den Positionen der Tiere neu
    /// </summary> 
    public void FillAnimalPositionList()
    {
        AnimalPositionList.Clear();
        foreach(AnimalController animal in AnimalList)
        {
              AnimalPositionList.Add(new KeyValuePair<string, Vector3>(animal.name, animal.transform.position));
        }
    }

    public void setConsole(Konsole c)
    {
        if (this.console == null)
        {
            this.console = c;
        }
    }

    public void deleteAnimals()
    {
        foreach (AnimalController animal in AnimalList)
        {
            animal.GetComponent<Renderer>().enabled = false ;
        }
    }



}
