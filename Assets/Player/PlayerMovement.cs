using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public enum Direction {
	NORTH,
	EAST,
	SOUTH,
	WEST
}

public class PlayerMovement : MonoBehaviour {

	public static PlayerMovement Player;
	public IntVector2 position;

	List<Direction> movementQueue;


	// Use this for initialization
	void Start () {
		Player = this;
		position.x = Mathf.RoundToInt(transform.position.x);
		position.y = Mathf.RoundToInt(transform.position.z);
		PlayerVision.S.OnPlayerMove(position);

		movementQueue = new List<Direction>();
	}
	// Update is called once per frame
	void Update () {

		// Detect if there is any input from the player.

		if (Input.GetKeyDown(KeyCode.W))
			movementQueue.Add(Direction.NORTH);
		else if (Input.GetKeyDown(KeyCode.D))
			movementQueue.Add(Direction.EAST);
		else if (Input.GetKeyDown(KeyCode.S))
			movementQueue.Add(Direction.SOUTH);
		else if (Input.GetKeyDown(KeyCode.A))
			movementQueue.Add(Direction.WEST);

		if (movementQueue.Count > 0)
		{
			Direction toDir = movementQueue[0];
			movementQueue.RemoveAt(0);

			IntVector2 newPos = position + GetVectorFromDirection(toDir);
			if (LevelManager.S.InBounds(newPos) && TowardsPassable(newPos))
			{
				LevelManager.S.MoveSomething(position, newPos);
				PlayerVision.S.OnPlayerMove(newPos);
				position = newPos;
			}
		}
	}

	IntVector2 GetVectorFromDirection(Direction d)
	{
		if (d == Direction.NORTH)
			return IntVector2.up;
		if (d == Direction.EAST)
			return IntVector2.right;
		if (d == Direction.SOUTH)
			return IntVector2.down;
		else
			return IntVector2.left;
	}


	bool TowardsPassable(IntVector2 newPos) {
		return LevelManager.S.realData [newPos.x, newPos.y].passable;
	}
}
