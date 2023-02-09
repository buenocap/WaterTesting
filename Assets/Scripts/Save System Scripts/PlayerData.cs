using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float xPos;
    public float yPos;
    public float zPos;

    public float xRot;
    public float yRot;
    public float zRot;

    public float walkSpeed;
    public PlayerData (float xPos, float yPos, float zPos, float xRot, float yRot, float zRot, float walkSpeed)
    {
        this.xPos = xPos;
        this.yPos = yPos;
        this.zPos = zPos;
        this.xRot = xRot;
        this.yRot = yRot;
        this.zRot = zRot;
        this.walkSpeed = walkSpeed;
    }
}