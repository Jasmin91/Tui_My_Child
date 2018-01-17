using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisable : MonoBehaviour {
    public GameObject Enable_Disable;
    public void Start()
    {
        Enable_Disable.SetActive(true);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "blume")
        {
            Enable_Disable.SetActive(false);

        }

    }
}
