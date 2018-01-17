using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisable_3 : MonoBehaviour {
    public GameObject Enable_Disable3;
    public void Start()
    {
        Enable_Disable3.SetActive(true);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "blume3")
        {
            Enable_Disable3.SetActive(false);

        }

    }
}
