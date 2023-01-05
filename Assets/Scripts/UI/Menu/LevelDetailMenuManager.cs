using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelDetailMenuManager : MonoBehaviour
{
    public AudioSource source;
    public TextMeshProUGUI songTitleText;
    public TextMeshProUGUI songBPMText;
    public TextMeshProUGUI songDescriptionText;
    public TextMeshProUGUI buttonMuteText;
    public Button detailGoButton;

    private AudioClip defaultClip;

    void Start()
    {
        defaultClip = source.clip;
    }

    public void LoadSongInfo(string songName)
    {
        string path = Util.GetSongPath(songName) + "/songParameters.json";
        string text = Util.ReadFile(path);
        string description = "";
        string bpm = "";
        if (text.Equals(""))
        {
            Debug.LogWarning("Error : song parameters does not exist (path=" + path + ")");
        }
        else
        {
            SongParameters songParameters = SongParameters.LoadFromJSON(text);

            description = songParameters.description;
            bpm = "BPM : " + songParameters.BPM.ToString();
        }

        if (description.Length == 0)
        {
            description = "No description";
        }


        songTitleText.text = songName;
        songDescriptionText.text = description;
        songBPMText.text = bpm;

        source.clip = Resources.Load<AudioClip>(songName);
        detailGoButton.interactable = true;

        source.Play();
    }

    public void ToggleAudio()
    {
        if (source.isPlaying)
        {
            source.Pause();
            buttonMuteText.text = "Resume";
        }
        else
        {
            source.Play();
            buttonMuteText.text = "Mute";
        }
    }

    public void UnloadAudio()
    {
        if (source.clip != defaultClip)
        {
            source.clip = defaultClip;
            source.Play();
        }
    }
}
