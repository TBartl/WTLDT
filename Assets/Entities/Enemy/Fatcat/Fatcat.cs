using UnityEngine;
using System.Collections;

public class Fatcat : Enemy {
    public Direction fatCatDir;
	// Use this for initialization
	protected override void Start() {
        if (fatCatDir == Direction.NORTH)
            SetRotation(0);
        else if (fatCatDir == Direction.EAST)
            SetRotation(90);
        else if (fatCatDir == Direction.SOUTH)
            SetRotation(180);
        else SetRotation(270);

		base.Start();
	}

	//FatCat looks in one direction, doesn't move unless he sees you
	public override void Move()
	{
		if (!alarmRaised) {
			if (CanSeePlayer (GetDirectionFacing ())) {
				RaiseAlarm();
			}
		}
		if (alarmRaised)
		{
			ChasePlayer();
		}
	}

	public override bool CanSeePlayer(Direction dir)
	{
		if (CanSeePlayerDirectionless())
		{
			if (dir == Direction.NORTH && PlayerMovement.S.pos.y > pos.y && PlayerMovement.S.pos.x == pos.x)
				return true;
			else if (dir == Direction.SOUTH && PlayerMovement.S.pos.y < pos.y && PlayerMovement.S.pos.x == pos.x)
				return true;
			else if (dir == Direction.EAST && PlayerMovement.S.pos.x > pos.x && PlayerMovement.S.pos.y == pos.y)
				return true;
			else if (dir == Direction.WEST && PlayerMovement.S.pos.x < pos.x && PlayerMovement.S.pos.y == pos.y)
				return true;
			else return false;
		}
		else return false;       
	}
}
