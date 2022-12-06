using UnityEngine;

public class SimpleNote : Note
{
    public int poolID;

    protected double time;
    public double Time { get => time; }
    public double Beat { get; }
    protected bool isPlaying;
    public bool IsPlaying { get => isPlaying; }

    protected SongChanelManager songChanelManager;
    protected GameObject note;

    public SimpleNote(double beat, SongChanelManager songChanelManager, int poolID)
    {
        Beat = beat;
        this.songChanelManager = songChanelManager;
        this.poolID = poolID;
    }
    public SimpleNote(double beat, SongChanelManager songChanelManager) : this(beat, songChanelManager, songChanelManager.defaultPoolId) {  }


    public virtual bool OnPlayPress(InputSystem.Inputs input)
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
    public bool OnPlayRelease(InputSystem.Inputs input)
    {
        //Nothing
        return false;
    }

    public void Start()
    {
        time = Conductor.Instance.BeatToTime(Beat);
        note = NotePoolManager.instance.pools[poolID].SpawnNewNote();

        isPlaying = true;
    }
    public void Update()
    {
        float timingLate = (float)songChanelManager.timingEvaluator.GetLatestInput();

        float startBeat = (float)(Beat - songChanelManager.beatInAdvance);
        float currentBeat = Conductor.Instance.songPositionInBeatsUI;
        float interpol = (currentBeat - startBeat) / ((float)Beat - startBeat);

        songChanelManager.displayer.UpdateNote(note, interpol);

        if (Conductor.Instance.songPosition > time + timingLate)
        {
            OnAction(-1);
        }
    }

    protected virtual void OnAction(int timingWindow)
    {
        //Todo play effects

        NotePoolManager.instance.pools[poolID].DeleteNote(note);

        isPlaying = false;
    }
}
