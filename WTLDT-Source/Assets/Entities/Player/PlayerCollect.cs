﻿using UnityEngine;
using System.Collections;

public class PlayerCollect : Collectable {
	public GameObject bloodFountain;

	public override void Collect(IntVector2 pos)
	{
		Game_Manager.S.FailLevel();
		SoundManager.SM.StartStab();
		Instantiate(bloodFountain,(Vector3)pos, Quaternion.identity);
	}
}
