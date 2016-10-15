using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int sightDistance = 2;
    IntVector2 pos;

	// Use this for initialization
	void Start () {
	
	}
	
    //For now, sees 2 in front, two to either side + 1 diagonally
	bool canSeePlayer(Direction facing)
    {
        int layer = 0;
        if(facing == Direction.NORTH)
        {
            
            for(int x = 0; x < sightDistance; x++)
            {
                for(int y = 0; y < sightDistance - layer; y++)
                {
                    if (x == 0 && y == 0) continue;
                    if(LevelManager.S.realData[pos.x + x, pos.y + y].occupant != null)
                    {
                        if (LevelManager.S.realData[pos.x + x, pos.y + y].occupant.tag == "Player")
                        {
                            return true;
                        }
                        
                    }
                    
                }
            }
        }
        else if(facing == Direction.SOUTH)
        {

        }
        else if(facing == Direction.EAST)
        {

        }
        else if(facing == Direction.WEST)
        {

        }
        return true;
    }
}
