/*Pedro Bueno
 File Description: This file contains code that helps in generating how a tile is texture is created and displayed on to the game. It's also what will allow us to view the different types of maps implemented. These terrains
get combined together to idenfity a biome.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that stores the data of all the merged tiles
public class LevelData
{
	private int tileDepthInVertices, tileWidthInVertices;

	public TileData[,] tilesData;

	public LevelData(int tileDepthInVertices, int tileWidthInVertices, int mapDepthInTiles, int mapWidthInTiles)
	{
		// Build the tiles data matrix based on the level depth and width
		tilesData = new TileData[tileDepthInVertices * mapDepthInTiles, tileWidthInVertices * mapWidthInTiles];
		this.tileDepthInVertices = tileDepthInVertices;
		this.tileWidthInVertices= tileWidthInVertices;
	}

	public TileCoordinate ConvertToTileCoordinate(int zIndex, int xIndex)
	{
		// The tile index is calculated by dividing the index by the number of tiles in that axis
		int tileZIndex = (int)Mathf.Floor ((float)zIndex / (float)this.tileDepthInVertices);
		int tileXIndex = (int)Mathf.Floor ((float)xIndex / (float)this.tileWidthInVertices);

		/*The coordinate index is calculated by getting the remainder of the division above
		 We also need to translate the origin to the bottom left corner*/
		int coordinateZIndex = this.tileDepthInVertices - (zIndex % this.tileDepthInVertices) - 1;
		int coordinateXIndex = this.tileWidthInVertices - (xIndex % this.tileDepthInVertices) - 1; // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! Potential error
		TileCoordinate tileCoordinate = new TileCoordinate(tileZIndex, tileXIndex, coordinateZIndex, coordinateXIndex);
		return tileCoordinate;
	}

	public void AddTileData(TileData tileData, int tileZIndex, int tileXIndex)
	{
		tilesData[tileZIndex, tileXIndex] = tileData;
	}
}

// Class that represents a coordinate in the Tile Coordinate system, this is needed to keep track of tile placement as we now have merged all tiles together
public class TileCoordinate
{
	public int tileZIndex;
	public int tileXIndex;
	public int coordinateZIndex;
	public int coordinateXIndex;

	public TileCoordinate(int tileZIndex, int tileXIndex, int coordinateZIndex, int coordinateXIndex)
	{
		this.tileZIndex = tileZIndex;
		this.tileXIndex = tileXIndex;
		this.coordinateZIndex = coordinateZIndex;
		this.coordinateXIndex = coordinateXIndex;
	}
}

//Main Code
public class LevelGeneration : MonoBehaviour {

	//Field that allows us to change the size of the map based on the difficulty selected in main menu *UPDATED: 11/29/22*
	[HideInInspector]
	private int mapWidthInTiles, mapDepthInTiles;

	//Field that allows us to select the template we want to use to create the level
	[SerializeField]
	private GameObject tilePrefab;

	//Field that allows us select the center z coordinate and the distance from this coordinate
	[SerializeField]
	private float centerVertexZ, maxDistanceZ;

	//Field that allows us to implement tree generation algorithm
	[SerializeField]
	private TreeGeneration treeGeneration;

	//Field that allows us to implement item generation algorithm
	[SerializeField]
	private ItemGeneration itemGeneration;

	[SerializeField]
	private PortalGeneration portalGeneration;

	[SerializeField]
	private MusicGeneration musicGeneration;


	// Runs when game is started
	void Start() {
		GenerateMap ();
	}

