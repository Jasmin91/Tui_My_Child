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

		GameObject circle9 = GameObject.Find ("Kreis1_3");
		GameObject circle10 = GameObject.Find ("Kreis2_3");
		GameObject circle11 = GameObject.Find ("Kreis3_3");
		GameObject circle12 = GameObject.Find ("Kreis4_3");

		fill2.fillAmount = RuebenController.ScreenArray [2]/2;

		if (fill2.fillAmount != null) {
			if (Fiducial_Counter.count == 1 && fill2.fillAmount == 1f) {
				SpriteRenderer renderer2 = circle9.GetComponent<SpriteRenderer> ();
				renderer2.color = new Color(0.133f, 0.545f, 0.133f) ;
			}

			if (Fiducial_Counter.count == 2 && fill2.fillAmount == 1f) {
				SpriteRenderer renderer2 = circle10.GetComponent<SpriteRenderer> ();
				renderer2.color = new Color(0.133f, 0.545f, 0.133f) ;
			}

			if (Fiducial_Counter.count == 3 && fill2.fillAmount == 1f) {
				SpriteRenderer renderer2 = circle11.GetComponent<SpriteRenderer> ();
				renderer2.color = new Color(0.133f, 0.545f, 0.133f) ;
			}

			if (Fiducial_Counter.count == 4 && fill2.fillAmount == 1f) {
				SpriteRenderer renderer2 = circle12.GetComponent<SpriteRenderer> ();
				renderer2.color = new Color(0.133f, 0.545f, 0.133f) ;
			}
		}
	}
}
