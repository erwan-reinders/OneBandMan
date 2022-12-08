using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public GameObject startUI;
    public GameObject endUI;

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
        instance.endUI.SetActive(false);
        instance.startUI.SetActive(false);
        Conductor.Instance.Play();
    }

    public static void EndGame(bool stopSong)
    {
        instance.endUI.SetActive(true);
        Conductor.Instance.Stop(stopSong);
    }

    public static void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void Quit()
    {
        Application.Quit();
    }
}
