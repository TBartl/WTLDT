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

public class PlayerMovement : BaseMovement {

	public static PlayerMovement S;

	List<Direction> movementQueue;


	// Use this for initialization
	protected override void Start () {
		base.Start();
		S = this;
		PlayerVision.S.OnPlayerMove(pos);

		movementQueue = new List<Direction>();
	}
	// Update is called once per frame
	void Update () {

		// Detect if there is any input from the player.

		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
			movementQueue.Add(Direction.NORTH);
		else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
			movementQueue.Add(Direction.EAST);
		else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
			movementQueue.Add(Direction.SOUTH);
		else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
			movementQueue.Add(Direction.WEST);

		if (movementQueue.Count > 0 && Game_Manager.S.levelEnding == false)
		{
			SoundManager.SM.StartStep ();

			Direction toDir = movementQueue[0];
			movementQueue.RemoveAt(0);

			IntVector2 newPos = pos + IntVector2.fromDirection(toDir);
			MoveIfAble(newPos);
		}
	}

	protected override void Move(IntVector2 newPos)
	{
		LevelManager.S.MoveOccupant(pos, newPos);
		StartCoroutine(SmoothMove(pos, newPos));
		pos = newPos;
		PlayerVision.S.OnPlayerMove(newPos);
		Game_Manager.S.OnPlayerMove();
	}

	bool TowardsPassable(IntVector2 newPos) {
		return LevelManager.S.realData [newPos.x, newPos.y].passable;
	}

}
