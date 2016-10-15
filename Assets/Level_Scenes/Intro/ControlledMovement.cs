using UnityEngine;
using System.Collections;

public class ControlledMovement : BaseMovement {
	public void MoveActor(IntVector2 pos)
	{
		Move(pos);
	}

	public void RotateActor(int rot)
	{
		SetRotation(rot);
	}
}
