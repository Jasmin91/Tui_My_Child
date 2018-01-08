

using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PortalController : MonoBehaviour
{
    private ManagerKlasse Manager; //Erstellt eine Instanz der Manager-Klasse
    public bool done = false;
    public AnimalController animal;


    void Awake()
    {
        
        this.Manager = ManagerKlasse.Instance;
        
    }

    void Start()
    {
        this.Manager.AddPortal(this);
    }

    void Update()
    {
        /**if (done)
            Debug.Log("destroy!");
        {
            destroyObject();
        }*/
    }

    //Collider, der erkennt ob Tier und Portal kollidieren
    void OnTriggerEnter2D(Collider2D col)
    {
          
          if (col.gameObject.name == this.animal.name)
          {
              this.Manager.AddVisitedPortal(this);
              Manager.ClearScene();

            String scenename = "Startspiel";
            switch (animal.name)
            {
                case "horse":
                    scenename = "Apfelspiel2";
                    break;
                case "bear":
                    scenename = "Startspiel";
                    break;
                case "dog":
                    scenename = "Knochenspiel";
                    break;
                case "rabbit":
                    scenename = "Ruebenspiel";
                    break;
                    
            }
            
              SceneManager.LoadScene(scenename);
            animal.setHasFood(true);
        }
        else
            {
            Manager.LetAnimalSaySomething(col.gameObject.name, "Ich möchte meinem Freund " + animal.getNickname() + " das Essen nicht wegnehmen!");
               // Debug.Log("Ich möchte meinem Freund " + animal + " das Essen nicht wegnehmen!");
            }
      

         
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Manager.getAnimalByName(col.name).BeSilent();
    }
    public void TestdestroyObject()
    {
        // Destroy(this.gameObject); //Zerstören des betretenen Portals
        this.transform.position = animal.transform.position;
       Collider2D  m_ObjectCollider = GetComponent<Collider2D>();
        //Here the GameObject's Collider is not a trigger
        m_ObjectCollider.isTrigger = false;

    }

    public void destroyObject()
    {
        Destroy(this.gameObject); //Zerstören des betretenen Portals
    }
}

