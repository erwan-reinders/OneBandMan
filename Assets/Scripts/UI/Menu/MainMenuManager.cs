using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public LevelDetailMenuManager detailManager;
    public LevelScoreMenuManager scoreManager;

    private string currentLevelName = "";

    public void LoadSongInfo(string songName)
    {
        if (currentLevelName == songName)
        {
            return;
        }

        currentLevelName = songName;

        detailManager.LoadSongInfo(songName);
        scoreManager.LoadSongInfo(songName);
    }

    public void LoadLevel()
    {
        if (currentLevelName.Length > 0)
        {
            SceneManager.LoadScene(currentLevelName);
        }
    }

    public void ReloadScore()
    {
        scoreManager.LoadSongInfo(currentLevelName);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
