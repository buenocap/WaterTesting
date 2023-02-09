/*Christian Cerezo*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// In progress...
/// </summary>
public class TimeManager : MonoBehaviour
{

    public static TimeManager instance = null;

    private static float InitialTime; 

    public static float TimeSinceLoad; //Time elapsed since Level (scene) has loaded

    public static float PreviousElapsedTime; //Time elapsed in previous game session

    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        InitialTime = Time.time;
        PreviousElapsedTime = GameManager.SaveData.TimeElapsed;
    }

    private void Update()
    {
        //Continously save elapsed time in temporary location
        TimeSinceLoad = Time.time - InitialTime;
        GameManager.SaveData.TimeElapsed = TimeSinceLoad + PreviousElapsedTime;


    }

}

