using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingEvaluator
{
    public TimingWindow[] timingWindows;


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
            this.end   =  symetricTime;
        }
    }
}
