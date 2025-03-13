using UnityEngine;

public class soundval : MonoBehaviour
{
    public static float musicVolume
    {
        get { return PlayerPrefs.GetFloat("MusicVolume"); }
        set
        {
            PlayerPrefs.SetFloat("MusicVolume", value);
            PlayerPrefs.Save();
        }
    }

    public static float sfxVolume
    {
        get { return PlayerPrefs.GetFloat("SFXVolume"); }
        set
        {
            PlayerPrefs.SetFloat("SFXVolume", value);
            PlayerPrefs.Save();
        }
    }
}