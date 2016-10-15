using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mole : Enemy {

    enum StepInProcess
    {
        Arrived,
        Up,
        Down
    }

    public Direction moleDir;
    public IntVector2[] hillLocations;
    public float yChange = 0.5f;

    int currentHillIndex;
    bool outOfHole;
    StepInProcess curStep;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        if (moleDir == Direction.NORTH)
            SetRotation(0);
        else if (moleDir == Direction.EAST)
            SetRotation(90);
        else if (moleDir == Direction.SOUTH)
            SetRotation(180);
        else SetRotation(270);
        if(hillLocations == null)
        {
            hillLocations = new IntVector2[1];
            hillLocations[1] = pos;
        }
        currentHillIndex = 0;
        curStep = StepInProcess.Arrived;
    }

    public override void Move()
    {
        if (!alarmRaised)
        {
            if (curStep == StepInProcess.Arrived)
            {
                ChangeElevation(yChange);
                curStep = StepInProcess.Up;
            }
            else if (curStep == StepInProcess.Up)
            {
                ChangeElevation(-yChange);
                curStep = StepInProcess.Down;
            }
            else if (curStep == StepInProcess.Down)
            {
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
            if (curStep == StepInProcess.Up)
            {
                if (CanSeePlayer(GetDirectionFacing()))
                {
                    RaiseAlarm();
                }
            }
        }
    }

    public IEnumerator ChangeElevation(float changeInY)
    {
        transform.position = (Vector3)pos;
        Vector3 fromPos = (Vector3)pos;
        Vector3 toPos = fromPos;
        toPos.y += changeInY;
        for (float f = 0; f < smoothMoveTime; f += Time.deltaTime)
        {
            float percent = f / smoothMoveTime;
            transform.position = Vector3.Lerp((Vector3)fromPos, (Vector3)toPos + Vector3.up * Mathf.Sin(percent * Mathf.PI / 2) * .5f, percent);
            yield return null;
        }
        transform.position = (Vector3)toPos;
    }
}
