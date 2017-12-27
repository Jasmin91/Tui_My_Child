using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

/// <summary>  
///  Diese Klasse hält den aktuellen Stand der Szene
/// </summary>
public class State {
    
    public string savegameName;//used as the file name when saving as well as for loading a specific savegame
    public string testString;//just a test variable of data we want to keep
    public List<string> nutNames = new List<string>();

    public State()
    {
        
    }

    public void updateNutNames(List<string> nn)
    {
        this.nutNames = nn;
    }

}
