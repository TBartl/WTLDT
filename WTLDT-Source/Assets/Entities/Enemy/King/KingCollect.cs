﻿using UnityEngine;
using System.Collections;

public class KingCollect : Collectable
{
	public GameObject bloodFountain;

	public override void Collect(IntVector2 pos)
	{
		Game_Manager.S.WinLevel();
		SoundManager.SM.StartStab ();
		Instantiate(bloodFountain, (Vector3)pos, Quaternion.identity);
	}
}
