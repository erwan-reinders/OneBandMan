using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuItemManager : MonoBehaviour
{
    public MainMenuManager menuManager;
    public TextMeshProUGUI levelName;

    public void Select()
    {
        menuManager.LoadSongInfo(levelName.text);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelName.text);
    }
}
