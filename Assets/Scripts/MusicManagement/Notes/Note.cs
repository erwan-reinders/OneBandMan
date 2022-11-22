
public interface Note
{
    public double Time { get; }

    public void OnPlayPress(double playTime);
    public void OnPlayRelease(double playTime);

    public void Start();
    public void Update();
}
