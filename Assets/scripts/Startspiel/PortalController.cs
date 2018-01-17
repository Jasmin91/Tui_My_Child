

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Klasse, welche die Funktion eines Portal-Objektes Steuert
/// </summary>

public class PortalController : MonoBehaviour
{
    /// <summary>
    ///Erstellt eine Instanz der Manager-Klasse 
    /// </summary>
    private ManagerKlasse Manager; 

    /// <summary>
    /// Bool, ob Portal bereits besucht
    /// </summary>
    public bool done = false;

    /// <summary>
    /// Zugehöriges Tier
    /// </summary>
    public AnimalController animal;


    void Awake()
    {
        this.Manager = ManagerKlasse.Instance;
     }

    void Start()
    {
        this.Manager.AddPortal(this);
    }

    /// <summary>
    ///Collider, der erkennt ob Tier und Portal kollidieren
    /// </summary>
    /// <param name="col">Collider</param>
    void OnTriggerEnter2D(Collider2D col)
    {
          
          if (col.gameObject.name == this.animal.name)
          {
              this.Manager.AddVisitedPortal(this);
              Manager.ClearScene();
            
            SceneManager.LoadScene(this.ChooseGame(animal.name));
            animal.SetHasFood(true);
        }
        else
            {
            Manager.LetAnimalSaySomething(col.gameObject.name, "Ich möchte meinem Freund " + animal.GetNickname() + " das Essen nicht wegnehmen!");
            
            }
    }

    /// <summary>
    /// Wählt den richtigen Spielnamen je nach Tier aus
    /// </summary>
    /// <param name="animalname">Tiername</param>
    /// <returns>Spielname</returns>
    private String ChooseGame(String animalname)
    {
        String result = "Startspiel";
        switch (animal.name)
        {
            case "horse":
               // result = "Apfelspiel2";
                break;
            case "bear":
                result = "Startspiel";
                break;
            case "dog":
               //result = "Knochenspiel";
                break;
            case "rabbit":
                //result = "Ruebenspiel";
                break;
         }
        
        return result;
    }

    /// <summary>
    /// Methode reguliert, was passiert, wenn Portal verlassen wird
    /// </summary>
    /// <param name="col">Collider</param>
    void OnTriggerExit2D(Collider2D col)
    {

        AnimalController animal = Manager.GetAnimalByName(col.name);
        if (animal != null)
        {
            animal.BeSilent();
        }
    }

    /// <summary>
    /// Löscht das betretene Portal
    /// </summary>
    public void DestroyObject()
    {
        Destroy(this.gameObject); //Zerstören des betretenen Portals
    }
}

