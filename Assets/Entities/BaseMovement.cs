using UnityEngine;
using System.Collections;

public class BaseMovement : MonoBehaviour {

	public IntVector2 pos;
	public static float smoothMoveTime = .1f;
	int rotation = 0;

	protected virtual void Start()
	{
		pos.x = Mathf.RoundToInt(transform.position.x);
		pos.y = Mathf.RoundToInt(transform.position.z);
	}

	protected void SetRotation(int val)
	{
		rotation = (val + 360) % 360;
		this.transform.rotation = Quaternion.Euler(0, rotation, 0);
	}
	protected int GetRotation()
	{
		return rotation;
	}

	protected Direction GetDirectionFacing()
	{
		int rotation = GetRotation();
		if ((rotation + 360) % 360 == 0)
			return Direction.NORTH;
		else if ((rotation + 360) % 360 == 90)
			return Direction.EAST;
		else if ((rotation + 360) % 360 == 180)
			return Direction.SOUTH;
		else
			return Direction.WEST;
	}	

	protected IntVector2 GetPosInfront()
	{
		Direction dir = GetDirectionFacing();
		return IntVector2.fromDirection(dir);
	}

	protected virtual void Move(IntVector2 newPos)
	{
		LevelManager.S.MoveOccupant(pos, newPos);
		StartCoroutine(SmoothMove(pos, newPos));
		pos = newPos;
	}

	protected virtual void MoveIfAble(IntVector2 newPos)
	{
		GameObject occupant = LevelManager.S.realData[newPos.x, newPos.y].occupant;
		if (LevelManager.S.InBounds(newPos)&& 
			LevelManager.S.realData[newPos.x, newPos.y].passable == true && 
			(occupant == null || (occupant != null && occupant.tag == "Collectable")))
		{
			Move(newPos);
		}
		else
		{
			StartCoroutine(SmoothHit(pos, newPos));
		}
	}

	protected IEnumerator SmoothMove(IntVector2 fromPos, IntVector2 toPos)
	{
		transform.position = (Vector3)fromPos;
		IntVector2 diff = toPos - fromPos;
		SetRotation(Mathf.RoundToInt(Mathf.Rad2Deg * Mathf.Atan2(diff.y, -diff.x) - 90));
		
		for (float f = 0; f < smoothMoveTime; f += Time.deltaTime)
		{
			float percent = f / smoothMoveTime;
			transform.position = Vector3.Lerp((Vector3)fromPos, (Vector3)toPos + Vector3.up * Mathf.Sin(percent * Mathf.PI / 2) * .5f, percent);
			yield return null;
		}
		transform.position = (Vector3)toPos;
        
	}

	protected IEnumerator SmoothHit(IntVector2 fromPos, IntVector2 toPos)
	{
		transform.position = (Vector3)fromPos;
		IntVector2 diff = toPos - fromPos;
		transform.rotation = Quaternion.Euler(0, Mathf.Rad2Deg * Mathf.Atan2(diff.y, -diff.x) - 90, 0);
		
		for (float f = 0; f < smoothMoveTime; f += Time.deltaTime)
		{
			float percent = f / smoothMoveTime;
			if (percent <=.5f)
				transform.position = Vector3.Lerp((Vector3)fromPos, (Vector3)toPos + Vector3.up * Mathf.Sin(percent * Mathf.PI / 2) * .5f, percent);
			else
				transform.position = Vector3.Lerp((Vector3)fromPos, (Vector3)toPos + Vector3.up * Mathf.Sin(percent * Mathf.PI / 2) * .5f, 1 - percent);
			yield return null;
		}
		transform.position = (Vector3)fromPos;
        LevelManager.S.SmoothHitCheck(fromPos, toPos);
    }

	

}
