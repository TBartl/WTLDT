using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

	public static SoundManager SM;

	public AudioSource step;
	public AudioSource whistle;
	public AudioSource stab;
	public AudioSource scare;

	public GameObject stepping;

	void Awake ()
	{
		if (SM != null)
		{
			Destroy(this.gameObject);
		}
		else
		{
			SM = this;
			DontDestroyOnLoad(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

	public void StartStep() {
		//if (!step.isPlaying)
		//step.Play ();
		Instantiate (stepping);
	}

	public void StartWhistle()
	{
		whistle.Play ();
	}

	public void StartStab() {
		stab.Play ();	
	}

	public void StartScare() {
		scare.Play ();
	}

}
