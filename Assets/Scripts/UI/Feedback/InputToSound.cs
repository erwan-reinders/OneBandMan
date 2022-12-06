using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputToSound : MonoBehaviour
{
    public string chanel;
    public AudioSource audioSource;
    public bool disableOnSongStart;

    void Update()
    {
        if (InputSystem.inputs[chanel].Pressed)
        {
            audioSource.Play();
        }
        if (disableOnSongStart && Conductor.Instance.musicPlaying)
        {
            enabled = false;
        }
    }
}
