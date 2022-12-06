using UnityEngine;

public class InstrumentVisibility : Note
{
    private double time;
    public double Time { get => time; }
    public double Beat { get; }
    private bool isPlaying;
    public bool IsPlaying { get => isPlaying; }

    private SongChanelManager songChanelManager;
    private bool visible;

    public InstrumentVisibility(double beat, SongChanelManager songChanelManager, bool visible)
    {
        Beat = beat;
        this.songChanelManager = songChanelManager;
        this.visible = visible;
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
            if (visible)
            {
                songChanelManager.onShowInstrument.Invoke();
            }
            else
            {
                songChanelManager.onHideInstrument.Invoke();
            }
        }
    }
}
