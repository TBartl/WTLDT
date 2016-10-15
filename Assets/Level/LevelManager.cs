using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum LightAmount {
	black, fogOfWar, revealed
}


[System.Serializable]
public struct TileData
{
	public LightAmount light;
	public bool passable;
	public GameObject occupant;

	public GameObject prefab;
	public char asciiChar;
}

public class LevelManager : MonoBehaviour {

	public static LevelManager S;

	[TextArea(3, 10)]
	public string rawData;

	public IntVector2 size;
	public TileData[,] realData;

	public List<TileData> tileProperties;

	void Awake()
	{
		S = this;
		GenerateRealDataAndDraw();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GenerateRealDataAndDraw()
	{

		size.x = rawData.IndexOf('\n');
		size.y = (rawData.Length + 1) / (size.x + 1);
		realData = new TileData[size.x, size.y];

		for (int y = 0; y < size.y; y += 1)
		{
			for (int x = 0; x < size.x; x += 1)
			{
				char c = rawData[y * (size.x + 1) + x];

				realData[x, y] = tileProperties[0];
				for (int i = 0; i < tileProperties.Count; i ++ )
				{
					if (c == tileProperties[i].asciiChar)
					{
						Instantiate(tileProperties[i].prefab, (Vector3)new IntVector2(x, y), Quaternion.identity);
						if (tileProperties[i].occupant != null)
							Instantiate(tileProperties[i].occupant, (Vector3)new IntVector2(x, y), Quaternion.identity);
						realData[x, y] = tileProperties[i];
					}
				}

			}
		}
	}

	// Move unit by one block. This handles tile properties and moves the gameobject. Used for both player and enemies.
	public void MoveUnitByOne(IntVector2 fromPos, IntVector2 toPos) {
		Debug.Log ("next: " + fromPos.x + ", " + fromPos.y);
		// Update the unit's gameobject's position
		Vector3 destination = (Vector3)toPos;
		realData [fromPos.x, fromPos.y].occupant.transform.position = destination;

//		Vector3 destination = Vector3(toPos);
//		realData [toPos.x, toPos.y].occupant.transform.position = destination;
		GameObject unit = realData[fromPos.x, fromPos.y].occupant;

		// New tile gains reference to unit
		realData[toPos.x, toPos.y].occupant = unit;

		// Previous tile loses reference to unit
		realData[fromPos.x, fromPos.y].occupant = null;

		UpdateUnitPosition (unit, toPos);
	}

	public void UpdateUnitPosition (GameObject unit, IntVector2 new_position) {
		Vector3 destination_position = new Vector3 (new_position.x, 0, new_position.y);
		unit.transform.position = destination_position;
	}

}
