using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisable_2 : MonoBehaviour {
    public GameObject Enable_Disable2;
    public void Start()
    {
        Enable_Disable2.SetActive(true);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "blume2")
        {
            Enable_Disable2.SetActive(false);

        }

    }
}
