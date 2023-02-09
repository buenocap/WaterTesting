using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public int prefabID;
    public string objectID;
    public float xPos;
    public float yPos;
    public float zPos;

    public ItemData(int ID, string name, float xPos, float yPos, float zPos)
    {
        this.prefabID = ID;
        this.objectID = name;
        this.xPos = xPos;
        this.yPos = yPos;
        this.zPos = zPos;
    }
}
