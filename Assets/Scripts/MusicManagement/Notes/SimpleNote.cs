using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNote : Note
{
    private double time;
    public double Time { get => time; }
    public double Beat { get; }
    private bool isPlaying;
    public bool IsPlaying { get => isPlaying; }

    private NotePool pool;
    private GameObject note;

    public SimpleNote(double beat, NotePool pool)
    {
        Beat = beat;
        this.pool = pool;
    }

    public void OnPlayPress()
    {
        // Evaluate timing
        double timing = Conductor.Instance.songPosition - time;
        int timingWindow = SongManager.instance.timingEvaluator.EvaluateTiming(timing);

        if (timingWindow >= 0)
        {
            Debug.Log(timing + " : " + timingWindow);
            OnAction(timingWindow);
        }
    }
    public void OnPlayRelease()
    {
        //Nothing
    }

    public void Start()
    {
        time = Conductor.Instance.BeatToTime(Beat);
        note = pool.SpawnNewNote();

        isPlaying = true;
    }
    public void Update()
    {
        //Todo get real values
        float timingEarly = 1.0f; // difficulty option or nb Beats seen in advance
        float timingLate = 0.5f; // largest Timing window

        float startTime = (float)time - timingEarly;
        float currentTime = Conductor.Instance.songPosition;
        float interpol = (currentTime - startTime) / ((float)time - startTime);

        note.transform.position = pool.spawnPos.position + (pool.timePos.position - pool.spawnPos.position) * interpol;

        if (currentTime > time + timingLate)
        {
            OnAction(-1);
        }
    }

    private void OnAction(int timingWindow)
    {
        //Todo play effects

        pool.DeleteNote(note);

        isPlaying = false;
    }
}
