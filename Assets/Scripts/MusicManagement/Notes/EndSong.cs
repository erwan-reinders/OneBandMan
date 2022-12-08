using UnityEngine;

public class EndSong : Note
{
    private double time;
    public double Time { get => time; }
    public double Beat { get; }
    private bool isPlaying;
    public bool IsPlaying { get => isPlaying; }

    private SongChanelManager songChanelManager;

    public EndSong(double beat, SongChanelManager songChanelManager)
    {
        Beat = beat;
        this.songChanelManager = songChanelManager;
    }

    public bool OnPlayPress(InputSystem.Inputs input)
    {
        //Nothing
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

        isPlaying = true;
    }
    public void Update()
    {
        if (Util.SameTime(Conductor.Instance.songPosition, (float)time))
        {
            songChanelManager.onEndSong.Invoke();
        }
    }
}
