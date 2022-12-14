using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    public static string FILE_NAME = "userSettings.json";

    public MainMenuManager mainMenuManager;

    public UserSettings userSettings;

    public AudioSource musicSource;
    public PlayRandomSound soundSource;

    public Toggle canPauseToggle;
    public Slider globalVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;

    public TextMeshProUGUI instrumentName;
    public TextMeshProUGUI instrumentLatency;

    private int currentInstrument;
    private List<string> instruments;

    private void Start()
    {
        string text = Util.ReadFile(Util.GetDataPath() + "/" + FILE_NAME);
        if (text.Equals(""))
        {
            userSettings = new UserSettings();
        }
        else
        {
            userSettings = UserSettings.LoadFromJSON(text);
        }

        InitValues();

        InvokeRepeating("SaveSettings", 1, 1);
    }

    private void InitValues()
    {
        canPauseToggle.isOn = userSettings.canPause;
        globalVolumeSlider.value = userSettings.globalVolume;
        musicVolumeSlider.value = userSettings.musicVolume;
        soundVolumeSlider.value = userSettings.soundVolume;

        instruments = new List<string>();
        foreach (KeyValuePair<string, UserSettings.InstrumentSetting> instrument in userSettings.instrumentSettings)
        {
            instruments.Add(instrument.Key);
        }
        currentInstrument = 0;
        UpdateInstrument();
    }

    private void SaveSettings()
    {
        Util.WriteFile(Util.GetDataPath() + "/" + FILE_NAME, UserSettings.SaveToJSON(userSettings));
    }

    public void ToggleCanPause()
    {
        userSettings.canPause = canPauseToggle.isOn;
    }

    private void UpdateVolume()
    {
        musicSource.volume = userSettings.globalVolume * userSettings.musicVolume;
        soundSource.audioSource.volume = userSettings.globalVolume * userSettings.soundVolume;
    }
    
    public void ChangeGlobalVolume()
    {
        userSettings.globalVolume = globalVolumeSlider.value;
        UpdateVolume();
    }

    public void ChangeMusicVolume()
    {
        userSettings.musicVolume = musicVolumeSlider.value;
        UpdateVolume();
    }

    public void ChangeSoundVolume()
    {
        userSettings.soundVolume = soundVolumeSlider.value;
        UpdateVolume();
        if (!soundSource.audioSource.isPlaying)
        {
            soundSource.Play();
        }
    }

    private void UpdateInstrument()
    {
        instrumentName.text = instruments[currentInstrument];
        UserSettings.InstrumentSetting curentInstrument = userSettings.instrumentSettings[instruments[currentInstrument]];
        instrumentLatency.text = "latency : " + curentInstrument.latency.ToString() + "ms";
    }

    public void NextInstrument()
    {
        currentInstrument = (currentInstrument + 1) % instruments.Count;
        UpdateInstrument();
    }
    public void PreviousInstrument()
    {
        currentInstrument--;
        if (currentInstrument < 0)
        {
            currentInstrument = instruments.Count-1;
        }
        UpdateInstrument();
    }

    public void AddLatency()
    {
        userSettings.instrumentSettings[instruments[currentInstrument]].latency++;
        UpdateInstrument();
    }

    public void RemoveLatency()
    {
        userSettings.instrumentSettings[instruments[currentInstrument]].latency--;
        UpdateInstrument();
    }

    public void ResetSettings()
    {
        userSettings = new UserSettings();
        InitValues();
    }

    public void DeleteAllData()
    {
        string resourcesPath = Util.GetDataPath();

        DirectoryInfo resourceInfo = new DirectoryInfo(resourcesPath);
        if (resourceInfo.Exists)
        {
            foreach (DirectoryInfo info in resourceInfo.EnumerateDirectories())
            {
                if (info.Exists)
                {
                    DeleteSongData(info);
                }
            }
        }
    }

    private void DeleteSongData(DirectoryInfo info)
    {
        string path = info.FullName+"/scores.json";
        if (File.Exists(path))
        {
            File.Delete(path);
            File.Delete(path + ".meta");
        }

        mainMenuManager.ReloadScore();
    }
}
