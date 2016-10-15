using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Redpanel : MonoBehaviour {

	public Image red_panel	;
	float fadeTime = 3f;

	// Use this for initialization
	void Start () {
		red_panel.CrossFadeAlpha (0, 0, false);

		//StartCoroutine(StartFlash ());
		//StartFlash();
	}

	// Update is called once per frame
	void Update () {
		//StartCoroutine (FlashRed ());
		if (Input.GetKeyDown (KeyCode.R)) {
			StartCoroutine(FlashRed ());
		}
	}

	public IEnumerator FlashRed() {
		while (true) {
			// Fade in
			red_panel.CrossFadeAlpha (1.0f, 1.0f, false);
			yield return new WaitForSeconds (1.5f);

			// Fade out
			red_panel.CrossFadeAlpha (0.0f, 1.0f, false);
			yield return new WaitForSeconds (1.5f);

		}
	}
}
