using UnityEngine;
using System.Collections;

public class Enemy : BaseMovement {

	public bool turnsFirst = true;
    public static bool _alarmRaised = false;
	
	protected override void Start()
	{
		base.Start();
	}

    public virtual void Move()
    {

    }

	public bool CanSeePlayerDirectionless()
	{
		return (LevelManager.S.realData[pos.x, pos.y].light == LightAmount.lit &&
			 LevelManager.S.realData[PlayerMovement.S.pos.x, PlayerMovement.S.pos.y].visionBlock == VisionBlock.open) ;
	}

	public bool CanSeePlayer(Direction dir)
    {
        if (CanSeePlayerDirectionless())
        {
            if (dir == Direction.NORTH && PlayerMovement.S.pos.y >= pos.y)
                return true;
            else if (dir == Direction.SOUTH && PlayerMovement.S.pos.y <= pos.y)
                return true;
            else if (dir == Direction.EAST && PlayerMovement.S.pos.x >= pos.x)
                return true;
            else if (dir == Direction.WEST && PlayerMovement.S.pos.x <= pos.x)
                return true;
            else return false;
        }
        else return false;       
    }

    public virtual void ChasePlayer()
    {
        IntVector2 playerPos = PlayerMovement.S.pos;
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
					SetRotation(180);
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
			if (_alarmRaised == false && value == true)
			{
				AudioManager.S.StartChase();
			}
            _alarmRaised = value;
        }
    }
}
