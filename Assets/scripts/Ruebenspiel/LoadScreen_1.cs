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

		GameObject circle5 = GameObject.Find ("Kreis1_2");
		GameObject circle6 = GameObject.Find ("Kreis2_2");
		GameObject circle7 = GameObject.Find ("Kreis3_2");
		GameObject circle8 = GameObject.Find ("Kreis4_2");


		fill1.fillAmount = RuebenController.ScreenArray [1]/2;

		if (fill1.fillAmount != null) {
			if (Fiducial_Counter.count == 1 && fill1.fillAmount == 1f) {
				SpriteRenderer renderer1 = circle5.GetComponent<SpriteRenderer> ();
				renderer1.color = new Color(0.133f, 0.545f, 0.133f) ;
			}

			if (Fiducial_Counter.count == 2 && fill1.fillAmount == 1f) {
				SpriteRenderer renderer1 = circle6.GetComponent<SpriteRenderer> ();
				renderer1.color = new Color(0.133f, 0.545f, 0.133f) ;
			}

			if (Fiducial_Counter.count == 3 && fill1.fillAmount == 1f) {
				SpriteRenderer renderer1 = circle7.GetComponent<SpriteRenderer> ();
				renderer1.color = new Color(0.133f, 0.545f, 0.133f) ;
			}

			if (Fiducial_Counter.count == 4 && fill1.fillAmount == 1f) {
				SpriteRenderer renderer1 = circle8.GetComponent<SpriteRenderer> ();
				renderer1.color = new Color(0.133f, 0.545f, 0.133f) ;
			}
		}
	}
}
