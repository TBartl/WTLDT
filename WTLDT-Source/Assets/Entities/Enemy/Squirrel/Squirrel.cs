using UnityEngine;
using System.Collections;

public class Squirrel : Enemy {

    public Direction squirrelDir;
	int moveDistance;

	// Use this for initialization
	protected override void Start () {
		moveDistance = 2;
        base.Start();
		if (squirrelDir == Direction.NORTH)
            SetRotation(0);
		else if (squirrelDir == Direction.EAST)
            SetRotation(90);
		else if (squirrelDir == Direction.SOUTH)
            SetRotation(180);
        else SetRotation(270);
    }

    public override void Move()
    {
        IntVector2 movePos = pos;
        IntVector2 initialPos = pos;
        if (!alarmRaised)
        {           

			if (moveDistance <= 0) {
				SetRotation(GetRotation() + 90);
				moveDistance = 2;
			}


            if (GetDirectionFacing() == Direction.EAST)
            {
                movePos.x += 1;
                MoveIfAble(movePos);
				moveDistance--;
            }
            else if (GetDirectionFacing() == Direction.WEST)
            {
                movePos.x -= 1;
                MoveIfAble(movePos);
				moveDistance--;
            }
            else if (GetDirectionFacing() == Direction.NORTH)
            {
                movePos.y += 1;
                MoveIfAble(movePos);
				moveDistance--;
            }
            else if (GetDirectionFacing() == Direction.SOUTH)
            {
                movePos.y -= 1;
                MoveIfAble(movePos);
				moveDistance--;
            }
//            //If unable to move, reverse direction
//            if (pos == initialPos)
//            {
//                SetRotation(GetRotation() + 180);
//            }
            if (CanSeePlayer(GetDirectionFacing()))
            {
                RaiseAlarm();
            }
        }
        else
        {
            ChasePlayer();
            //If he only turned, also move
            if (initialPos == pos) ChasePlayer();
        }
    }
}
