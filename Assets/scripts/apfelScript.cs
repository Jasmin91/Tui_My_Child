using System;
using System.Collections.Generic;
using UnityEngine;

//Apfel fällt schon, wenn noch garnicht sichtbar!

public class apfelScript : MonoBehaviour
{
    private Management ms_Instance;
    private bool appleVisible = false;
    public float fallingSpeed = 0.23f;
    private bool fallen = false;



    void Start()
    {
        // tuioManager = UniducialLibrary.TuioManager.Instance;
        this.ms_Instance = Management.Instance;
        this.ms_Instance.addApple(this);
        this.HideApple();
        
    }

    void Update()
    {
        Debug.Log("Mal schauen, ob wir ernten können...");
        if (this.ms_Instance.getHarvestingReady())
        {
            Debug.Log("Wir können ernten...");
            this.ShowApple();
        }
        
    }

    public void setFallen(bool fallen)
    {
        this.fallen = fallen;
    }

    public bool getFallen()
    {
        return this.fallen;
    }

    void ShowApple()
    {


        //if (gameObject.GetComponent<Renderer>() != null && !gameObject.GetComponent<Renderer>().enabled)
        
            gameObject.GetComponent<Renderer>().enabled = true;
        appleVisible = true;

            
        

    }

    void HideApple()
    {


        //set 3d game object to visible, if it was hidden before
        //if (gameObject.GetComponent<Renderer>() != null && gameObject.GetComponent<Renderer>().enabled)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            Debug.Log("Der Apfel verschwindet");
        }

    }

    public void FallingApple()
    {


        //if (gameObject.GetComponent<Renderer>() != null && !gameObject.GetComponent<Renderer>().enabled)
        
            if (this.appleVisible) { 
            gameObject.GetComponent<Rigidbody2D>().gravityScale = this.fallingSpeed;
            this.setFallen(true);
            Debug.Log("Der Apfel fällt.");
        }

    }
}
