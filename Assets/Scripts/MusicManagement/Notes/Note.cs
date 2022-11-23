
public interface Note
{
    public double Time { get; }
    public double Beat { get; }
    public bool IsPlaying { get; }

    public void OnPlayPress();
    public void OnPlayRelease();

    public void Start();
    public void Update();
}
