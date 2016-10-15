using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int sightDistance = 2;
    public IntVector2 pos;
    public static bool _alarmRaised = false;

	// Use this for initialization
	void Start () {
	
	}
	
    //For now, sees 2 in front, two to either side + 1 diagonally
	public bool CanSeePlayer(Direction dir)
    {
        if (LevelManager.S.realData[pos.x, pos.y].light == LightAmount.lit)
        {
            if (dir == Direction.NORTH && PlayerMovement.Player.position.y >= pos.y)
                return true;
            else if (dir == Direction.SOUTH && PlayerMovement.Player.position.y <= pos.y)
                return true;
            else if (dir == Direction.EAST && PlayerMovement.Player.position.x >= pos.x)
                return true;
            else if (dir == Direction.WEST && PlayerMovement.Player.position.x <= pos.x)
                return true;
            else return false;
        }
        else return false;       
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
}
