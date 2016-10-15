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

	IntVector2 size;
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


}