	void GenerateMap() {
		//NEWLY IMPLEMENTED
		string msg = string.Format("Current Level Difficulty: {0}", DifficultyMenu.levelDifficulty); //Debug to make sure the level difficulty has transferred
		Debug.Log(msg); //Send message to console


		/* NEWLY IMPLEMENTED
		Difficulty setting selected from the Main Menu will determine the map size via a switch function */
		switch(DifficultyMenu.levelDifficulty)
		{
			case 0:
				this.mapDepthInTiles = 5;
				this.mapWidthInTiles = 5;
				Debug.Log("Easy Mode: Map Size 5x5");
				break;
			case 1:
				this.mapDepthInTiles = 15;
				this.mapWidthInTiles = 15;
				Debug.Log("Normal Mode: Map Size 15x15");
				break;
			case 2:
				this.mapDepthInTiles = 30;
				this.mapWidthInTiles = 30;
				Debug.Log("Hard Mode: Map Size 30x30");
				break;
			default:
				this.mapDepthInTiles = 20;
				this.mapWidthInTiles = 20;
				Debug.Log("Invaild Input Reverting to Default: Map Size 20x20");
				break;
		}

		// Get the tile dimensions from the tile Prefab
		Vector3 tileSize = tilePrefab.GetComponent<MeshRenderer> ().bounds.size;
		int tileWidth = (int)tileSize.x;
		int tileDepth = (int)tileSize.z;

		// Calculate the number of vertices of the tile in each axis using the mesh
		Vector3[] tileMeshVertices = tilePrefab.GetComponent<MeshFilter>().sharedMesh.vertices;
		int tileDepthInVertices = (int)Mathf.Sqrt(tileMeshVertices.Length);
		int tileWidthInVertices = tileDepthInVertices;

		float distanceBetweenVertices = (float)tileDepth / (float)tileDepthInVertices;

		// Build an empty level data object to be filled with the tiles generated
		LevelData levelData = new LevelData(tileDepthInVertices, tileWidthInVertices, this.mapDepthInTiles, this.mapWidthInTiles);

		// For each Tile, instantiate a Tile in the correct position
		for (int xTileIndex = 0; xTileIndex < mapWidthInTiles; xTileIndex++) {
			for (int zTileIndex = 0; zTileIndex < mapDepthInTiles; zTileIndex++) {
				// Calculate the tile position based on the X and Z indices
				Vector3 tilePosition = new Vector3(this.gameObject.transform.position.x + xTileIndex * tileWidth, 
					this.gameObject.transform.position.y, 
					this.gameObject.transform.position.z + zTileIndex * tileDepth);
				// Instantiate a new Tile
				GameObject tile = Instantiate (tilePrefab, tilePosition, Quaternion.identity) as GameObject;
				// Generate the Tile texture
				TileData tileData = tile.GetComponent<TileGeneration> ().GenerateTile(centerVertexZ, maxDistanceZ);
				levelData.AddTileData(tileData, zTileIndex, xTileIndex);
			}
		}

		// Level will either be generated randomly or using SaveData based on MainMenu.isContinue value - Christian

		if (MainMenu.isContinue)
		{
			// Generate trees for the level
			treeGeneration.GenerateTrees(this.mapDepthInTiles * tileDepthInVertices, this.mapWidthInTiles * tileWidthInVertices, distanceBetweenVertices, levelData);

			// generate items for the level
			itemGeneration.GenerateItemsFromSave();

			// Generate portals for the level
			portalGeneration.GeneratePortalsFromSave(distanceBetweenVertices, levelData);

			// Generate music players for the level
			musicGeneration.GenerateMusic(this.mapDepthInTiles * tileDepthInVertices, this.mapWidthInTiles * tileWidthInVertices, distanceBetweenVertices, levelData);
		}

		else
		{
			// Generate trees for the level
			treeGeneration.GenerateTrees(this.mapDepthInTiles * tileDepthInVertices, this.mapWidthInTiles * tileWidthInVertices, distanceBetweenVertices, levelData);

			// generate items for the level
			itemGeneration.GenerateItems(this.mapDepthInTiles * tileDepthInVertices, this.mapWidthInTiles * tileWidthInVertices, distanceBetweenVertices, levelData);

			// Generate portals for the level
			portalGeneration.GeneratePortals(this.mapDepthInTiles * tileDepthInVertices, this.mapWidthInTiles * tileWidthInVertices, distanceBetweenVertices, levelData);

			// Generate music players for the level
			musicGeneration.GenerateMusic(this.mapDepthInTiles * tileDepthInVertices, this.mapWidthInTiles * tileWidthInVertices, distanceBetweenVertices, levelData);
		}
	}
}
