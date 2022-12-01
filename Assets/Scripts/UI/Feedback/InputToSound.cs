using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputToSound : MonoBehaviour
{
    public string chanel;
    public AudioSource audioSource;

    void Update()
    {
        if (InputSystem.inputs[chanel].Pressed)
        {
            audioSource.Play();
        }
    }
}
