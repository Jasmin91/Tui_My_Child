using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fiducial_Counter : MonoBehaviour {

	public static int count = 1;
	public int fiducialId = 0;

	// Use this for initialization
	void Start () {
	

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public static void UpdateCount(){
		count++;
		if (count > 4) {
			count = 1;
		}
	}
}
