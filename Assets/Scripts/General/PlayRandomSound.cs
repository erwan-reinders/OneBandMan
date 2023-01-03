using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip[] clips;

    public void Play()
    {
        audioSource.clip = clips[Random.Range(0, clips.Length)];
        audioSource.Play();
    }
}
