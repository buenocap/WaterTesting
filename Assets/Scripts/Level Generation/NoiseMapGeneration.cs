/*Pedro Bueno
 File Description: This file contains code that helps in generating a noise maps this is what allows the game to generate unqiue tiles to create a different world environment every single time*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Wave class object attributes
[System.Serializable]
public class Wave
{
    public float seed;
    public float frequency;
    public float amplitude;
}


// Main code
public class NoiseMapGeneration : MonoBehaviour
{

	static float randomSeed;        //Variable will contain a random number generated

	static int seedIndex = 0; //used to retrieve seeds from savedata - Christian

    void Start()
	{
		//generate random seed if not continuing level, otherwise, load from SaveData - Christian
		if (MainMenu.isContinue)
		{
			randomSeed = GameManager.SaveData.LevelSeeds[0];
		}
		else
		{
			randomSeed = Random.Range(1.0f, 9999.0f);   //Random number is stored

			GameManager.SaveData.LevelSeeds.Add(randomSeed);
			SaveManager.Save(GameManager.SaveData);
		}
    }


	// Generates a non-uniform perlin noise map
	public float[,] GeneratePerlinNoiseMap(int mapDepth, int mapWidth, float scale, float offsetX, float offsetZ, Wave[] waves)
	{
		// Creating an empty noise map with the mapDepth and mapWidth coordinates
		float[,] noiseMap = new float[mapDepth, mapWidth];

		for (int zIndex = 0; zIndex < mapDepth; zIndex++)
		{
			for (int xIndex = 0; xIndex < mapWidth; xIndex++)
			{
				// Calculating sample indices based on the coordinates, the scale and the offset
				float sampleX = (xIndex + offsetX) / scale;
				float sampleZ = (zIndex + offsetZ) / scale;

				float noise = 0f;
				float normalization = 0f;

				foreach (Wave wave in waves)
				{
                    // Generating noise value using PerlinNoise for a given Wave **UPDATED** now with the addition of a seed modifier.
                    noise += wave.amplitude * Mathf.PerlinNoise(sampleX * wave.frequency + wave.seed + randomSeed, sampleZ * wave.frequency + wave.seed + randomSeed);
                    normalization += wave.amplitude;
                }
				// Normalizing the noise value so that it is within 0 and 1 float values
				noise /= normalization;

				noiseMap [zIndex, xIndex] = noise;
			}
		}

		return noiseMap;
	}

	// Generates a uniform perlin noise map, this is mainly used in heat and moisture map
	public float[,] GenerateUniformNoiseMap(int mapDepth, int mapWidth, float centerVertexZ, float maxDistanceZ, float offsetZ)
	{
		// create an empty noise map with the mapDepth and mapWidth coordinates
		float[,] noiseMap = new float[mapDepth, mapWidth];

		for (int zIndex = 0; zIndex < mapDepth; zIndex++) {
			// Calculating the sampleZ by summing the index and the offset
			float sampleZ = zIndex + offsetZ;
			// Calculate the noise proportional to the distance of the sample to the center of the level
			float noise = Mathf.Abs (sampleZ - centerVertexZ) / maxDistanceZ;

			// Apply the noise for all points with this Z coordinate
			for (int xIndex = 0; xIndex < mapWidth; xIndex++) {
				noiseMap [mapDepth - zIndex - 1, xIndex] = noise;
			}
		}

		return noiseMap;
	}
}
