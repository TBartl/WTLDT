using UnityEngine;
using System.Collections;

public class PlayerVision : MonoBehaviour {

	public static PlayerVision S;

	public int radius;

	IntVector2 lastPos;

	int arraySize;
	int[,] lightValues;

	public void Awake()
	{
		arraySize = radius * 2 + 1; //Left(r) + Right(r) + Player(1)
		lightValues = new int[arraySize, arraySize];
		S = this;
	}

	void Start()
	{
		/// Nothing here but make sure the player calls OnMove right at the start
	}

	// The player movement should call this
	public void OnPlayerMove(IntVector2 newPos)
	{
		DeactivateLights();
		lastPos = newPos;
		ActivateLights(newPos);
	}


	void DeactivateLights()
	{
		for (int yIndex = 0; yIndex < arraySize; yIndex++)
		{
			int yReal = yIndex - radius - 1 + lastPos.y;
			if (yReal < 0 || yReal >= LevelManager.S.size.y)
				continue; // Out of bounds
			for (int xIndex = 0; xIndex < arraySize; xIndex++)
			{
				int xReal = xIndex - radius - 1 + lastPos.x;
				if (xReal < 0 || xReal >= LevelManager.S.size.x)
					continue; // Out of bounds

				// Change lit tiles to revealed
				if (LevelManager.S.realData[xReal, yReal].light == LightAmount.lit || LevelManager.S.realData[xReal, yReal].light == LightAmount.litOutskirts)
				{
					LevelManager.S.SetLightOfTile(new IntVector2(xReal, yReal), LightAmount.revealed);
				}

				// Reset light matrix
				lightValues[xIndex, yIndex] = 0;
			}
		}
	}

	void ActivateLights(IntVector2 newPos)
	{

		lightValues[radius + 1, radius + 1] = radius + 1; //Light player
		SpreadLightAroundPos(new IntVector2(radius + 1, radius + 1), 1); //Light around player

		GenerateLightMatrix();

		LightLevel();
	}

	void SpreadLightAroundPos(IntVector2 pos, int lightDecrease)
	{
		// Note: This function executes only on the local matrix
		int spreadLightAmount = lightValues[pos.x, pos.y] - lightDecrease;

		if (pos.x - 1 >= 0) // Left
			lightValues[pos.x - 1, pos.y] = Mathf.Max(lightValues[pos.x - 1, pos.y], spreadLightAmount);
		if (pos.x + 1 < arraySize) // Right
			lightValues[pos.x + 1, pos.y] = Mathf.Max(lightValues[pos.x + 1, pos.y], spreadLightAmount);
		if (pos.y + 1 < arraySize) // Up
			lightValues[pos.x, pos.y + 1] = Mathf.Max(lightValues[pos.x, pos.y + 1], spreadLightAmount);
		if (pos.y - 1 >= 0) // Down
			lightValues[pos.x, pos.y - 1] = Mathf.Max(lightValues[pos.x, pos.y - 1], spreadLightAmount);
	}

	void GenerateLightMatrix()
	{
		//TODO possibly optimize with another matrix of bools to determine which tiles still need to be done (or do recursively)
		for (int step = 1; step < radius; step++) // Run one time less than the radius (No need to spread 1 value lights)
		{
			for (int yIndex = 0; yIndex < arraySize; yIndex++)
			{
				int yReal = yIndex - radius - 1 + lastPos.y;
				if (yReal < 0 || yReal >= LevelManager.S.size.y)
					continue; // Out of bounds
				for (int xIndex = 0; xIndex < arraySize; xIndex++)
				{
					int xReal = xIndex - radius - 1 + lastPos.x;
					if (xReal < 0 || xReal >= LevelManager.S.size.x)
						continue; // Out of bounds

					if (LevelManager.S.realData[xReal, yReal].visionBlock == VisionBlock.open)
						SpreadLightAroundPos(new IntVector2(xIndex, yIndex), 1);
					else if (LevelManager.S.realData[xReal, yReal].visionBlock == VisionBlock.shortGrass)
						SpreadLightAroundPos(new IntVector2(xIndex, yIndex), 2);
				}
			}
		}
	}

	void LightLevel()
	{
		for (int yIndex = 0; yIndex < arraySize; yIndex++)
		{
			int yReal = yIndex - radius - 1 + lastPos.y;
			if (yReal < 0 || yReal >= LevelManager.S.size.y)
				continue; // Out of bounds
			for (int xIndex = 0; xIndex < arraySize; xIndex++)
			{
				int xReal = xIndex - radius - 1 + lastPos.x;
				if (xReal < 0 || xReal >= LevelManager.S.size.x)
					continue; // Out of bounds
				if (lightValues[xIndex, yIndex] > 1)
					LevelManager.S.SetLightOfTile(new IntVector2(xReal, yReal), LightAmount.lit);
				else if (lightValues[xIndex, yIndex] > 0)
					LevelManager.S.SetLightOfTile(new IntVector2(xReal, yReal), LightAmount.litOutskirts);
			}
		}
	}
}
