using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public static AudioManager S;

	public AudioSource mainMusic;
	public AudioSource chaseMusic;
	public AudioSource mainMusic2;
	public AudioSource chaseMusic2;
	public AudioSource creditMusic;

	public int musiclevel;

	void Awake ()
	{
		if (S != null)
		{
			Destroy(this.gameObject);
		}
		else
		{
			S = this;
			DontDestroyOnLoad(this.gameObject);
		}
		musiclevel = 1;
	}

	// Use this for initialization
	void Start () {
		startMusic ();
	}

	public void startMusic() {
		Debug.Log ("music level: " + musiclevel);
		if (musiclevel == 1)
			mainMusic.Play ();
		else if (musiclevel == 2)
			mainMusic2.Play ();
		else if (musiclevel == 3)
			creditMusic.Play ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeMusic() {
		mainMusic.Stop ();
		chaseMusic.Stop ();
		musiclevel = 2;
		mainMusic2.Play ();
	}

	public void ChangeToCreditMusic() {
		mainMusic.Stop ();
		chaseMusic.Stop ();
		mainMusic2.Stop ();
		chaseMusic2.Stop ();
		musiclevel = 3;
		creditMusic.Play ();
	}

	public void StartChase()
	{
		if (musiclevel == 1) {
			mainMusic.Stop ();
			chaseMusic.Play ();
		}
		else if (musiclevel == 2) {
			mainMusic2.Stop ();
			chaseMusic2.Play ();
		}
	}

	void OnLevelWasLoaded()
	{
		
		if (chaseMusic.isPlaying || chaseMusic2.isPlaying)
		{
			chaseMusic.Stop ();
			chaseMusic2.Stop ();
			if (musiclevel == 1) {
				mainMusic2.Stop ();
				mainMusic.Play ();
			} else if (musiclevel == 2) {
				mainMusic.Stop ();
				mainMusic2.Play ();
			}
		}
	}

}
