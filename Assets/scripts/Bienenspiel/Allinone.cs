using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Allinone : MonoBehaviour {
    public AudioSource tickSource;
    public AudioSource sound;
    private BienenManager manager;
    public int counter=0;
    bool play = false;
    float targetTime = 3;

    // Use this for initialization
    void Start ()
    {
       // tickSource.Play();
        manager = BienenManager.Instance;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        int filling = manager.GetFilling();
        string name = "glas" + filling;
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load(name, typeof(Sprite)) as Sprite;

        if (manager.GetReady()&&play==false)
        {
            tickSource.Play();
            sound.Pause();
            play = true;
            
            
        }

        if(play){
            targetTime -= Time.deltaTime;
            if (targetTime <= 0.0f)
            {
                
                play = false;
                manager.FinisfhGame();
            }
        }

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
           /** if (other.gameObject.name == "bee" || other.gameObject.name == "bee1" || other.gameObject.name == "bee2" || other.gameObject.name == "bee3")
            {
                counter++;
                if (counter == 4)
                {
                    tickSource.Play();
                }
            }   */
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "bee" || other.gameObject.name == "bee1" || other.gameObject.name == "bee2" || other.gameObject.name == "bee3")
        {
            counter--;
        }
     
    }
    
}
