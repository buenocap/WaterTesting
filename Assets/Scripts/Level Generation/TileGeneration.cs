/*Pedro Bueno
 File Description: This file contains code that helps in generating how a tile is texture is created and displayed on to the game. It's also what will allow us to view the different types of maps implemented. These terrains
get combined together to idenfity a biome.*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Terrain class object attributes
[System.Serializable]
public class TerrainType
{
    public string name;
    public float threshold;
    public Color color;
    public int index;
}

// Biome class object attributes
[System.Serializable]
public class Biome
{
    public string name;
    public Color color;
    public int index;
}


// Biomerow class object attribute -- allows us to have a 2D array with Biome class to represent a biome correctly
[System.Serializable]
public class BiomeRow
{
    public Biome[] biomes;
}

// Tile class will store all the data information of a tile
public class TileData
{
	public float[,] heightMap;
	public float[,] heatMap;
	public float[,] moistureMap;
	public TerrainType[,] chosenHeightTerrainTypes;
	public TerrainType[,] chosenHeatTerrainTypes;
	public TerrainType[,] chosenMoistureTerrainTypes;
	public Biome[,] chosenBiome;
	public Mesh mesh;

	//Sets all the data
	public TileData(float[,] heightMap, float[,] heatMap, float[,] moistureMap,
		TerrainType[,] chosenHeightTerrainTypes, TerrainType[,] chosenHeatTerrainTypes, TerrainType[,] chosenMoistureTerrainTypes,
		Biome[,] chosenBiome, Mesh mesh)
	{
		this.heightMap = heightMap;
		this.heatMap = heatMap;
		this.moistureMap = moistureMap;
		this.chosenHeightTerrainTypes = chosenHeightTerrainTypes;
		this.chosenHeatTerrainTypes = chosenHeatTerrainTypes;
		this.chosenMoistureTerrainTypes = chosenMoistureTerrainTypes;
		this.chosenBiome = chosenBiome;
		this.mesh = mesh;
	}
}


// Main Tile Generation Code
public class TileGeneration : MonoBehaviour
{
	// Field that allows us to include noise map generation in tilegeneration
	[SerializeField]
	NoiseMapGeneration noiseMapGeneration;

	// Fields that allows us to select what object mesh properites our tile will us, by default it uses the 3D object "plane" mesh properties
	[SerializeField]
	private MeshRenderer tileRenderer;

	[SerializeField]
	private MeshFilter meshFilter;

	[SerializeField]
	private MeshCollider meshCollider;

	// Field that allows us to change the scale of the height map
	[SerializeField]
	private float levelScale;

	// Fields that allow us to input and identify the different types of terrains, heat, and moisture in game
	[SerializeField]
	private TerrainType[] heightTerrainTypes;

	[SerializeField]
	private TerrainType[] heatTerrainTypes;

	[SerializeField]
	private TerrainType[] moistureTerrainTypes;

	// Field that allows us to change the intensity of the heights in the map
	[SerializeField]
	private float heightMultiplier;

	// Fields that allow us to correct height levels meaning any height that is below the specified threshold is considered a Zero this is what allows our water areas to be completely flat, same can be applied to heat and moisture
	[SerializeField]
	private AnimationCurve heightCurve;

	[SerializeField]
	private AnimationCurve heatCurve;

	[SerializeField]
	private AnimationCurve moistureCurve;

	// Wave fields allow act as a polish and also allows us to change the tile via a seed, which allows tiles to be completely different from each other
	[SerializeField]
	private Wave[] heightWaves;

	[SerializeField]
	private Wave[] heatWaves;

	[SerializeField]
	private Wave[] moistureWaves;

	// Field allows us to specify our different biomes
	[SerializeField]
	private BiomeRow[] biomes;

	// Default color of water 
	[SerializeField]
	private Color waterColor;

	// Field allows us to toggle the map view between, height, heat, moisture, and biomes
	[SerializeField]
	private VisualizationMode visualizationMode;

	public TileData GenerateTile(float centerVertexZ, float maxDistanceZ)
	{
		// Calculating tile depth and width based on the mesh vertices
		Vector3[] meshVertices = this.meshFilter.mesh.vertices;
		int tileDepth = (int)Mathf.Sqrt (meshVertices.Length);
		int tileWidth = tileDepth;

		// Calculating the offsets based on the tile position
		float offsetX = -this.gameObject.transform.position.x;
		float offsetZ = -this.gameObject.transform.position.z;

		// Generating a heightMap using Perlin Noise
		float[,] heightMap = this.noiseMapGeneration.GeneratePerlinNoiseMap (tileDepth, tileWidth, this.levelScale, offsetX, offsetZ, this.heightWaves);

		// Calculating vertex offset based on the Tile position and the distance between vertices
		Vector3 tileDimensions = this.meshFilter.mesh.bounds.size;
		float distanceBetweenVertices = tileDimensions.z / (float)tileDepth;
		float vertexOffsetZ = this.gameObject.transform.position.z / distanceBetweenVertices;

		// Generate a heatMap using uniform noise
		float[,] uniformHeatMap = this.noiseMapGeneration.GenerateUniformNoiseMap (tileDepth, tileWidth, centerVertexZ, maxDistanceZ, vertexOffsetZ);

		// Generate a heatMap using Perlin Noise
		float[,] randomHeatMap = this.noiseMapGeneration.GeneratePerlinNoiseMap (tileDepth, tileWidth, this.levelScale, offsetX, offsetZ, this.heatWaves);
		float[,] heatMap = new float[tileDepth, tileWidth];
		for (int zIndex = 0; zIndex < tileDepth; zIndex++)
		{
			for (int xIndex = 0; xIndex < tileWidth; xIndex++)
			{
				// Mix both heat maps together by multiplying their values
				heatMap [zIndex, xIndex] = uniformHeatMap [zIndex, xIndex] * randomHeatMap [zIndex, xIndex];
				// Makes higher regions colder, by adding the height value to the heat map
				heatMap [zIndex, xIndex] += this.heatCurve.Evaluate(heightMap [zIndex, xIndex]) * heightMap [zIndex, xIndex];
			}
		}

		// Generate a moistureMap using Perlin Noise
		float[,] moistureMap = this.noiseMapGeneration.GeneratePerlinNoiseMap (tileDepth, tileWidth, this.levelScale, offsetX, offsetZ, this.moistureWaves);
		for (int zIndex = 0; zIndex < tileDepth; zIndex++)
		{
			for (int xIndex = 0; xIndex < tileWidth; xIndex++)
			{
				// Makes higher regions dryer, by reducing the height value from the heat map
				moistureMap [zIndex, xIndex] -= this.moistureCurve.Evaluate(heightMap [zIndex, xIndex]) * heightMap [zIndex, xIndex];
			}
		}

		// Build a Texture2D from the height map
		TerrainType[,] chosenHeightTerrainTypes = new TerrainType[tileDepth, tileWidth];
		Texture2D heightTexture = BuildTexture (heightMap, this.heightTerrainTypes, chosenHeightTerrainTypes);
		// Build a Texture2D from the heat map
		TerrainType[,] chosenHeatTerrainTypes = new TerrainType[tileDepth, tileWidth];
		Texture2D heatTexture = BuildTexture (heatMap, this.heatTerrainTypes, chosenHeatTerrainTypes);
		// Build a Texture2D from the moisture map
		TerrainType[,] chosenMoistureTerrainTypes = new TerrainType[tileDepth, tileWidth];
		Texture2D moistureTexture = BuildTexture (moistureMap, this.moistureTerrainTypes, chosenMoistureTerrainTypes);

		// Build a biomes Texture2D from the three other noise variables

		Biome[,] chosenBiome = new Biome[tileDepth, tileWidth];
		Texture2D biomeTexture = BuildBiomeTexture(chosenHeightTerrainTypes, chosenHeatTerrainTypes, chosenMoistureTerrainTypes, chosenBiome);

		switch (this.visualizationMode)
		{
		case VisualizationMode.Height:
			// Assign material texture to be the heightTexture
			this.tileRenderer.material.mainTexture = heightTexture;
			break;
		case VisualizationMode.Heat:
			// Assign material texture to be the heatTexture
			this.tileRenderer.material.mainTexture = heatTexture;
			break;
		case VisualizationMode.Moisture:
			// Assign material texture to be the moistureTexture
			this.tileRenderer.material.mainTexture = moistureTexture;
			break;
		case VisualizationMode.Biome:
			// Assign material texture to be the moistureTexture
			this.tileRenderer.material.mainTexture = biomeTexture;
			break;
		}

		// Update the tile mesh vertices according to the height map
		UpdateMeshVertices (heightMap);
		// Generate tile data
		TileData tileData = new TileData(heightMap, heatMap, moistureMap, chosenHeightTerrainTypes, chosenHeatTerrainTypes, chosenMoistureTerrainTypes, chosenBiome, this.meshFilter.mesh);
		return tileData;
	}

	// Function that allows us to generate the correct texture based on the terrain
	private Texture2D BuildTexture(float[,] heightMap, TerrainType[] terrainTypes, TerrainType[,] chosenTerrainTypes)
	{
		int tileDepth = heightMap.GetLength (0);
		int tileWidth = heightMap.GetLength (1);

		Color[] colorMap = new Color[tileDepth * tileWidth];
		for (int zIndex = 0; zIndex < tileDepth; zIndex++)
		{
			for (int xIndex = 0; xIndex < tileWidth; xIndex++)
			{
				// Transform the 2D map index is an Array index
				int colorIndex = zIndex * tileWidth + xIndex;
				float height = heightMap [zIndex, xIndex];
				// Choose a terrain type according to the height value
				TerrainType terrainType = ChooseTerrainType (height, terrainTypes);
				// Assign the color according to the terrain type
				colorMap[colorIndex] = terrainType.color;

				// Save the chosen terrain type
				chosenTerrainTypes [zIndex, xIndex] = terrainType;
			}
		}

		// Create a new texture and set its pixel colors
		Texture2D tileTexture = new Texture2D (tileWidth, tileDepth);
		tileTexture.wrapMode = TextureWrapMode.Clamp;
		tileTexture.SetPixels (colorMap);
		tileTexture.Apply ();

		return tileTexture;
	}

	TerrainType ChooseTerrainType(float noise, TerrainType[] terrainTypes)
	{
		// For each terrain type, check if the height is lower than the one for the terrain type
		foreach (TerrainType terrainType in terrainTypes)
		{
			// Return the first terrain type whose height is higher than the generated one
			if (noise < terrainType.threshold)
			{
				return terrainType;
			}
		}
		return terrainTypes [terrainTypes.Length - 1];
	}

	private void UpdateMeshVertices(float[,] heightMap)
	{
		int tileDepth = heightMap.GetLength (0);
		int tileWidth = heightMap.GetLength (1);

		Vector3[] meshVertices = this.meshFilter.mesh.vertices;

		// Iterate through all the heightMap coordinates, updating the vertex index
		int vertexIndex = 0;
		for (int zIndex = 0; zIndex < tileDepth; zIndex++)
		{
			for (int xIndex = 0; xIndex < tileWidth; xIndex++)
			{
				float height = heightMap [zIndex, xIndex];

				Vector3 vertex = meshVertices [vertexIndex];
				// Change the vertex Y coordinate, proportional to the height value. The height value is evaluated by the heightCurve function, in order to correct it.
				meshVertices[vertexIndex] = new Vector3(vertex.x, this.heightCurve.Evaluate(height) * this.heightMultiplier, vertex.z);

				vertexIndex++;
			}
		}

		// Updating the vertices in the mesh and update its properties
		this.meshFilter.mesh.vertices = meshVertices;
		this.meshFilter.mesh.RecalculateBounds ();
		this.meshFilter.mesh.RecalculateNormals ();
		// Updating the mesh collider
		this.meshCollider.sharedMesh = this.meshFilter.mesh;
	}

	private Texture2D BuildBiomeTexture(TerrainType[,] heightTerrainTypes, TerrainType[,] heatTerrainTypes, TerrainType[,] moistureTerrainTypes, Biome[,] chosenBiome)
	{
		int tileDepth = heatTerrainTypes.GetLength (0);
		int tileWidth = heatTerrainTypes.GetLength (1);

		Color[] colorMap = new Color[tileDepth * tileWidth];

		for (int zIndex = 0; zIndex < tileDepth; zIndex++)
		{
			for (int xIndex = 0; xIndex < tileWidth; xIndex++)
			{
				int colorIndex = zIndex * tileWidth + xIndex;

				TerrainType heightTerrainType = heightTerrainTypes [zIndex, xIndex];
				// Check if the current coordinate is a water region

				if (heightTerrainType.name != "water")
				{
					// If a coordinate is not water, its biome will be defined by the heat and moisture values
					TerrainType heatTerrainType = heatTerrainTypes [zIndex, xIndex];
					TerrainType moistureTerrainType = moistureTerrainTypes [zIndex, xIndex];

					// Terrain type index is used to access the biomes table
					Biome biome = this.biomes [moistureTerrainType.index].biomes [heatTerrainType.index];
					// Assign the color according to the selected biome
					colorMap [colorIndex] = biome.color;
					// Save biome in chosenBiome matrix only when it is not in water
					chosenBiome[zIndex, xIndex] = biome;
				} 
				else
				{
					// Water regions don't have biomes, they always have the same color
					colorMap [colorIndex] = this.waterColor;
				}
			}
		}

		// Create a new texture and set its pixel colors
		Texture2D tileTexture = new Texture2D (tileWidth, tileDepth);
		tileTexture.wrapMode = TextureWrapMode.Clamp;
		tileTexture.SetPixels (colorMap);
		tileTexture.Apply ();

		return tileTexture;
	}
}

enum VisualizationMode {Height, Heat, Moisture, Biome}