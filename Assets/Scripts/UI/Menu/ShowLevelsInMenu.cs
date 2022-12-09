using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShowLevelsInMenu : MonoBehaviour
{
    public MainMenuManager menuManager;
    public GameObject menuParent;
    public MenuItemManager menuItemPrefab;

    void Start()
    {
        string resourcesPath = Application.dataPath + "/Resources";

        DirectoryInfo resourceInfo = new DirectoryInfo(resourcesPath);
        if (resourceInfo.Exists)
        {
            foreach (DirectoryInfo info in resourceInfo.EnumerateDirectories())
            {
                if (info.Exists)
                {
                    AddInfoToMenu(info);
                }
            }
        }
        else
        {
            Debug.LogError("Error : Beatmaps were not found at " + resourcesPath);
        }
    }

    public void AddInfoToMenu(DirectoryInfo info)
    {
        MenuItemManager item = Instantiate(menuItemPrefab, menuParent.transform);
        item.levelName.text = info.Name;
        item.menuManager = menuManager;
    }
}
