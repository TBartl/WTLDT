using UnityEngine;
using System.Collections;

public class BaseMovement : MonoBehaviour {

	public IntVector2 pos;
	public static float smoothMoveTime = .1f;
	int rotation = 0;

	Animation anim;

	protected virtual void Start()
	{
		pos.x = Mathf.RoundToInt(transform.position.x);
		pos.y = Mathf.RoundToInt(transform.position.z);
		rotation = Mathf.RoundToInt(rotation);
		anim = this.GetComponent<Animation>();
	}

	protected void SetRotation(int val)
	{
		val = (val + 360) % 360;
		StartCoroutine(SmoothTurn(rotation, val));
		rotation = val;
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
		if (LevelManager.S.InBounds(newPos) && 
			LevelManager.S.realData[newPos.x, newPos.y].passable == true && 
			(LevelManager.S.realData[newPos.x, newPos.y].occupant == null || (LevelManager.S.realData[newPos.x, newPos.y].occupant != null && LevelManager.S.realData[newPos.x, newPos.y].occupant.tag == "Collectable")))
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
		if (anim)
			anim.Play("Jump");
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

		if (anim)
			anim.Play("Idle");
	}

	protected IEnumerator SmoothHit(IntVector2 fromPos, IntVector2 toPos)
	{
		if (anim)
			anim.Play("Jump");

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

		if (anim)
			anim.Play("Idle");
	}

	protected IEnumerator SmoothTurn(int fromRot, int toRot)
	{
		transform.rotation = Quaternion.Euler(0, fromRot, 0);
		for (float f = 0; f < smoothMoveTime; f += Time.deltaTime)
		{
			float percent = f / smoothMoveTime;
			transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, fromRot, 0), Quaternion.Euler(0, toRot, 0), percent);
			yield return null;
		}
		transform.rotation = Quaternion.Euler(0, toRot, 0);
	}
	

}
