

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
    private bool done = false;

    /// <summary>
    /// Zugehöriges Tier
    /// </summary>
    public AnimalController animal;

    /// <summary>
    /// Sound, der bei Betreten des Portals abgespielt wird
    /// </summary>
    public AudioSource sound;
    //Quellen:
    //Bär: https://www.youtube.com/watch?v=TCswyCcVQNg&t (Abrufdatum: 29.01.18)
    //Hund: http://www.salamisound.de/3561361-einmal-bellen-und (Abrufdatum: 29.01.18)
    //Hase: http://soundbible.com/85-Cartoon-Hop.html (Abrufdatum: 29.01.18)
    //Pferd: https://www.youtube.com/watch?v=D0nCnS-UJLs (Abrufdatum: 29.01.18)

    /// <summary>
    /// Delay, bevor Minigame gestartet wird
    /// </summary>
    float CountDownTime = 1.5f;

    /// <summary>
    /// Hilfsbool, damit Sound nur 1x gespielt wird
    /// </summary>
    bool play = false;

    /// <summary>
    /// Bool, ob Portal gerade aktiv ist (ob Minispiel bei betreten gestartet wird). Nur zu Debugging Zwecken
    /// </summary>
    public bool Aktiv = true;

    void Awake()
    {
        this.Manager = ManagerKlasse.Instance;
     }

    void Update()
    {
        if (done)
        {
            if (!play)
            {
                sound.Play();
                play = true;
            }


            CountDownTime -= Time.deltaTime;
            if (CountDownTime <= 0.0f)
            {
                WhatToDoWhenCollected();


            }

        }
         
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


            done = true;
       
        }
        else
            {
            Manager.LetAnimalSaySomething(Manager.GetAnimalByName(col.name), "Ich möchte meinem Freund " + animal.GetNickname() + " das Essen nicht wegnehmen!");
            
            }
    }

    /// <summary>
    /// Methode regelt, was beim Sammeln der Nuss passiert
    /// </summary>
    private void WhatToDoWhenCollected()
    {
        this.Manager.AddVisitedPortal(this);
        Manager.ClearScene();
        animal.SetHasFood(true);
        SceneManager.LoadScene(this.ChooseGame(animal.name));
       


    }

    /// <summary>
    /// Wählt den richtigen Spielnamen je nach Tier aus
    /// </summary>
    /// <param name="animalname">Tiername</param>
    /// <returns>Spielname</returns>
    private String ChooseGame(String animalname)
    {
        String result = "Startspiel";
        if (Aktiv)
        {
            switch (animal.name)
            {
                case "horse":
                    result = "Apfelspiel2";
                    break;
                case "bear":
                    result = "biene";
                    break;
                case "dog":
                    result = "Knochenspiel";
                    break;
                case "rabbit":
                    result = "Ruebenspiel";
                    break;
            }
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

