/*Christian Cerezo*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    // Sliders
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _xAxisSlider;
    [SerializeField] private Slider _yAxisSlider;

    [SerializeField] private GameObject _optionsCanvas;
    [SerializeField] private GameObject _mainMenuCanvas;

    // Start is called before the first frame update
    void Start()
    {

        _musicSlider.value = PlayerPrefsManager.GetMusicVolume();
        _soundSlider.value = PlayerPrefsManager.GetSoundVolume();
        _xAxisSlider.value = PlayerPrefsManager.GetCamSensitivityX();
        _yAxisSlider.value = PlayerPrefsManager.GetCamSensitivityY();

        //values are updated in PlayerPrefs as sliders are manipulated
        _musicSlider.onValueChanged.AddListener(delegate { OnChangeMusicVolume(_musicSlider.value); });
        _soundSlider.onValueChanged.AddListener(delegate { OnChangeSoundVolume(_soundSlider.value); });
        _xAxisSlider.onValueChanged.AddListener(delegate { OnChangeXAxisSensitivity(_xAxisSlider.value); });
        _yAxisSlider.onValueChanged.AddListener(delegate { OnChangeYAxisSensitivity(_yAxisSlider.value); });

    }


    void OnChangeMusicVolume(float value)
    {
        PlayerPrefsManager.SetMusicVolume(_musicSlider.value);
        Debug.Log("music volume changed");
    }

    void OnChangeSoundVolume(float value)
    {
        PlayerPrefsManager.SetSoundVolume(_soundSlider.value);
        Debug.Log("sound volume changed");
    }

    void OnChangeXAxisSensitivity(float value)
    {
        PlayerPrefsManager.SetCamSensitivityX(_xAxisSlider.value);
        Debug.Log("X Axis Sensitivity changed");
    }

    void OnChangeYAxisSensitivity(float value)
    {
        PlayerPrefsManager.SetCamSensitivityY(_yAxisSlider.value);
        Debug.Log("Y Axis Sensitivity changed");
    }

    public void SetDefaultOptions()
    {
        PlayerPrefsManager.SetMusicVolume(1.0f);
        PlayerPrefsManager.SetSoundVolume(0.8f);
        PlayerPrefsManager.SetCamSensitivityX(0.5f);
        PlayerPrefsManager.SetCamSensitivityY(0.5f);

        _musicSlider.value = PlayerPrefsManager.GetMusicVolume();
        _soundSlider.value = PlayerPrefsManager.GetSoundVolume();
        _xAxisSlider.value = PlayerPrefsManager.GetCamSensitivityX();
        _yAxisSlider.value = PlayerPrefsManager.GetCamSensitivityY();
    }

    public void CloseOptions()
    {
        _optionsCanvas.SetActive(false);
        _mainMenuCanvas.SetActive(true);
    }
}