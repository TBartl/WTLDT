using UnityEngine;
using System.Collections;

public class Fatcat : Enemy {


	public Fatcat(Direction start_direction) {
		currentDirection = start_direction;
	}

	// Use this for initialization
	void Start () {

	}


	//Sentinel looks around, doesn't move unless he sees you
	public override void Move()
	{
		if (alarmRaised)
		{
			ChasePlayer();
		}
	}
}
