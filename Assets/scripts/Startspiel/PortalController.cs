

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    private ManagerKlasse Manager; //Erstellt eine Instanz der Manager-Klasse



    void Awake()
    {
        
        this.Manager = ManagerKlasse.Instance;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //Collider, der erkennt ob Tier und Portal kollidieren
    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.gameObject.name == "Apfel" && col.gameObject.name == "pferd")
        {
            Destroy(this.gameObject); //Zerstören des betretenen Portals
            
            SceneManager.LoadScene("Apfelspiel2", LoadSceneMode.Additive);
            //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = false;
          
        }
    }


}

