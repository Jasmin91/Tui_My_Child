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

		GameObject circle1 = GameObject.Find ("Kreis1_1");
		GameObject circle2 = GameObject.Find ("Kreis2_1");
		GameObject circle3 = GameObject.Find ("Kreis3_1");
		GameObject circle4 = GameObject.Find ("Kreis4_1");

		fill.fillAmount = RuebenController.ScreenArray [0]/2;

		if (fill.fillAmount != null) {
			if (Fiducial_Counter.count == 1 && fill.fillAmount == 1f) {
				SpriteRenderer renderer = circle1.GetComponent<SpriteRenderer> ();
				renderer.color = new Color(0.133f, 0.545f, 0.133f) ;
			}

			if (Fiducial_Counter.count == 2 && fill.fillAmount == 1f) {
				SpriteRenderer renderer = circle2.GetComponent<SpriteRenderer> ();
				renderer.color = new Color(0.133f, 0.545f, 0.133f) ;
			}

			if (Fiducial_Counter.count == 3 && fill.fillAmount == 1f) {
				SpriteRenderer renderer = circle3.GetComponent<SpriteRenderer> ();
				renderer.color = new Color(0.133f, 0.545f, 0.133f) ;
			}

			if (Fiducial_Counter.count == 4 && fill.fillAmount == 1f) {
				SpriteRenderer renderer = circle4.GetComponent<SpriteRenderer> ();
				renderer.color = new Color(0.133f, 0.545f, 0.133f) ;
			}
		}
	}
}




/*if (this.name.IndexOf ("Ruebe" + Fiducial_Counter.count) > -1) {
			if (this.name.Equals ("Ruebe" + Fiducial_Counter.count + "_")) {
				GameObject circle = GameObject.Find (this.name.Replace ("Ruebe", "Kreis"));
				Debug.Log (circle);

		
						if (Fiducial_Counter.count == 1 && fill.fillAmount == 1f) {
							SpriteRenderer renderer = circle.GetComponents<SpriteRenderer> () [0];
							renderer.color = new Color (0.133f, 0.545f, 0.133f);
						}
					}
				}*/
