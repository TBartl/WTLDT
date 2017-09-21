using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour {

    public static Game_Manager S;
	public bool levelEnding = false;

	public static int deathCount;

	float inactiveTime = 0;

	public static bool undetectable = false;

    void Awake()
    {
        S = this;
    }

	void Start() {
		if (deathCount > 5) {
			OtherDialogues.OD.ChangeDialogue ();
		}
		int scene = SceneManager.GetActiveScene().buildIndex;
		if (scene <= 8) {
			AudioManager.S.musiclevel = 1;
		} else if (scene > 8 && scene != 19) {
			AudioManager.S.musiclevel = 2;
		} else {
			AudioManager.S.musiclevel = 3;
		}
//		AudioManager.S.startMusic ();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.H))
		{
			WinLevel();
		}
		if (Input.GetKeyDown(KeyCode.Alpha0))
		{
			SceneManager.LoadScene(0);
		}

		if (Input.GetKeyDown(KeyCode.U))
		{
			undetectable = !undetectable;
		}

		inactiveTime += Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)
			|| Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
			inactiveTime = 0;
		if (inactiveTime > 60 && SceneManager.GetActiveScene().buildIndex != 0)
		{
			SceneManager.LoadScene(0);
		}
	}

    public void OnPlayerMove()
    {
        //update enemies
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("enemy"))
        {
            enemy.GetComponent<Enemy>().Move();
        }
    }
    public void FailLevel()
    {
		StartCoroutine(FailLevelCoroutine());
    }
	IEnumerator FailLevelCoroutine()
	{
		levelEnding = true;
		for (float f = 0; f < 1f; f += Time.deltaTime)
			yield return null;
		deathCount++;
		int scene = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(scene);
	}

	public void WinLevel()
	{
		StartCoroutine(WinLevelCoroutine());
	}
	IEnumerator WinLevelCoroutine()
	{
		levelEnding = true;
		for (float f = 0; f < 1f; f += Time.deltaTime)
			yield return null;
		deathCount = 0;
		int scene = SceneManager.GetActiveScene().buildIndex;
		if (scene == 8) {
			AudioManager.S.changeMusic ();
		} 
		// Change this number later
		else if (scene == 18) {
			AudioManager.S.ChangeToCreditMusic ();
		}
		else if (scene == 19)
			AudioManager.S.changeMusic ();
		SceneManager.LoadScene( (scene + 1) % SceneManager.sceneCountInBuildSettings );
	}
}
