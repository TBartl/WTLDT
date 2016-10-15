using UnityEngine;
using System.Collections;

public class Bird : Enemy {

	public Direction birddir;

	// Use this for initialization
	protected override void Start () {
		base.Start();
		if (birddir == Direction.NORTH)
			SetRotation(0);
		else if (birddir == Direction.EAST)
			SetRotation(90);
		else if (birddir == Direction.SOUTH)
			SetRotation(180);
		else SetRotation(270);
	}
	
	public override void Move()
	{
		IntVector2 movePos = pos;
		IntVector2 initialPos = pos;
		if (!alarmRaised)
		{           
			if (GetDirectionFacing() == Direction.EAST)
			{
				movePos.x += 1;
				MoveIfAble(movePos);
			}
			else if (GetDirectionFacing() == Direction.WEST)
			{
				movePos.x -= 1;
				MoveIfAble(movePos);
			}
			else if (GetDirectionFacing() == Direction.NORTH)
			{
				movePos.y += 1;
				MoveIfAble(movePos);
			}
			else if (GetDirectionFacing() == Direction.SOUTH)
			{
				movePos.y -= 1;
				MoveIfAble(movePos);
			}
			//If unable to move, reverse direction
			if (pos == initialPos)
			{
				SetRotation(GetRotation() + 180);
			}
			if (CanSeePlayer(GetDirectionFacing()))
			{
				RaiseAlarm();
			}
		}
		else
		{
//			ChasePlayer();
//			//If he only turned, also move
//			if (initialPos == pos) ChasePlayer();
		}
	}



}
