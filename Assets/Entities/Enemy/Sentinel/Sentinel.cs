using UnityEngine;
using System.Collections;

public class Sentinel : Enemy {
	public bool useRandomDirection = true;

	// Use this for initialization
	protected override void Start () {
		base.Start();

		if (useRandomDirection)
		{
			float val = Random.value;
			if (val < 0.25f)
			{
				SetRotation(0);
			}
			else if (val < 0.5f)
			{
				SetRotation(90);
			}
			else if (val < 0.75f)
			{
				SetRotation(180);
			}
			else
			{
				SetRotation(270);
			}
		}
    }
	

    //Sentinel looks around, doesn't move unless he sees you
	public override void Move()
    {
        if (!alarmRaised)
		{
			SetRotation((GetRotation() + 90));
			if (CanSeePlayer(GetDirectionFacing()))
			{
				alarmRaised = true;
			}
		}
        else
        {
            ChasePlayer();
        }
    }
}
