using UnityEngine;

public class LongNote : Note
{
    public int poolID;

    protected double timeStart;
    protected double timeEnd;
    public double Time { get => timeStart; }
    public double Beat { get; }
    protected bool isPlaying;
    public bool IsPlaying { get => isPlaying; }
    public double beatDuration;

    protected SongChanelManager songChanelManager;
    protected GameObject note;

    private bool hitStart;

    public LongNote(double beat, double beatDuration, SongChanelManager songChanelManager)
    {
        Beat = beat;
        this.beatDuration = beatDuration;
        this.songChanelManager = songChanelManager;
        poolID = 3;
    }


    public virtual bool OnPlayPress(InputSystem.Inputs input)
    {
        // Evaluate timing
        double timing = Conductor.Instance.songPosition - timeStart;
        int timingWindow = songChanelManager.timingEvaluator.EvaluateTiming(timing);

        if (timingWindow >= 0)
        {
            hitStart = true;
            OnActionStart(timingWindow, timing);
            return true;
        }
        return false;
    }
    public bool OnPlayRelease(InputSystem.Inputs input)
    {
        if (hitStart)
        {
            // Evaluate timing
            double timing = Conductor.Instance.songPosition - timeEnd;
            int timingWindow = songChanelManager.timingEvaluator.EvaluateTiming(timing);
            OnActionEnd(timingWindow, timing);
            return true;
        }
        return false;
    }

    public void Start()
    {
        timeStart = Conductor.Instance.BeatToTime(Beat);
        timeEnd = Conductor.Instance.BeatToTime(Beat+beatDuration);
        note = NotePoolManager.instance.pools[poolID].SpawnNewNote();

        isPlaying = true;
        hitStart = false;
    }
    public void Update()
    {
        float timingLate = (float)songChanelManager.timingEvaluator.GetLatestInput();

        float startBeatStart = (float)(Beat - songChanelManager.beatInAdvance);
        float currentBeatStart = Conductor.Instance.songPositionInBeatsUI;
        float interpolStart = (currentBeatStart - startBeatStart) / ((float)Beat - startBeatStart);

        float endBeat = (float)(Beat + beatDuration);
        float startBeatEnd = (float)(endBeat - songChanelManager.beatInAdvance);
        float currentBeatEnd = Conductor.Instance.songPositionInBeatsUI;
        float interpolEnd = (currentBeatEnd - startBeatEnd) / (endBeat - startBeatEnd);

        songChanelManager.displayer.UpdateNote(note.transform.GetChild(0).gameObject, !hitStart ? (interpolStart < 0 ? 0 : interpolStart) : (interpolStart > 1 ? 1 : interpolEnd));
        songChanelManager.displayer.UpdateNote(note.transform.GetChild(1).gameObject, interpolEnd < 0 ? 0 : interpolEnd);

        if ((!hitStart && Conductor.Instance.songPosition > timeStart + timingLate) || (hitStart && Conductor.Instance.songPosition > timeEnd + timingLate))
        {
            OnActionEnd(-1, 0);
        }
    }

    protected virtual void OnActionStart(int timingWindow, double timing)
    {
        EffectManager.instance.PlayEffect(timingWindow, timing / songChanelManager.timingEvaluator.GetLatestInput());

        ScoreManager.instance.ComputeScore(timingWindow);
    }

    protected virtual void OnActionEnd(int timingWindow, double timing)
    {
        EffectManager.instance.PlayEffect(timingWindow, timing / songChanelManager.timingEvaluator.GetLatestInput());

        ScoreManager.instance.ComputeScore(timingWindow);

        NotePoolManager.instance.pools[poolID].DeleteNote(note);

        isPlaying = false;
    }
}
