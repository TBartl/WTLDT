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

    int currentHillIndex;
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
                //Animate it coming out of hole
                //------------------------------------
                curStep = StepInProcess.Up;
            }
            else if (curStep == StepInProcess.Up)
            {
                //Animate it going into hole
                //----------------------------------
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
        transform.position = (Vector3)newPos;
        pos = newPos;
    }
}
