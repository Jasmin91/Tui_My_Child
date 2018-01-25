using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    /// <summary>
    ///Spiel beendet 
    /// </summary>
    private bool finished = false;

    /// <summary>
    /// Speichert das Nuss-Korb-Objekt
    /// </summary>
    private BasketScript basket;

    /// <summary>
    /// Timer-Objekt
    /// </summary>
    private Timer T;



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
    ///  Methode erhöht den NutCounter
    /// </summary> 
    public void CollectNut()
    {
        this.NutCounter++;
        this.basket.UpdateBasket(NutCounter);
    }

    /// <summary>  
    ///  Methode, die anhand des Tiernamens das Tier in der entsprechenden Liste speichert, wenn es sich auf der Hütte befindet
    /// </summary> 
    public void VisitHut(string name)
    {
        //Hilfsvariable um zu überprüfen, ob Tier schon in Liste 
        bool AlreadyThere = false;
        
        foreach (AnimalController animal in AnimalList) {
            
            if (animal.name == name)
            {
                foreach (AnimalController a in VisitingHut)
                {
                    if (a.name == name)
                    {
                        AlreadyThere = true;

                    } 
                }
                if (!AlreadyThere)
                {
                    this.VisitingHut.Add(animal);
                }
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
        this.DeletedNutList.Add(nut.GetName());
    }
    
    
    /// <summary>
    /// Blendet alle Tiere aus
    /// </summary>
    public void HideAnimals()
    {
        foreach (AnimalController animal in AnimalList)
        {
            animal.GetComponent<Renderer>().enabled = false ;
            animal.BeSilent();
        }
    }

    /// <summary>
    /// Lässt das Tier etwas "sagen"
    /// </summary>
    /// <param name="animal">Tier</param>
    /// <param name="text">Auszugebender Text</param>
    public void LetAnimalSaySomething(AnimalController animal, string text)
    {
        
        if (animal != null)
        {
            animal.Speak(text);
        }
    }

    /// <summary>
    /// Lässt das Tier mit gesuchtem Namen etwas "sagen"
    /// </summary>
    /// <param name="name">Tiername</param>
    /// <param name="text">Auszugebender Text</param>
    public void LetAnimalSaySomething(string name, string text)
    {
        AnimalController animal = GetAnimalByName(name);
        if (animal != null)
        {
            animal.Speak(text);
        }
    }

    /// <summary>
    /// Lässt das Tier mit gesuchtem Namen eine bestimmte Zeit lang etwas "sagen"
    /// </summary>
    /// <param name="name">Tiername, des zu sprechenden Tieres</param>
    /// <param name="text">Auszugebender Text</param>
    public void LetAnimalSaySomething(string name, string text, float x)
    { AnimalController animal = GetAnimalByName(name);
        if (animal != null)
        {
            animal.Speak(text, x);
        }
    }

    /// <summary>
    /// Blendet die Sprechblase des Tieres aus
    /// </summary>
    /// <param name="name">Tiername, des zu sprechenden Tieres</param>
    public void LetAnimalBeQuiet(string name)
    {
        AnimalController animal = GetAnimalByName(name);
            if (animal != null) {
            animal.BeSilent();
        }
    }

    #region Funktionen um Spielstand zu speichern und wiederherzustellen


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
    ///  Leert die befüllten Listen
    /// </summary> 
    public void Reset()
    {
        AnimalPositionList.Clear();
        AnimalList.Clear();
        NutList.Clear();
        PortalList.Clear();
        VisitingHut.Clear();
        DeletedNutList.Clear();
        DeletedPortalList.Clear();
        NutCounter = 0;
    }

    /// <summary>  
    ///  Diese Methode stellt den alten Stand des Startspiels wieder her
    /// </summary> 
    public void GetOldState()
    {
            this.DeleteCollectedNutsInit();
            this.DeleteVisitedPortalsInit();
            this.SetOldAnimalPosition();
        
    }

    /// <summary>  
    ///  Methode füllt die Liste mit den Positionen der Tiere neu um sie später wieder dort platzieren zu können
    /// </summary> 
    public void FillAnimalPositionList()
    {
        AnimalPositionList.Clear();
        foreach (AnimalController animal in AnimalList)
        {
            AnimalPositionList.Add(new KeyValuePair<string, Vector3>(animal.name, animal.transform.position));
        }
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
                    this.GetPortalByName(s).animal.SetHasFood(true);
                    PortalList[i].DestroyObject(); //Löscht Portal, das in DeletedPortalList und PortalList zu finden ist

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

        foreach (string s in DeletedNutList)
        {
            int i = 0;
            bool ready = false;
            while (!ready)
            {
                if (NutList[i].GetName() == s)
                {
                    NutList[i].DestroyObject(); //Löscht Nuss, die in DeletedNutList und NutList zu finden ist
                    ready = true;
                }
                else if (i == NutList.Count - 1)
                {
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
    ///  Methode gibt an, ob alle Nüsse zu Beginn des Startspiels geladen wurden
    /// </summary> 
    public bool NutsComplete()
    {
        bool result = false;
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
        if (this.AnimalList.Count == PlayerCount)
        {
            result = true;
        }
        return result;
    }

    /// <summary>
    /// Methode überprüft, ob Manager komplett geladen wurde
    /// </summary>
    /// <returns>Bool, ob Manager komplett geladen wurde</returns>
    public bool LoadingComplete()
    {
        bool result = false;
        if (Manager.NutsComplete() && Manager.PortalsComplete() && Manager.AnimalsComplete()) //Checkt, ob alle Nüsse, Portale und Tiere geladen wurden
        {
            result = true;
        }
        return result;
    }

 

 

    #endregion

    #region Getter&Setter

    /// <summary>
    /// Gibt ein Tier mit dem gesuchten Namen zurück
    /// </summary>
    /// <param name="name">Tiername</param>
    /// <returns>Gesuchtes Tier</returns>
    public AnimalController GetAnimalByName(string name)
    {
        AnimalController result = null;

        if (AnimalList.Count == this.PlayerCount)
        {
            foreach (AnimalController animal in AnimalList)
            {
                if (animal.name == name)
                {
                    result = animal;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Gibt Portal mit gesuchtem Namen zurück
    /// </summary>
    /// <param name="name">Portalname</param>
    /// <returns>Gesuchtes Portal</returns>
    public PortalController GetPortalByName(string name)
    {
        PortalController result = null;

        foreach (PortalController portal in PortalList)
        {
            if (portal.name == name)
            {
                result = portal;
            }
        }
        return result;
    }

    /// <summary>
    /// Setzt den Nusskorb
    /// </summary>
    /// <param name="basket">Nusskorb</param>
    public void SetBasket(BasketScript basket)
    {
        if (this.basket == null)
        {
            this.basket = basket;
        }
    }

    /// <summary>
    /// Gibt alle verwendeten Fiducial IDs zurück
    /// </summary>
    /// <returns></returns>
    public int[] GetAllIDs(){

        int[] allIDs = new int[PlayerCount];
        int counter = 0;
        string result="IDs:";
        if (AnimalList.Count == allIDs.Length)
        {
            foreach (AnimalController animal in AnimalList)
            {
                allIDs[counter] = animal.MarkerID;
                result += ", " + animal.MarkerID;
                counter++;
            }
        }
         else{
            result = "Sende default-IDs";
            allIDs = new int[4]{0,1,2,3};
          }
        Debug.Log(result);
         return allIDs;
       }

    #endregion

    #region Adder


    /// <summary>  
    ///  Fügt eine Nuss zu Beginn des Spiels der NutList hinzu
    /// </summary> 
    public void AddNut(NutScript nut)
    {
        this.NutList.Add(nut);
    }


    /// <summary>  
    ///  Fügt ein Tier dem Tier-Array hinzu
    /// </summary> 
    public void AddAnimal(AnimalController animal)
    {
        if (animal != null&&!AnimalList.Contains(animal))
        {
            this.AnimalList.Add(animal);
        }
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

    public void SetTimer(Timer t)
    {
        this.T = t;
    }


    public Timer GetTimer()
    {
        return this.T;
    }
    #endregion


}
