using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen_3 : MonoBehaviour {

	[SerializeField]
	private float units3;
	float timeAMT3 = 10;
	float time;
	public Text timeText3;

	[SerializeField]
	private Image fill3;

	private float fillAmount;

	// Use this for initialization
	void Start () {
		//	Start (count);

		time = 0;
	}

	// Update is called once per frame
	void Update () {
		fill3.fillAmount = RuebenController.ScreenArray [3]/3;
	}
}
