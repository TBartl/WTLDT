using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game_Manager : MonoBehaviour {

    public static Game_Manager GM;
    public int game_level;
    public Button Start;


    public void OnStartClick()
    {
        Application.LoadLevel("main");
        Debug.Log("You have clicked the button!");
    }

    void Awake()
    {
        GM = this;
        //Application load level, start menu
        game_level = 1;
    }

    public void on_player_move()
    {
        //update enemies
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("enemy"))
        {
            //enemy.move()
        }
    }
    //On level complete
    void level_complete()
    {
        char c;
        //base will be continue, will change on input from the game, later
        c = 'c';
        //show menu? continue, retry, exit?
        if (c == 'c')
            game_level++;
        // load level
        else if (c == 'r')
            return;
        //reload level
        else if (c == 'e')
            return;
    }

}
