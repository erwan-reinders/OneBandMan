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
        string text = Util.ReadFile(Util.GetDataPath() + "/userSettings.json");
        if (text.Equals(""))
        {
            userSettings = new UserSettings();
        }
        else
        {
            userSettings = UserSettings.LoadFromJSON(text);
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
