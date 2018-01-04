using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>  
///  Diese Klasse steuert das Upaten des Startspiels nach einem Minispiel
/// </summary> 


public class GameState : MonoBehaviour
{
    
    private ManagerKlasse Manager; //Erstellt eine Instanz der Manager-Klasse
    private KnochenManager km_Instance; //Erstellt eine Instanz der KnochenManager-Klasse
    private bool updated = false; //Wurde bereits geupdated



    void Start()
    {
        this.Manager = ManagerKlasse.Instance;
        this.km_Instance = KnochenManager.Instance;
    }
    
    void Update()
    {
       // if (km_Instance.getGameSolved())
       // {
         //   this.finishGame();
       // }
    }

    private void finishGame()
    {
        SceneManager.LoadScene("Startspiel");
    }
    
}
