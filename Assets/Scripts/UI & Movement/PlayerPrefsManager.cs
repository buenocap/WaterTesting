/*Christian Cerezo*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerPrefsManager
{

    /* NOTE: 
     * player prefs should only be used to save/load user settings
     * as they can easily be manipulated by the user */
    const string musicVolumeSaveKey = "Music Volume";
    public static float MusicVolume;

    const string soundVolumeSaveKey = "SFX Volume";
    public static float SoundVolume;

    const string camSensitivityXSaveKey = "Camera X";
    public static float camSensitivityX;

    const string camSensitivityYSaveKey = "Camera Y";
    public static float camSensitivityY;



    public static float GetMusicVolume()
    {
        MusicVolume  = PlayerPrefs.GetFloat(musicVolumeSaveKey);
        return MusicVolume;
    }

    public static void SetMusicVolume(float value)
    {
        if (value >= 0f && value <= 1f)
        {
            PlayerPrefs.SetFloat(musicVolumeSaveKey, value);
        }
        else
        {
            Debug.LogException(new System.Exception("Music volume slider out of range. Set slider between 0 and 1 and Not whole numbers."));
        }
    }

    public static float GetSoundVolume()
    {
        SoundVolume = PlayerPrefs.GetFloat(soundVolumeSaveKey);
        return SoundVolume;
    }

    public static void SetSoundVolume(float value)
    {
        if (value >= 0f && value <= 1f)
        {
            PlayerPrefs.SetFloat(soundVolumeSaveKey, value);
        }
        else
        {
            Debug.LogException(new System.Exception("Sound volume slider out of range. Set slider between 0 and 1 and Not whole numbers."));
        }
    }

    public static float GetCamSensitivityX()
    {
        camSensitivityX = PlayerPrefs.GetFloat(camSensitivityXSaveKey);
        return camSensitivityX;
    }

    public static void SetCamSensitivityX(float value)
    {
        if (value >= 0f && value <= 1f)
        {
            PlayerPrefs.SetFloat(camSensitivityXSaveKey, value);
        }
        else
        {
            Debug.LogException(new System.Exception("Camera X Axis Sensitivity slider out of range. Set slider between 0 and 1 and Not whole numbers."));
        }
    }

    public static float GetCamSensitivityY()
    {
        camSensitivityY = PlayerPrefs.GetFloat(camSensitivityYSaveKey);
        return camSensitivityY;
    }

    public static void SetCamSensitivityY(float value)
    {
        if (value >= 0f && value <= 1f)
        {
            PlayerPrefs.SetFloat(camSensitivityYSaveKey, value);
        }
        else
        {
            Debug.LogException(new System.Exception("Camera X Axis Sensitivity slider out of range. Set slider between 0 and 1 and Not whole numbers."));
        }
    }


}