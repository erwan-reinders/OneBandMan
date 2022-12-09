using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuItemManager : MonoBehaviour
{
    public TextMeshProUGUI levelName;

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelName.text);
    }
}
