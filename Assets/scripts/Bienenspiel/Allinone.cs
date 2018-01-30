using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Allinone : MonoBehaviour {
    /// <summary>
    /// Sound für den Erfolg
    /// </summary>
    public AudioSource tickSource;
    public AudioSource sound;
    /// <summary>
    /// Manager aus der BienenManager-Klasse wird definiert
    /// </summary>
    private BienenManager manager;
    /// <summary>
    /// Abspielen des Erfolgsounds wird als boolean-Wert definiert
    /// </summary>
    bool play = false;
    /// <summary>
    /// Zeitspanne für die länge des Sounds wird bestimmt
    /// </summary>
    float targetTime = 3;

    /// <summary>
    /// Instanz des Managers wird erstellt
    /// Reset des Managers wird vorgenommen
    /// Collider wird deaktiviert
    /// </summary>
    void Start ()
    {
        manager = BienenManager.Instance; 
        manager.ResetManager(); 
        gameObject.GetComponent<Collider2D>().enabled = false; 
    }

    /// <summary>
    /// Füllung wird im Manager abgefragt
    /// Dateiname wird generiert
    /// Bilddatei wird auf den Ressourcen hervorgeholt
    /// </summary>
    void Update()
    {

        int filling = manager.GetFilling(); 
        string name = "glas" + filling; 
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load(name, typeof(Sprite)) as Sprite; 

        if (manager.GetReady()&& !play)
        {
            tickSource.Play();
            sound.Pause();
            play = true;
            
            
        }
        ///<summary>
        ///Zeit wird abgeartet bis Sound zu Ende gespielt wird
        ///</summary>
        if(play){
            targetTime -= Time.deltaTime; 
            if (targetTime <= 0.0f)
            {
                play = false;
                manager.FinishGame();
            }
        }

    }

    
}
