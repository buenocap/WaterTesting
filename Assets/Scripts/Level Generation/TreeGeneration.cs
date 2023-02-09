/*Pedro Bueno
 This code will allow us to place trees on the level map based on different factors such as trees being able to only be found in certain biomes; This algorithm will be the basis for
the findable objects hiding algorithm.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGeneration : MonoBehaviour
{
    // Field that allows the selection of the noise map generation script
    [SerializeField]
    private NoiseMapGeneration noiseMapGeneration;

    // Field that allows us to input the seed, frequency, and amplitude (allows for procedural placement of the trees)
    [SerializeField]
    private Wave[] waves;

    // 
    [SerializeField]
    private float levelScale;

    // Will allow us to change how close new trees can be placed next to each other, 0 - No space between -> higher num = more space between trees / asset
    [SerializeField]
    private float[] neighborRadius;

    // Field that allows us to select which trees we want to add to the level
    [SerializeField]
    private GameObject[] treePrefab;


    public void GenerateTrees(int mapDepth, int mapWidth, float distanceBetweenVertices, LevelData levelData)
    {
        // Generate a tree noise map using Perlin Noise
        float[,] treeMap = this.noiseMapGeneration.GeneratePerlinNoiseMap(mapDepth, mapWidth, this.levelScale, 0, 0, this.waves);
        float levelSizeX = mapWidth * distanceBetweenVertices;
        float levelSizeZ = mapDepth * distanceBetweenVertices;

        for (int zIndex = 0; zIndex < mapDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < mapWidth; xIndex++)
            {
                // Convert from level coordinate system to tile coordinate system and retrieve the corresponding tile data
                TileCoordinate tileCoordinate = levelData.ConvertToTileCoordinate(zIndex, xIndex);
                TileData tileData = levelData.tilesData[tileCoordinate.tileZIndex, tileCoordinate.tileXIndex];
                int tileWidth = tileData.heightMap.GetLength(1);

                // Calculate the mesh vertex index
                Vector3[] meshVertices = tileData.mesh.vertices;
                int vertexIndex = tileCoordinate.coordinateZIndex * tileWidth + tileCoordinate.coordinateXIndex;

                // Get the terrain type of this coordinate
                TerrainType terrainType = tileData.chosenHeightTerrainTypes[tileCoordinate.coordinateZIndex, tileCoordinate.coordinateXIndex];

                // Get the biome of this coordinate
                Biome biome = tileData.chosenBiome[tileCoordinate.coordinateZIndex, tileCoordinate.coordinateXIndex];

                // Check if it is a water terrain. Trees cannot be placed in water
                if (terrainType.name != "water")
                {
                    float treeValue = treeMap[zIndex, xIndex];
                    int terrainTypeIndex = terrainType.index;

                    //Compares the current tree noise value to the neighbor ones
                    int neighborZBegin = (int)Mathf.Max(0, zIndex - this.neighborRadius[biome.index]);
                    int neighborZEnd = (int)Mathf.Min(mapDepth - 1, zIndex + this.neighborRadius[biome.index]);
                    int neighborXBegin = (int)Mathf.Max(0, xIndex - this.neighborRadius[biome.index]);
                    int neighborXEnd = (int)Mathf.Min(mapWidth - 1, xIndex + this.neighborRadius[biome.index]);
                    float maxValue = 0f;

                    for (int neighborZ = neighborZBegin; neighborZ <= neighborZEnd; neighborZ++)
                    {
                        for (int neighborX = neighborXBegin; neighborX <= neighborXEnd; neighborX++)
                        {
                            float neighborValue = treeMap[neighborZ, neighborX];
                            // saves the maximum tree noise value in the radius
                            if (neighborValue >= maxValue)
                            {
                                maxValue = neighborValue;
                            }
                        }
                    }

                    // If the current tree noise value is the maximum one, place a tree in this location
                    if(treeValue == maxValue)
                    {
                        Vector3 treePosition = new Vector3(xIndex * distanceBetweenVertices, meshVertices[vertexIndex].y - 0.04f, zIndex * distanceBetweenVertices);
                        GameObject tree = Instantiate(this.treePrefab[biome.index], treePosition, Quaternion.identity) as GameObject;
                        tree.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);       // Allows us to change the size of the tree assets
                    }
                }
            }
        }
    }

}
