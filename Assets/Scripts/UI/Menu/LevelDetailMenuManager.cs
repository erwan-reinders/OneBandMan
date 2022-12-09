using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelDetailMenuManager : MonoBehaviour
{
    public AudioSource source;
    public TextMeshProUGUI songTitleText;
    public TextMeshProUGUI songBPMText;
    public TextMeshProUGUI songDescriptionText;
    public TextMeshProUGUI buttonMuteText;
    public Button detailGoButton;

    public void LoadSongInfo(string songName)
    {
        string path = songName + "/songParameters";
        TextAsset file = Resources.Load<TextAsset>(path);
        string description = "";
        string bpm = "";
        if (file == null)
        {
            Debug.LogWarning("Error : song parameters does not exist (path=" + path + ")");
        }
        else
        {
            SongParameters songParameters = SongParameters.LoadFromJSON(file.text);
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

        detailGoButton.interactable = true;

        source.clip = SongManager.GetSong(songName);
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
}
