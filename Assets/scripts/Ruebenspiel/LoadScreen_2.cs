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

	//	public float sec = 10f;
	//	public float startVal = 0f;
	//	public float progress = 0f;

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

	/*

	public IEnumerator BuildUnits(){

		//Vector2 topLeft = Camera.main.ViewportToWorldPoint(new Vector2(0,1));
		//Vector2 bottomRight = Camera.main.ViewportToWorldPoint (new Vector2 (1, 0));

		for(int i = 0; 1 <= units; i++){


			fillAmount = i / units;
		//	Instantiate (count);
			yield return null;
		}
		count++;
	}

	private void UpdateBar(){

		fill.fillAmount = fillAmount;

	} */
}
