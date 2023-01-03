using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public AudioSource[] musicSources;
    public AudioSource[] soundSources;

    void Start()
    {
        UpdateVolume();
    }

    public void UpdateVolume()
    {
        UserSettings userSettings;
        string fileName = "userSettings";
        TextAsset file = Resources.Load<TextAsset>(fileName);
        if (file == null)
        {
            userSettings = new UserSettings();
        }
        else
        {
            userSettings = UserSettings.LoadFromJSON(file.text);
            Resources.UnloadAsset(file);
        }

        float musicVolume = userSettings.globalVolume * userSettings.musicVolume;
        float soundVolume = userSettings.globalVolume * userSettings.soundVolume;

        foreach (AudioSource source in musicSources)
        {
            source.volume = musicVolume;
        }
        foreach (AudioSource source in soundSources)
        {
            source.volume = soundVolume;
        }
    }
}
