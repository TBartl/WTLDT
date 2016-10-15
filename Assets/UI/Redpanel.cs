using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Redpanel : MonoBehaviour {

	public Image red_panel	;
	float fadeTime = 3f;

	// Use this for initialization
	void Start () {
		//StartCoroutine(StartFlash ());
	}
	
	// Update is called once per frame
	void Update () {
		//StartCoroutine (FlashRed ());

	}

//	public IEnumerable StartFlash() {
//		while (true) {
//			yield return StartCoroutine (FlashRed ());
//		}
//	}
//
//	public IEnumerator FlashRed() {
//		
//		red_panel.CrossFadeAlpha (0.0f, 1.0f, false);
//		yield return WaitForSeconds (0.5f);
//		red_panel.CrossFadeAlpha (1.0f, 1.0f, false);
//	}
}
