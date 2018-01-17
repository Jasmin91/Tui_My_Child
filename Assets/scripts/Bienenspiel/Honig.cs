using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honig : MonoBehaviour {
    public AudioSource tickSource;
    public GameObject Honey;
    // Use this for initialization
    public void Start()
    {
        tickSource = GetComponent<AudioSource>();
        Honey.SetActive(false);
    }
    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "blume")
        {
            tickSource.Play();
            Honey.SetActive(true);
            
        }

    }
}
