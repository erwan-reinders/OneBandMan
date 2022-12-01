using UnityEngine;

public class ShowChannel : Note
{
    private double time;
    public double Time { get => time; }
    public double Beat { get; }
    private bool isPlaying;
    public bool IsPlaying { get => isPlaying; }

    private SongChanelManager songChanelManager;

    public ShowChannel(double beat, SongChanelManager songChanelManager)
    {
        Beat = beat;
        this.songChanelManager = songChanelManager;
    }

    public bool OnPlayPress()
    {
        //Nothing
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

        isPlaying = true;
    }
    public void Update()
    {
        /*float timingLate = (float)songChanelManager.timingEvaluator.GetLatestInput();

        float startBeat = (float)(Beat - songChanelManager.beatInAdvance);
        float currentBeat = Conductor.Instance.songPositionInBeatsUI;
        float interpol = (currentBeat - startBeat) / ((float)Beat - startBeat);

        songChanelManager.pool.UpdateNote(note, interpol);*/

        if (Util.SameTime(Conductor.Instance.songPosition, (float)time))
        {
            songChanelManager.pool.gameObject.SetActive(true);
        }
    }
}
