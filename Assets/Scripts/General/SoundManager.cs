using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public Sound[] soundList;

    private Dictionary<string, AudioClip> audioClips;

    void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Error : Only one Sound manager should exists");
        }

        audioClips = new Dictionary<string, AudioClip>();

        foreach (Sound sound in soundList)
        {
            audioClips.Add(sound.name, sound.clip);
        }
    }

    public AudioClip GetClip(string name)
    {
        return audioClips.GetValueOrDefault(name);
    }

    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }
}
