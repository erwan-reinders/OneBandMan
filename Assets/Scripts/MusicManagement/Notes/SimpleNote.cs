using UnityEngine;

public class SimpleNote : Note
{
    private double time;
    public double Time { get => time; }
    public double Beat { get; }
    private bool isPlaying;
    public bool IsPlaying { get => isPlaying; }

    private SongChanelManager songChanelManager;
    private GameObject note;

    public SimpleNote(double beat, SongChanelManager songChanelManager)
    {
        Beat = beat;
        this.songChanelManager = songChanelManager;
    }

    public bool OnPlayPress()
    {
        // Evaluate timing
        double timing = Conductor.Instance.songPosition - time;
        int timingWindow = songChanelManager.timingEvaluator.EvaluateTiming(timing);

        if (timingWindow >= 0)
        {
            Debug.Log(timing + " : " + timingWindow);
            OnAction(timingWindow);
            return true;
        }
        return false;
    }
    public bool OnPlayRelease()
    {
        //Nothing
        return false;
    }

    public void Start()
    {
        time = Conductor.Instance.BeatToTime(Beat);
        note = songChanelManager.pool.SpawnNewNote();

        isPlaying = true;
    }
    public void Update()
    {
        float timingLate = (float)songChanelManager.timingEvaluator.GetLatestInput();

        float startBeat = (float)(Beat - songChanelManager.beatInAdvance);
        float currentBeat = Conductor.Instance.songPositionInBeatsUI;
        float interpol = (currentBeat - startBeat) / ((float)Beat - startBeat);

        songChanelManager.pool.UpdateNote(note, interpol);

        if (Conductor.Instance.songPosition > time + timingLate)
        {
            OnAction(-1);
        }
    }

    private void OnAction(int timingWindow)
    {
        //Todo play effects

        songChanelManager.pool.DeleteNote(note);

        isPlaying = false;
    }
}
