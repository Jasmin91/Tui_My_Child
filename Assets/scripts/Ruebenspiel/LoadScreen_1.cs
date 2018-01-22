using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen_1 : MonoBehaviour {

	[SerializeField]
	private Image fill1;

	private float fillAmount;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

/*		GameObject circle1 = GameObject.Find ("Kreis1_2");
		GameObject circle2 = GameObject.Find ("Kreis2_2");
		GameObject circle3 = GameObject.Find ("Kreis3_2");
		GameObject circle4 = GameObject.Find ("Kreis4_2");

		GameObject[] circle = new GameObject[] {
			circle1, circle2, circle3, circle4
		}; */

		fill1.fillAmount = RuebenController.ScreenArray [1]/2;

	/*	for (int i = 1; i <= Fiducial_Counter.count; i++) {
			if (i < Fiducial_Counter.count) {
				SpriteRenderer renderer = circle [i - 1].GetComponent<SpriteRenderer> ();
				renderer.color = new Color (0.133f, 0.545f, 0.133f);
				continue;
			} else {
				if (fill1.fillAmount >= 1f) {
					SpriteRenderer renderer = circle [i - 1].GetComponent<SpriteRenderer> ();
					renderer.color = new Color (0.133f, 0.545f, 0.133f);
					continue;
				}
			} 
				SpriteRenderer renderer1 = circle [i - 1].GetComponent<SpriteRenderer> ();
				renderer1.color = new Color (0.35f, 0.19f, 0.1f);
		} */
	}
}
