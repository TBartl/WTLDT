using UnityEngine;
using System.Collections;

public class Sentinel : Enemy {

    public Direction currentDirection;

	// Use this for initialization
	void Start () {
        float val = Random.Range(0, 1);
        if (val < 0.25f)
        {
            currentDirection = Direction.NORTH;
        }
        else if (val < 0.5f)
        {
            currentDirection = Direction.SOUTH;
        }
        else if (val < 0.75f)
        {
            currentDirection = Direction.EAST;
        }
        else
        {
            currentDirection = Direction.WEST;
        }
    }
	

    //Sentinel looks around, doesn't move unless he sees you
	public override void Move()
    {
        if (!alarmRaised)
        {
            if (currentDirection == Direction.EAST) currentDirection = Direction.SOUTH;
            else if (currentDirection == Direction.SOUTH) currentDirection = Direction.WEST;
            else if (currentDirection == Direction.WEST) currentDirection = Direction.NORTH;
            else if (currentDirection == Direction.NORTH) currentDirection = Direction.EAST;
            if (CanSeePlayer(currentDirection))
            {
                alarmRaised = true;
            }
        }
        else
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        IntVector2 playerPos = PlayerMovement.Player.position;
        float xDif, yDif;
        xDif = Mathf.Abs(playerPos.x - pos.x);
        yDif = Mathf.Abs(playerPos.y - pos.y);
        if(xDif >= yDif)
        {
            if(playerPos.x < pos.x)
            {
                if (currentDirection != Direction.WEST)
                    currentDirection = Direction.WEST;
                else {
                    IntVector2 movePos = pos;
                    movePos.x -= 1;
                    if (LevelManager.S.realData[movePos.x, movePos.y].passable)
                    {
                        LevelManager.S.MoveSomething(pos, movePos);
                    }
                }
            }
            else
            {
                if (currentDirection != Direction.EAST)
                    currentDirection = Direction.EAST;
                else
                {
                    IntVector2 movePos = pos;
                    movePos.x += 1;
                    if (LevelManager.S.realData[movePos.x, movePos.y].passable)
                    {
                        LevelManager.S.MoveSomething(pos, movePos);
                    }
                }
            }     
        }
        else
        {
            if (playerPos.y < pos.y)
            {
                if (currentDirection != Direction.SOUTH)
                    currentDirection = Direction.SOUTH;
                else
                {
                    IntVector2 movePos = pos;
                    movePos.y -= 1;
                    if (LevelManager.S.realData[movePos.x, movePos.y].passable)
                    {
                        LevelManager.S.MoveSomething(pos, movePos);
                    }
                }
            }
            else
            {
                if (currentDirection != Direction.NORTH)
                    currentDirection = Direction.NORTH;
                else
                {
                    IntVector2 movePos = pos;
                    movePos.y += 1;
                    if (LevelManager.S.realData[movePos.x, movePos.y].passable)
                    {
                        LevelManager.S.MoveSomething(pos, movePos);
                    }
                }
            }
        }
    }
}
