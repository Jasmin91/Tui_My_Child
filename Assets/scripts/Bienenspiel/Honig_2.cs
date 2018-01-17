using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honig_2 : MonoBehaviour {
    public AudioSource tickSource;
    public GameObject Honey2;
    // Use this for initialization
    public void Start()
    {
        tickSource = GetComponent<AudioSource>();
        Honey2.SetActive(false);
    }
    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "blume2")
        {
            tickSource.Play();
            Honey2.SetActive(true);

        }

    }
}
