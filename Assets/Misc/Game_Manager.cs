using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour {

    public static Game_Manager S;
	public bool levelEnding = false;

	float inactiveTime = 0;
	
    void Awake()
    {
        S = this;
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
		int scene = SceneManager.GetActiveScene().buildIndex;
		if (scene == 8) {
			AudioManager.S.changeMusic ();
		} 
		// Change this number later
		else if (scene == 16) {
			AudioManager.S.ChangeToCreditMusic ();
		}
		SceneManager.LoadScene(scene + 1);
	}
}
