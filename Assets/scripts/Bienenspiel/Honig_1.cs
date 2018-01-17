using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honig_1 : MonoBehaviour {
    public AudioSource tickSource;
    public GameObject Honey1;
    // Use this for initialization
    public void Start()
    {
        tickSource = GetComponent<AudioSource>();
        Honey1.SetActive(false);
    }
    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "blume1")
        {
            tickSource.Play();
            Honey1.SetActive(true);

        }

    }
}
