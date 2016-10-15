using UnityEngine;
using System.Collections;

public enum Direction {
	NORTH,
	EAST,
	SOUTH,
	WEST
}

public class PlayerMovement : MonoBehaviour {

	public PlayerMovement Player;
	public TileData onTile;
    public IntVector2 position;
	int movement_buffer;

	// Use this for initialization
	void Start () {
		Player = this;
		position.x = (int)(gameObject.transform.position.x);
		position.y = (int)(gameObject.transform.position.z);
		// Initialize first tile
		onTile = LevelManager.S.realData [position.x, position.y];
		movement_buffer = 0;
	}
	// Update is called once per frame
	void Update () {



		// Detect if there is any input from the player.

		// Move Up
		if (Input.GetKeyDown (KeyCode.W)) {
			Debug.Log ("first" + position.x + ", " + position.y);
			IntVector2 newPosition = position + new IntVector2 (0, 1);
			LevelManager.S.MoveUnitByOne (position, newPosition);
			Game_Manager.GM.on_player_move ();
			position = newPosition;
		} 
		// Move Down
		else if (Input.GetKeyDown (KeyCode.S)) {
			IntVector2 newPosition = position + new IntVector2 (0, -1);
			LevelManager.S.MoveUnitByOne (position, newPosition);
			Game_Manager.GM.on_player_move ();
			position = newPosition;
		}
		// Move Right
		else if (Input.GetKeyDown (KeyCode.D)) {
			IntVector2 newPosition = position + new IntVector2 (1, 0);
			LevelManager.S.MoveUnitByOne (position, newPosition);
			Game_Manager.GM.on_player_move ();
			position = newPosition;
		}
		// Move Left
		else if (Input.GetKeyDown (KeyCode.A)) {
			IntVector2 newPosition = position + new IntVector2 (-1, 0);
			LevelManager.S.MoveUnitByOne (position, newPosition);
			Game_Manager.GM.on_player_move ();
			position = newPosition;
		}


	}

}
