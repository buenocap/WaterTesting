/*Christian Cerezo*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseScreen : MonoBehaviour
{
    public static bool isPaused;

    [SerializeField] private GameObject _pauseCanvas;
    [SerializeField] private GameObject _playCanvas;
    [SerializeField] private GameObject _optionsCanvas;
    [SerializeField] private GameObject _itemCanvas;
    public string menuScreen;

    [SerializeField] private TextMeshProUGUI _timeElapsed;
    [SerializeField] private Slider _timeSlider;
    [SerializeField] private TextMeshProUGUI _timeleft;

    [SerializeField] private GameObject timeIntervalPrefab;

    [SerializeField] private GameObject intervalContainer;

    public FixedButton pauseButton;

    public void Start()
    {
        _timeSlider.maxValue = GameManager.instance._timeGoalMax;

        //create time left bar intervals
        for(int i = 0; i < GameManager.instance._timeGoalIntervals - 1; i++)
        {
            GameObject obj = Instantiate(timeIntervalPrefab);
            obj.transform.SetParent(intervalContainer.transform, false);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown("escape") || pauseButton.Clicked)
        {
            if (Time.timeScale == 1)
            {
                _pauseCanvas.SetActive(true);
                _playCanvas.SetActive(false);
                _itemCanvas.SetActive(false);
                Time.timeScale = 0;
                isPaused = true;

                //Save game
                SaveManager.Save(GameManager.SaveData);

                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                _pauseCanvas.SetActive(false);
                _playCanvas.SetActive(true);
                _itemCanvas.SetActive(true);
                Time.timeScale = 1;
                isPaused = false;

                //check whether device is a desktop before locking cursor
                if (SystemInfo.deviceType == DeviceType.Desktop)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
            pauseButton.Clicked = false;
        }
        float timeElapsed = GameManager.SaveData.TimeElapsed;
        float timeLeft = GameManager.instance._timeGoalMax - timeElapsed;

        string seconds = timeElapsed.ToString("0.00");
        _timeElapsed.text = "Time Elapsed: " + seconds + "s";

        if (timeLeft >= 0f)
        {
            _timeSlider.value = timeElapsed;

            _timeleft.text = "Time Left: " + timeLeft.ToString("0.00") + "s";
        }
        else
        {
            _timeSlider.value = GameManager.instance._timeGoalMax;
            _timeleft.text = "Time Left: 0.00s";
        }
    }
    public void ClosePauseMenu()
    {
        _pauseCanvas.SetActive(false);
        _playCanvas.SetActive(true);
        _itemCanvas.SetActive(true);
        Time.timeScale = 1;
        isPaused = false;

        //check whether device is desktop before locking cursor
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void OpenOptions()
    {
        _pauseCanvas.SetActive(false);
        _optionsCanvas.SetActive(true);
    }

    public void CloseOptions()
    {
        _optionsCanvas.SetActive(false);
        _pauseCanvas.SetActive(true);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(menuScreen);
    }
}
