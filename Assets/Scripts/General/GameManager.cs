using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public GameObject startUI;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Error : there is more than one GameManager !");
        }
    }

    public static void StartGame()
    {
        instance.startUI.SetActive(false);
        Conductor.Instance.Play();
    }
}
