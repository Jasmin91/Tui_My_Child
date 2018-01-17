using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisable_1 : MonoBehaviour {
    public GameObject Enable_Disable1;
    public void Start()
    {
        Enable_Disable1.SetActive(true);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "blume1")
        {
            Enable_Disable1.SetActive(false);

        }

    }
}
