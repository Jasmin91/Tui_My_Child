using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour {

	[SerializeField]
	private float units;
	float timeAMT = 10;
	float time;
	public Text timeText;

	[SerializeField]
	private Image fill;

	private float fillAmount;

	// Use this for initialization
	void Start () {
	//	Start (count);

		time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		fill.fillAmount = RuebenController.ScreenArray [0]/2;
	}
}
