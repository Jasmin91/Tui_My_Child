using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Allinone : MonoBehaviour {
public GameObject All_in_one;
public AudioSource tickSource;
public int counter=0;
    
    // Use this for initialization
    void Start ()
    {
        tickSource = GetComponent<AudioSource>();
        All_in_one.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        //if (counter == 4)
        //{
        //    All_in_one.SetActive(true);
        //    tickSource.Play();
        //}

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
            if (other.gameObject.name == "bee" || other.gameObject.name == "bee1" || other.gameObject.name == "bee2" || other.gameObject.name == "bee3")
            {
                counter++;
                if (counter == 4)
                {
                    All_in_one.SetActive(true);
                    tickSource.Play();
                }
            }   
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "bee" || other.gameObject.name == "bee1" || other.gameObject.name == "bee2" || other.gameObject.name == "bee3")
        {
            counter--;
        }
     
    }
    
}
