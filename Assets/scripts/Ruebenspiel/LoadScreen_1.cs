using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen_1 : MonoBehaviour {

	[SerializeField]
	private float units1;
	float timeAMT1 = 10;
	float time;
	public Text timeText1;

	[SerializeField]
	private Image fill1;

	private float fillAmount;

	// Use this for initialization
	void Start () {
		//	Start (count);

		time = 0;
	}

	// Update is called once per frame
	void Update () {
		fill1.fillAmount = RuebenController.ScreenArray [1]/2;
	}
}
