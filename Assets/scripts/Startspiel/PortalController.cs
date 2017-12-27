

using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PortalController : MonoBehaviour
{
    private ManagerKlasse Manager; //Erstellt eine Instanz der Manager-Klasse
    public bool done = false;


    void Awake()
    {
        
        this.Manager = ManagerKlasse.Instance;
        
    }

    void Start()
    {
        this.Manager.addPortal(this);
        Debug.Log("Eine neues Portal ist geboren und ich heiße " + name);
    }

    void Update()
    {
        
    }

    //Collider, der erkennt ob Tier und Portal kollidieren
    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.gameObject.name == "Apfel" && col.gameObject.name == "pferd")
        {
            this.Manager.addVisitedPortal(this);
            Destroy(this.gameObject); //Zerstören des betretenen Portals
            Manager.clearScene();
            SceneManager.LoadScene("Apfelspiel2");
            //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = false;
          
        }
    }

    public void destroyObject()
    {
        Destroy(this.gameObject); //Zerstören des betretenen Portals
    }


}

