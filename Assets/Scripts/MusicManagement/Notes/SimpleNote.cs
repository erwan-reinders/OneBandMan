using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNote : Note
{
    public double Time { get; }

    private GameObject note;

    public SimpleNote(double time)
    {
        Time = time;
    }

    public void OnPlayPress(double playTime)
    {
        //TODO Evaluate timing (timing evaluator)
    }
    public void OnPlayRelease(double playTime)
    {
        //Nothing
    }

    public void Start()
    {
        //Todo get the note from Pool
        note = null;
    }
    public void Update()
    {
        //Todo move the note
    }
}
