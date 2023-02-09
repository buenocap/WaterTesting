using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemScreen : MonoBehaviour
{
    [SerializeField] private GameObject _fullInventoryCanvas;
    [SerializeField] private GameObject _playScreenCanvas;

    public FixedButton itemButton;
    public TextMeshProUGUI itemButtonText;

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("tab") || itemButton.Clicked) && !PauseScreen.isPaused)
        {


            if (Time.timeScale == 1)
            {
                Debug.Log("Item Screen: true");
                _fullInventoryCanvas.SetActive(true);
                _playScreenCanvas.SetActive(false); 
                Time.timeScale = 0;
                itemButtonText.text = "Close List";

                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Debug.Log("Item Screen: false");
                _fullInventoryCanvas.SetActive(false);
                _playScreenCanvas.SetActive(true);
                Time.timeScale = 1;
                itemButtonText.text = "Open List";

                //check whether device is desktop before locking cursor
                if (SystemInfo.deviceType == DeviceType.Desktop)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
            Debug.Log(Time.timeScale);
            itemButton.Clicked = false;
        }
    }
}
