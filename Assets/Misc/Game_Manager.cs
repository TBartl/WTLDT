using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour {

    public static Game_Manager S;
	public bool levelEnding = false;
	
    void Awake()
    {
        S = this;
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
		SceneManager.LoadScene(scene + 1);
	}
}
