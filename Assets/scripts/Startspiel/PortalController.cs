

using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PortalController : MonoBehaviour
{
    private ManagerKlasse Manager; //Erstellt eine Instanz der Manager-Klasse
    public bool done = false;
    public String animal = "nicht vergeben";


    void Awake()
    {
        
        this.Manager = ManagerKlasse.Instance;
        
    }

    void Start()
    {
        this.Manager.addPortal(this);
    }

    void Update()
    {
        
    }

    //Collider, der erkennt ob Tier und Portal kollidieren
    void OnTriggerEnter2D(Collider2D col)
    {
          
          if (col.gameObject.name == this.animal)
          {
              this.Manager.addVisitedPortal(this);
              Manager.clearScene();

            String scenename = "Startspiel";
            switch (animal)
            {
                case "horse":
                    scenename = "Apfelspiel2";
                    break;
                case "bear":
                    scenename = "Startspiel";
                    break;
                case "dog":
                    scenename = "Startspiel";
                    break;
                case "rabbit":
                    scenename = "Startspiel";
                    break;
                    
            }
              SceneManager.LoadScene(scenename);
          }else
            {
                Debug.Log("Ich möchte meinem Freund " + animal + " das Essen nicht wegnehmen!");
            }
      


          /**
        if (this.gameObject.name == "apple")
        {
            if (col.gameObject.name == "horse")
            {
                this.Manager.addVisitedPortal(this);
                Manager.clearScene();
                SceneManager.LoadScene("Apfelspiel2");
            }
            
        }*/
    }

    public void destroyObject()
    {
         Destroy(this.gameObject); //Zerstören des betretenen Portals
        //this.gameObject.SetActive(false);
        Debug.Log(name+ " stirbt");
    }


}

