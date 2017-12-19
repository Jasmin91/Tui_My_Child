using System;
using System.Collections.Generic;
using UnityEngine;
using TUIO;


//wird nicht genutzt!
public class Korb : MonoBehaviour {

    public float speed = 15;
    public string axis = "Horizontal";
    //private UniducialLibrary.TuioManager tuioManager;
    public int markerID = 0;

    void start()
    {
       // tuioManager = UniducialLibrary.TuioManager.Instance;
		Debug.Log("test");
    }

    void FixedUpdate()
    {
        float v = Input.GetAxisRaw(axis);
        GetComponent<Rigidbody2D>().velocity = new Vector2(v, 0) * speed;

		//if(tuioManager.isMarkerAlive(markerID)){
			//Debug.Log("Bin da!");
        //TUIO.TuioObject marker = tuioManager.getMarker(markerID);
        //}
		//Debug.Log("test");
	}
}
