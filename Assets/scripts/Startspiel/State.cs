using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

/// <summary>  
///  Diese Klasse hält den aktuellen Stand der Szene
/// </summary>
public class State {

    private ManagerKlasse Manager; //Erstellt eine Instanz der Manager-Klasse
    public string savegameName;//used as the file name when saving as well as for loading a specific savegame
    public string testString;//just a test variable of data we want to keep

    public State()
    {
        this.Manager = ManagerKlasse.Instance;
        Manager.addSceneState(this);
    }

}
