using UnityEngine;
using System.Collections;

public class Enemy : BaseMovement {

	public bool turnsFirst = true;
    public static bool _alarmRaised = false;
	
	protected override void Start()
	{
		base.Start();
		alarmRaised = false;
	}

    public virtual void Move()
    {

    }

	public bool CanSeePlayerDirectionless()
	{
		return (LevelManager.S.realData[pos.x, pos.y].light == LightAmount.lit &&
			 LevelManager.S.realData[PlayerMovement.S.pos.x, PlayerMovement.S.pos.y].visionBlock == VisionBlock.open) ;
	}

	public virtual bool CanSeePlayer(Direction dir)
    {
        if (CanSeePlayerDirectionless())
        {
            if (dir == Direction.NORTH && PlayerMovement.S.pos.y > pos.y)
                return true;
            else if (dir == Direction.SOUTH && PlayerMovement.S.pos.y < pos.y)
                return true;
            else if (dir == Direction.EAST && PlayerMovement.S.pos.x > pos.x)
                return true;
            else if (dir == Direction.WEST && PlayerMovement.S.pos.x < pos.x)
                return true;
            else return false;
        }
        else return false;       
    }

    public virtual void ChasePlayer()
    {
        IntVector2 playerPos = PlayerMovement.S.pos;
        float xDif, yDif;
        xDif = Mathf.Abs(playerPos.x - pos.x);
        yDif = Mathf.Abs(playerPos.y - pos.y);
        IntVector2 movePos = pos;
        if (xDif >= yDif)
        {
            if (playerPos.x < pos.x)
            {
				if (turnsFirst && GetRotation() != 270)
					SetRotation(270);
				else
				{
					movePos.x -= 1;
					MoveIfAble(movePos);
				}
            }
            else
            {
                if (turnsFirst && GetRotation() != 90)
					SetRotation(90);
				else
                {
                    movePos.x += 1;
					MoveIfAble(movePos);
				}
            }
        }
        else
        {
            if (playerPos.y < pos.y)
            {
				if (turnsFirst && GetRotation() != 180)
					SetRotation(180);
				else
                {
                    movePos.y -= 1;
					MoveIfAble(movePos);
                }
            }
            else
            {
				if (turnsFirst && GetRotation() != 0)
					SetRotation(0);
				else
                {
                    
                    movePos.y += 1;
					MoveIfAble(movePos);
				}
            }
        }
    }

    public static bool alarmRaised
    {
        get
        {
            return _alarmRaised;
        }
        set
        {
            _alarmRaised = value;
        }
    }

	protected void RaiseAlarm()
	{
		alarmRaised = true;
		AudioManager.S.StartChase();
		SoundManager.SM.StartWhistle();

		// if in credit level, should not flash red
		Redpanel.RP.StartFlash();

		StartCoroutine(Hop());

		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Vision"))
		{
			g.SetActive(false);
		}
		StartCoroutine(PreserveThisVisionForASecond());
		
	}

	IEnumerator Hop()
	{
		Vector3 position = this.transform.position;
		float length = .3f;
		for (float f = 0; f < length; f += Time.deltaTime)
		{
			float percent = f / length;
			transform.position = position + .6f * Vector3.up * Mathf.Sin(percent * Mathf.PI);
			yield return null;
		}
		this.transform.position = (Vector3)pos;
	}

	IEnumerator PreserveThisVisionForASecond()
	{
		GameObject vision = this.transform.FindChild("Vision").gameObject;

		vision.SetActive(true);
		for (float f = 0; f < 1f; f+= Time.deltaTime)
			yield return null;

		vision.SetActive(false);
	}
}
