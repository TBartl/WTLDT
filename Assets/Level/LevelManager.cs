using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum VisionBlock
{
	open, blocked
}

public enum LightAmount {
	black, revealed, litOutskirts, lit 
}


[System.Serializable]
public struct TileData
{
	public LightAmount light;
	public bool passable;
	public VisionBlock visionBlock;
	public GameObject occupant;

	public GameObject prefab;
	public char asciiChar;
}

public class LevelManager : MonoBehaviour {

	public static LevelManager S;

	public TextAsset textFile;
	string rawData;

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
		rawData = textFile.text;
		string correctedData = "";
		for (int i = 0; i < rawData.Length; i++)
		{
			Debug.Log(correctedData);
			if (rawData[i] != '\r')
				correctedData = correctedData + rawData[i];
		}
		rawData = correctedData;

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
						realData[x, y] = tileProperties[i];
						realData [x, y].prefab = (GameObject)Instantiate (tileProperties [i].prefab, (Vector3)new IntVector2 (x, y), Quaternion.identity);

						if (tileProperties [i].occupant != null) {
							realData [x, y].occupant = (GameObject)Instantiate (tileProperties [i].occupant, (Vector3)new IntVector2 (x, y), Quaternion.identity);
						}
					}
				}

			}
		}
	}
	

	public void MoveOccupant(IntVector2 fromPos, IntVector2 toPos) {
		// Update the unit's gameobject's position

		// Try collecting occupant
		if (realData[toPos.x, toPos.y].occupant && realData[toPos.x, toPos.y].occupant.tag == "Collectable")
		{
			realData[toPos.x, toPos.y].occupant.GetComponent<Collectable>().Collect(toPos);
			realData[toPos.x, toPos.y].occupant.GetComponent<MeshRenderer>().enabled = false;
		}

		// New tile gains reference to unit
		realData[toPos.x, toPos.y].occupant = realData[fromPos.x, fromPos.y].occupant;

		// Previous tile loses reference to unit
		realData[fromPos.x, fromPos.y].occupant = null;

		SetLightOfTile(toPos, realData[toPos.x, toPos.y].light);
	}

	public void SmoothHitCheck(IntVector2 fromPos, IntVector2 toPos)
    {
		if (!InBounds(toPos))
			return;
            if (realData[fromPos.x, fromPos.y].occupant && realData[fromPos.x, fromPos.y].occupant.tag == "Collectable" &&
                realData[toPos.x, toPos.y].occupant && realData[toPos.x, toPos.y].occupant.tag == "enemy")
            {
                realData[fromPos.x, fromPos.y].occupant.GetComponent<Collectable>().Collect(toPos);
                realData[fromPos.x, fromPos.y].occupant.GetComponent<MeshRenderer>().enabled = false;
            }
    }


	public bool InBounds (IntVector2 pos)
	{
		return (pos.x >= 0 && pos.y >= 0 && pos.x < size.x && pos.y < size.y);
	}


	public void SetLightOfTile(IntVector2 pos, LightAmount amount)
	{
		realData[pos.x, pos.y].light = amount;
		realData[pos.x, pos.y].prefab.GetComponent<Lightable>().UpdateLight(amount, true);
		if (realData[pos.x, pos.y].occupant)
			realData[pos.x, pos.y].occupant.GetComponent<Lightable>().UpdateLight(amount, true);
	}

}
