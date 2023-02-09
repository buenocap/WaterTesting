/*Christian Cerezo */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject _optionsScreen;
    [SerializeField] private GameObject _difficultyScreen;
    [SerializeField] private GameObject _mainMenuScreen;

    [SerializeField] private string _mainLevel;

    public static bool isContinue; //used during level generation to determine whether to use save data for generation - Christian

    public void OpenOptions()
    {
        _mainMenuScreen.SetActive(false);
        _optionsScreen.SetActive(true);
    }

    public void QuitGame()
    {
        //Won't do anything in editor
        Application.Quit();
        Debug.Log("Quitting");
    }

    public void OpenDifficulty()
    {
        //TODO: DIsplay a warning message if a level is in progress


        _mainMenuScreen.SetActive(false);
        _difficultyScreen.SetActive(true);


    }

    public void ContinueGame()
    {
        Time.timeScale = 1;

        //TODO: check if save data exists before loading scene (whether a game has been started)
        isContinue = true;
        SceneManager.LoadScene(_mainLevel);
    }


}
