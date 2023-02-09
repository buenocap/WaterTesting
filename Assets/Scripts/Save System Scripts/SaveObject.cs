/*Christian Cerezo*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveObject
{
    // Fields to be saved in JSON format: must be public and able to be serializable
    public int GameDifficulty;
    public float TimeElapsed;
    public bool NewGame;

    public PlayerData Player = new PlayerData(2f, 3f, 2f, 0f, 0f, 0f, 10f);
    public BonusItemData BonusItems;
    public List<PortalData> Portals = new List<PortalData>();
    public List<ItemData> Items = new List<ItemData>();
    public List<float> LevelSeeds = new List<float>();

    public SaveObject()
    {
        this.GameDifficulty = 0;
        this.TimeElapsed = 0;
        this.NewGame = true;

    }

}
