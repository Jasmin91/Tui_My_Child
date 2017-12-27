using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


/// <summary>  
///  Diese Klasse speichert und lädt den aktuellen Stand der Szene
/// </summary>
public class SaveLoad {


    private ManagerKlasse Manager; //Erstellt eine Instanz der Manager-Klasse

    public SaveLoad()
    {
        this.Manager = ManagerKlasse.Instance;
        Manager.addSaveLoad(this);
    }

    //it's static so we can call it from anywhere
    public void Save(State saveGame)
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create("C:/Savegames/" + saveGame.savegameName + ".sav"); //you can call it anything you want, including the extension. The directories have to exist though.
        bf.Serialize(file, saveGame);
        file.Close();
        Debug.Log("Saved Game: " + saveGame.savegameName);

    }

    public State Load(string gameToLoad)
    {
        if (File.Exists("C:/Savegames/" + gameToLoad + ".sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open("C:/Savegames/" + gameToLoad + ".gd", FileMode.Open);
            State loadedGame = (State)bf.Deserialize(file);
            file.Close();
            Debug.Log("Loaded Game: " + loadedGame.savegameName);
            return loadedGame;
        }
        else
        {
            Debug.Log("File doesn't exist!");
            return null;
        }

    }


}