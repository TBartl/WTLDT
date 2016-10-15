using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int sightDistance = 2;
    public IntVector2 pos;
    public static bool _alarmRaised = false;
    public Direction currentDirection;

	// Use this for initialization
	void Awake() {
        pos.x = Mathf.RoundToInt(transform.position.x);
        pos.y = Mathf.RoundToInt(transform.position.z);
    }
	
    public virtual void Move()
    {

    }

    //For now, sees 2 in front, two to either side + 1 diagonally
	public bool CanSeePlayer(Direction dir)
    {
        if (LevelManager.S.realData[pos.x, pos.y].light == LightAmount.lit)
        {
            if (dir == Direction.NORTH && PlayerMovement.Player.position.y >= pos.y)
                return true;
            else if (dir == Direction.SOUTH && PlayerMovement.Player.position.y <= pos.y)
                return true;
            else if (dir == Direction.EAST && PlayerMovement.Player.position.x >= pos.x)
                return true;
            else if (dir == Direction.WEST && PlayerMovement.Player.position.x <= pos.x)
                return true;
            else return false;
        }
        else return false;       
    }

    public virtual void ChasePlayer()
    {
        IntVector2 playerPos = PlayerMovement.Player.position;
        float xDif, yDif;
        xDif = Mathf.Abs(playerPos.x - pos.x);
        yDif = Mathf.Abs(playerPos.y - pos.y);
        IntVector2 movePos = pos;
        if (xDif >= yDif)
        {
            if (playerPos.x < pos.x)
            {
                if (currentDirection != Direction.WEST)
                    currentDirection = Direction.WEST;
                else
                {
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
                    
                    movePos.y += 1;
                    if (LevelManager.S.realData[movePos.x, movePos.y].passable)
                    {
                        LevelManager.S.MoveSomething(pos, movePos);
                    }
                }
            }
        }
        pos = movePos;
    }

    public static bool alarmRaised
    {
        get
        {
            return _alarmRaised;
        }
        set
        {
            _alarmRaised = value;
        }
    }
}
