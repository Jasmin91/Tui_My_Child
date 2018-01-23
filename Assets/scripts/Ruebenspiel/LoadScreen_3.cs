using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen_3 : MonoBehaviour {

	[SerializeField]
	private Image fill3;

	private float fillAmount;

	// Use this for initialization
	void Start () {
		UpdateBar ();
	}

	// Update is called once per frame
	void Update () {

/*		GameObject circle1 = GameObject.Find ("Kreis1_4");
		GameObject circle2 = GameObject.Find ("Kreis2_4");
		GameObject circle3 = GameObject.Find ("Kreis3_4");
		GameObject circle4 = GameObject.Find ("Kreis4_4");

		GameObject[] circle = new GameObject[] {
			circle1, circle2, circle3, circle4
		}; */

		fill3.fillAmount = RuebenController.ScreenArray [3]/2;

	/*	for (int i = 1; i <= Fiducial_Counter.count; i++) {
			if (i < Fiducial_Counter.count) {
				SpriteRenderer renderer = circle [i - 1].GetComponent<SpriteRenderer> ();
				renderer.color = new Color (0.133f, 0.545f, 0.133f);
				continue;
			} else {
				if (fill3.fillAmount >= 1f) {

					Debug.Log ("ich bin jetzt in der Else 3");
					SpriteRenderer renderer = circle [i - 1].GetComponent<SpriteRenderer> ();
					renderer.color = new Color (0.133f, 0.545f, 0.133f);
					continue;
				}
			} 
				SpriteRenderer renderer1 = circle [i - 1].GetComponent<SpriteRenderer> ();
				renderer1.color = new Color (0.35f, 0.19f, 0.1f); 
		} */
	}

	private void UpdateBar(){
		fill3.fillAmount = 0f;
	}
}
