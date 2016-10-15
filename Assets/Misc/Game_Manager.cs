using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game_Manager : MonoBehaviour {

    public static Game_Manager S;
	
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
    //On level complete
    public void EndLevel()
    {

    }

}
