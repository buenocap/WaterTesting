using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGeneration : MonoBehaviour
{
    [HideInInspector]
    private int numberOfItems;

    [SerializeField]
    private GameObject[] itemPrefab;

    int objectID = 0; // used to uniquely identify each scavenger hunt item - Christian

    public void GenerateItems(int mapDepth, int mapWidth, float distanceBetweenVertices, LevelData levelData)
    {
        // **UPDATE** Changes number of items based on the level difficulty selected - Pedro
        switch(DifficultyMenu.levelDifficulty)
        {
            case 0:
                numberOfItems = 10;
                break;
            case 1:
                numberOfItems = 20;
                break;
            case 2:
                numberOfItems = 30; 
                break;
            default:
                numberOfItems = 25; 
                break;
        }

        var prevCoords = new List<(int, int)> {};

        for (int i = 0; i < numberOfItems; i++) 
        {
            int a, b;

            // generate random numbers that have not been previously used
            // TODO - Make sure items do not spawn in positions where trees are located
            do {
                a = Random.Range(1, mapDepth);
                b = Random.Range(1, mapWidth);
            } while (prevCoords.Contains((a, b)));

            // Convert from level coordinate system to tile coordinate system and retrieve the corresponding tile data
            TileCoordinate tileCoordinate = levelData.ConvertToTileCoordinate(a, b);
            TileData tileData = levelData.tilesData[tileCoordinate.tileZIndex, tileCoordinate.tileXIndex];
            int tileWidth = tileData.heightMap.GetLength(1);

            // Calculate the mesh vertex index
            Vector3[] meshVertices = tileData.mesh.vertices;
            int vertexIndex = tileCoordinate.coordinateZIndex * tileWidth + tileCoordinate.coordinateXIndex;

            // add index to previous coordinates used
            prevCoords.Add((a, b));

            // place randomly selected item at index
            // Altered so that random integer corresponding to prefab in array can be saved - Christian
            Vector3 itemPosition = new Vector3(b * distanceBetweenVertices, meshVertices[vertexIndex].y + 2f, a * distanceBetweenVertices);
            int randomInt = Random.Range(0, itemPrefab.Length);
            GameObject item = Instantiate(this.itemPrefab[randomInt], itemPosition, Quaternion.identity) as GameObject;

            item.name = item.name + objectID; // Set unique name to be used as the GameObject's ID - Christian

            // Save data is retrieved before overwriting with new scavenger hunt item data - Christian
            ItemData ItemData = new ItemData(randomInt, item.name, itemPosition.x, itemPosition.y, itemPosition.z);
            GameManager.SaveData.Items.Add(ItemData);


            objectID++; //Increment Item ID to uniquely identify each GameObject - Christian

            item.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);      //Allows us to change the size of findable objects - Pedro


        }

        SaveManager.Save(GameManager.SaveData); //item data is saved after loop to reduce number of write cycles - Christian





    }

    public void GenerateItemsFromSave()
    {

        for (int i = 0; i < GameManager.SaveData.Items.Count; i++)
        {
            ItemData ItemData = GameManager.SaveData.Items[i];
            Vector3 itemPosition = new Vector3(ItemData.xPos, ItemData.yPos, ItemData.zPos);
            // instantiate item based on save data
            GameObject item = Instantiate(this.itemPrefab[ItemData.prefabID], itemPosition, Quaternion.identity) as GameObject;

            item.name = ItemData.objectID; // Set unique name to be used as the GameObject's ID - Christian

            item.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);      //Allows us to change the size of findable objects - Pedro

        }

    }
}
