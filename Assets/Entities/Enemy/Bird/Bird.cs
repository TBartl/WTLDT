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

	protected override void MoveIfAble(IntVector2 newPos)
	{
		if (LevelManager.S.InBounds(newPos) && 
			(LevelManager.S.realData[newPos.x, newPos.y].passable == true || LevelManager.S.realData[newPos.x, newPos.y].prefab.tag == "Water") && 
			(LevelManager.S.realData[newPos.x, newPos.y].occupant == null || (LevelManager.S.realData[newPos.x, newPos.y].occupant != null && LevelManager.S.realData[newPos.x, newPos.y].occupant.tag == "Collectable")))
		{
			Move(newPos);
		}
		else
		{
			StartCoroutine(SmoothHit(pos, newPos));
		}
	}

	public override bool CanSeePlayer(Direction dir)
	{
		if (CanSeePlayerDirectionless())
		{
			if (Mathf.Abs (PlayerMovement.S.pos.x - pos.x) <= 1 && Mathf.Abs (PlayerMovement.S.pos.y - pos.y) <= 1) {
				return true;
			}
			else return false;
		}
		else return false;       
	}

}
