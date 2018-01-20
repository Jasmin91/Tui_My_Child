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

		GameObject circle13 = GameObject.Find ("Kreis1_4");
		GameObject circle14 = GameObject.Find ("Kreis2_4");
		GameObject circle15 = GameObject.Find ("Kreis3_4");
		GameObject circle16 = GameObject.Find ("Kreis4_4");

		fill3.fillAmount = RuebenController.ScreenArray [3]/2;

		if (fill3.fillAmount != null) {
			if (Fiducial_Counter.count == 1 && fill3.fillAmount == 1f) {
				SpriteRenderer renderer3 = circle13.GetComponent<SpriteRenderer> ();
				renderer3.color = new Color(0.133f, 0.545f, 0.133f) ;
			}

			if (Fiducial_Counter.count == 2 && fill3.fillAmount == 1f) {
				SpriteRenderer renderer3 = circle14.GetComponent<SpriteRenderer> ();
				renderer3.color = new Color(0.133f, 0.545f, 0.133f) ;
			}

			if (Fiducial_Counter.count == 3 && fill3.fillAmount == 1f) {
				SpriteRenderer renderer3 = circle15.GetComponent<SpriteRenderer> ();
				renderer3.color = new Color(0.133f, 0.545f, 0.133f) ;
			}

			if (Fiducial_Counter.count == 4 && fill3.fillAmount == 1f) {
				SpriteRenderer renderer3 = circle16.GetComponent<SpriteRenderer> ();
				renderer3.color = new Color(0.133f, 0.545f, 0.133f) ;
			}
		}
	}
}
