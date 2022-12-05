using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePoolManager : MonoBehaviour
{
    public static NotePoolManager instance;

    public NotePool[] pools;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Error : There are more than one NotePoolManager");
        }
    }
}
