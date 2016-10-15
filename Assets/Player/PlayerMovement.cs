using UnityEngine;
using System.Collections;

enum Direction {
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
		LevelManager.S.realData [position.x, position.y].occupant = gameObject;
		movement_buffer = 0;

		// Initialize first tile
	}
	// Update is called once per frame
	void Update () {
		this.transform.position += Vector3.left * Time.deltaTime;
		




		// Detect if there is any input from the player.

		// Move Up
		if (Input.GetKeyDown (KeyCode.W)) {
			MovePlayerByOne (Direction.NORTH);
		} 
		// Move Down
		else if (Input.GetKeyDown (KeyCode.S)) {
			MovePlayerByOne (Direction.SOUTH);
		}
		// Move Right
		else if (Input.GetKeyDown (KeyCode.D)) {
			MovePlayerByOne (Direction.EAST);
		}
		// Move Left
		else if (Input.GetKeyDown (KeyCode.A)) {
			MovePlayerByOne (Direction.WEST);
			IntVector2 newPosition = position += new IntVector2 (0, 1);
			LevelManager.S.MoveUnitByOne (position, newPosition);
			Game_Manager.GM.on_player_move ();
		} 
		// Move Down
		else if (Input.GetKeyDown (KeyCode.S)) {
			IntVector2 newPosition = position += new IntVector2 (0, -1);
			LevelManager.S.MoveUnitByOne (position, newPosition);
			Game_Manager.GM.on_player_move ();
		}
		// Move Right
		else if (Input.GetKeyDown (KeyCode.D)) {
			IntVector2 newPosition = position += new IntVector2 (1, 0);
			LevelManager.S.MoveUnitByOne (position, newPosition);
			Game_Manager.GM.on_player_move ();
		}
		// Move Left
		else if (Input.GetKeyDown (KeyCode.A)) {
			IntVector2 newPosition = position += new IntVector2 (-1, 0);
			LevelManager.S.MoveUnitByOne (position, newPosition);
			Game_Manager.GM.on_player_move ();
		}


	}
	
	// Move player by one block. This handles tile properties.
	void MovePlayerByOne(Direction moving_direction) {

		// Previous tile loses reference to player

		// New tile gains reference to player

	}
}
