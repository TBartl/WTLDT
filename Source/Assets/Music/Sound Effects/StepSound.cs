using UnityEngine;
using System.Collections;

public class StepSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AudioSource s = GetComponent<AudioSource>();
		s.timeSamples = 15000;
		s.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!GetComponent<AudioSource> ().isPlaying) {
			Destroy (gameObject);
		}
	}
}
