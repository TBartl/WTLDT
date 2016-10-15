using UnityEngine;
using System.Collections;

public class Enemy : BaseMovement {

    public int sightDistance = 2;
	public bool turnsFirst = true;
    public static bool _alarmRaised = false;
	
	protected override void Start()
	{
		base.Start();
	}

    public virtual void Move()
    {

    }

    //For now, sees 2 in front, two to either side + 1 diagonally
	public bool CanSeePlayer(Direction dir)
    {
        if (LevelManager.S.realData[pos.x, pos.y].light == LightAmount.lit)
        {
            if (dir == Direction.NORTH && PlayerMovement.Player.pos.y >= pos.y)
                return true;
            else if (dir == Direction.SOUTH && PlayerMovement.Player.pos.y <= pos.y)
                return true;
            else if (dir == Direction.EAST && PlayerMovement.Player.pos.x >= pos.x)
                return true;
            else if (dir == Direction.WEST && PlayerMovement.Player.pos.x <= pos.x)
                return true;
            else return false;
        }
        else return false;       
    }

    public virtual void ChasePlayer()
    {
        IntVector2 playerPos = PlayerMovement.Player.pos;
        float xDif, yDif;
        xDif = Mathf.Abs(playerPos.x - pos.x);
        yDif = Mathf.Abs(playerPos.y - pos.y);
        IntVector2 movePos = pos;
        if (xDif >= yDif)
        {
            if (playerPos.x < pos.x)
            {
				if (turnsFirst && GetRotation() != 270)
					SetRotation(270);
				else
				{
					movePos.x -= 1;
					MoveIfAble(movePos);
				}
            }
            else
            {
                if (turnsFirst && GetRotation() != 90)
					SetRotation(90);
				else
                {
                    movePos.x += 1;
					MoveIfAble(movePos);
				}
            }
        }
        else
        {
            if (playerPos.y < pos.y)
            {
				if (turnsFirst && GetRotation() != 180)
					SetRotation(0);
				else
                {
                    movePos.y -= 1;
					MoveIfAble(movePos);
                }
            }
            else
            {
				if (turnsFirst && GetRotation() != 0)
					SetRotation(0);
				else
                {
                    
                    movePos.y += 1;
					MoveIfAble(movePos);
				}
            }
        }
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
