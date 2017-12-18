using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("korb_1", typeof(Sprite)) as Sprite;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
