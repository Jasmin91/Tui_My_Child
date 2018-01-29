using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Allinone : MonoBehaviour {
    public AudioSource tickSource;
    public AudioSource sound;
    private BienenManager manager;
    bool play = false;
    float targetTime = 3;

    // Use this for initialization
    void Start ()
    {
        manager = BienenManager.Instance; //Instanz v. Bienenmanager
        manager.ResetManager(); //resette d Managers
        gameObject.GetComponent<Collider2D>().enabled = false; //Collider deaktiviert
    }

    // Update is called once per frame
    void Update()
    {

        int filling = manager.GetFilling(); //Füllung abfragen im Manager
        string name = "glas" + filling; //Dateiname generieren
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load(name, typeof(Sprite)) as Sprite; //holt Bilddatei aus Ressources

        if (manager.GetReady()&& !play)
        {
            tickSource.Play();
            sound.Pause();
            play = true;
            
            
        }

        if(play){
            targetTime -= Time.deltaTime; //Warten, bis Sound fertig ist
            if (targetTime <= 0.0f)
            {
                play = false;
                manager.FinishGame();
            }
        }

    }

    
}
