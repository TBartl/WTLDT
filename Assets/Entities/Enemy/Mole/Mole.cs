using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mole : Enemy
{

	GameObject meshGO;

	enum StepInProcess
	{
		Arrived,
		Up
	}

	public Direction moleDir;
	public IntVector2[] hillLocations;

	int currentHillIndex;
	StepInProcess curStep;

	// Use this for initialization
	protected override void Start()
	{
		base.Start();
		if (moleDir == Direction.NORTH)
			SetRotation(0);
		else if (moleDir == Direction.EAST)
			SetRotation(90);
		else if (moleDir == Direction.SOUTH)
			SetRotation(180);
		else SetRotation(270);
		if (hillLocations == null)
		{
			hillLocations = new IntVector2[1];
			hillLocations[1] = pos;
		}
		currentHillIndex = 0;
		curStep = StepInProcess.Arrived;
		meshGO = transform.FindChild("Mole").gameObject;
	}

	public override void Move()
	{
		if (!alarmRaised)
		{
			if (curStep == StepInProcess.Arrived)
			{
				//Animate it coming out of hole
				//------------------------------------
				curStep = StepInProcess.Up;
			}
			else if (curStep == StepInProcess.Up)
			{
				//Animate it going into hole
				//----------------------------------

				IntVector2 initialPos = pos;
				currentHillIndex++;
				if (currentHillIndex >= hillLocations.Length)
				{
					currentHillIndex = 0;
				}
				IntVector2 movePos = hillLocations[currentHillIndex];
				if (movePos != pos) //there are more than one mole hill
				{
					MoveIfAble(movePos);
					if (pos != initialPos) // you moved, so change to arrived
					{
						curStep = StepInProcess.Arrived;
					}
				}
				else // there is only one mole hill
				{
					curStep = StepInProcess.Arrived;
				}

			}
			if (CanSeePlayer(GetDirectionFacing()))
			{
				RaiseAlarm();
			}
		}
	}

	protected override void MoveIfAble(IntVector2 newPos)
	{
		if (LevelManager.S.InBounds(newPos) &&
			LevelManager.S.realData[newPos.x, newPos.y].passable == true &&
			(LevelManager.S.realData[newPos.x, newPos.y].occupant == null || (LevelManager.S.realData[newPos.x, newPos.y].occupant != null && LevelManager.S.realData[newPos.x, newPos.y].occupant.tag == "Collectable")))
		{
			Move(newPos);
		}
	}

	protected override void Move(IntVector2 newPos)
	{
		LevelManager.S.MoveOccupant(pos, newPos);
		StartCoroutine(AnimateMole(pos, newPos));
		pos = newPos;
	}

	IEnumerator AnimateMole(IntVector2 fromPos, IntVector2 toPos)
	{
		this.transform.position = (Vector3)fromPos;
		float length = smoothMoveTime;
		for (float f = 0; f < length; f += Time.deltaTime)
		{
			meshGO.transform.localPosition = Vector3.Lerp(Vector3.zero, Vector3.down * .5f, f / length);
			yield return null;
		}
		this.transform.position = (Vector3)toPos;
		for (float f = 0; f < length; f += Time.deltaTime)
		{
			meshGO.transform.localPosition = Vector3.Lerp(Vector3.down * .5f, Vector3.zero, f / length);
			yield return null;
		}
	}
}
