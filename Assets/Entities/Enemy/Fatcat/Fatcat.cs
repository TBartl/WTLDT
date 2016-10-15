using UnityEngine;
using System.Collections;

public class Fatcat : Enemy {

	// Use this for initialization
	protected override void Start() { 
		base.Start();
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
