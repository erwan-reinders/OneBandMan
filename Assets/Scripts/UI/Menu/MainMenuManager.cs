using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public LevelDetailMenuManager detailManager;

    private string currentLevelName = "";

    public void LoadSongInfo(string songName)
    {
        if (currentLevelName == songName)
        {
            return;
        }

        currentLevelName = songName;

        detailManager.LoadSongInfo(songName);
    }

    public void LoadLevel()
    {
        if (currentLevelName.Length > 0)
        {
            SceneManager.LoadScene(currentLevelName);
        }
    }
}
