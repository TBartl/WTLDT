using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public static AudioManager S;

	public AudioSource mainMusic;
	public AudioSource chaseMusic;

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
	}

	// Use this for initialization
	void Start () {
		mainMusic.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeMusic() {
		mainMusic.Play ();
	}

	public void StartChase()
	{
		mainMusic.Stop();
		chaseMusic.Play();
	}

	void OnLevelWasLoaded()
	{
		if (chaseMusic.isPlaying)
		{
			chaseMusic.Stop();
			mainMusic.Play();
		}
	}

}
