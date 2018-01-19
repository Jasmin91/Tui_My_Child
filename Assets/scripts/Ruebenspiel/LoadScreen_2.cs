using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen_2 : MonoBehaviour {

	[SerializeField]
	private float units2;
	float timeAMT2 = 10;
	float time;
	public Text timeText2;

	[SerializeField]
	private Image fill2;

	private float fillAmount;

	// Use this for initialization
	void Start () {
		//	Start (count);

		time = 0;
	}

	// Update is called once per frame
	void Update () {
		fill2.fillAmount = RuebenController.ScreenArray [2]/3;
	}
}
