
public interface Note
{
    public double Time { get; }
    public double Beat { get; }
    public bool IsPlaying { get; }

    //Returns true if we should consume input
    public bool OnPlayPress();
    public bool OnPlayRelease();

    public void Start();
    public void Update();
}
