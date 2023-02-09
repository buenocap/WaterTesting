using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGeneration : MonoBehaviour
{
    [SerializeField]
    private GameObject[] musicPlayerPrefab = new GameObject[4];

    // places music players at the center of four quartiles of the map
    public void GenerateMusic(int mapDepth, int mapWidth, float distanceBetweenVertices, LevelData levelData)
    {
        /*
        // Convert from level coordinate system to tile coordinate system and retrieve the corresponding tile data
        TileCoordinate tileCoordinate = levelData.ConvertToTileCoordinate(a, b);
        TileData tileData = levelData.tilesData[tileCoordinate.tileZIndex, tileCoordinate.tileXIndex];
        int tileWidth = tileData.heightMap.GetLength(1);

        // Calculate the mesh vertex index
        Vector3[] meshVertices = tileData.mesh.vertices;
        int vertexIndex = tileCoordinate.coordinateZIndex * tileWidth + tileCoordinate.coordinateXIndex;
        */

        // set max distance values of players
        musicPlayerPrefab[0].GetComponent<AudioSource>().maxDistance = mapDepth * .35f;
        musicPlayerPrefab[1].GetComponent<AudioSource>().maxDistance = mapDepth * .35f;
        musicPlayerPrefab[2].GetComponent<AudioSource>().maxDistance = mapDepth * .35f;
        musicPlayerPrefab[3].GetComponent<AudioSource>().maxDistance = mapDepth * .35f;


        // place players
        Vector3 playerPosition1 = new Vector3(mapDepth * .25f, 0, mapWidth * .25f);
        GameObject player1 = Instantiate(this.musicPlayerPrefab[0], playerPosition1, Quaternion.identity) as GameObject;
        Vector3 playerPosition2 = new Vector3(mapDepth * .25f, 0, mapWidth * .75f);
        GameObject player2 = Instantiate(this.musicPlayerPrefab[1], playerPosition2, Quaternion.identity) as GameObject;
        Vector3 playerPosition3 = new Vector3(mapDepth * .75f, 0, mapWidth * .25f);
        GameObject player3 = Instantiate(this.musicPlayerPrefab[2], playerPosition3, Quaternion.identity) as GameObject;
        Vector3 playerPosition4 = new Vector3(mapDepth * .75f, 0, mapWidth * .75f);
        GameObject player4 = Instantiate(this.musicPlayerPrefab[3], playerPosition4, Quaternion.identity) as GameObject;

    }
    

}
