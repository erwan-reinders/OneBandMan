using System;

[Serializable]
public class TimingEvaluator
{
    public TimingWindow[] timingWindows;

    public TimingEvaluator()
    {
        timingWindows = new TimingWindow[3];
        timingWindows[0] = new TimingWindow(0.03333d);
        timingWindows[1] = new TimingWindow(0.11667d);
        timingWindows[2] = new TimingWindow(0.25000d);
    }

    public int EvaluateTiming(double time)
    {
        for (int tW = 0; tW < timingWindows.Length; tW++)
        {
            TimingWindow timingWindow = timingWindows[tW];
            if (timingWindow.Contains(time))
            {
                return tW;
            }
        }
        return -1;
    }

    public double GetLatestInput()
    {
        return timingWindows[timingWindows.Length - 1].end;
    }

    [Serializable]
    public class TimingWindow
    {
        public double start;
        public double end;

        public TimingWindow(double start, double end)
        {
            this.start = start;
            this.end = end;
        }

        public TimingWindow(double symetricTime)
        {
            this.start = -symetricTime;
            this.end = symetricTime;
        }

        public bool Contains(double time)
        {
            return start <= time && time <= end;
        }
    }
}
