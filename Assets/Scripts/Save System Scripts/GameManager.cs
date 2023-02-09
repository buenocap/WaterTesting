using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;


    [SerializeField] GameObject Player;

    public float _timeGoalMax;

    private float time;
    public int _timeGoalIntervals;
    
    private float interpolationPeriod = 15;
    [SerializeField] private GameObject endScreen;
    public static bool isEndGame = false;

    public static SaveObject SaveData; //save data: only one file is being written to, therefore only one saveObject is required - Christian
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        SaveData = SaveManager.Load();
        SaveData.NewGame = false;
        SaveManager.Save(SaveData);
        Player.transform.position = new Vector3(SaveData.Player.xPos, SaveData.Player.yPos, SaveData.Player.zPos);

        isEndGame = false;
    }

    private void Update()
    {
        //Continously Save Player Location (in temporary Save object) - Christian
        SaveData.Player.xPos = Player.transform.position.x;
        SaveData.Player.yPos = Player.transform.position.y + 0.1f;
        SaveData.Player.zPos = Player.transform.position.z;

        //keep track of time loops for saving
        time += Time.deltaTime;
        if (time >= interpolationPeriod)
        {
            time -= interpolationPeriod;

            //Game data is saved every 15 seconds
            SaveManager.Save(SaveData);
            Debug.Log("15/15 seconds: Game has been saved");
        }

        if (isEndGame)
        {
            EndGame();
        }

    }

    public void EndGame()
    {
        //define what should happen when all items have been found - Christian
        endScreen.SetActive(true);
    }

}
