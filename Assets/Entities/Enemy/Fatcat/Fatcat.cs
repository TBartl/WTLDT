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
}
