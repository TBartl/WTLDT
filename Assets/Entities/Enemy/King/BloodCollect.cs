using UnityEngine;
using System.Collections;

public class BloodCollect : Collectable
{
	public GameObject bloodFountain;

	public override void Collect(IntVector2 pos)
	{
		SoundManager.SM.StartStab ();
		Instantiate(bloodFountain, (Vector3)pos, Quaternion.identity);
	}
}
