using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineTimeline : MonoBehaviour
{
    public bool loop;
    //public float loopTime;
    private int loopCounter;
    private PlayableDirector director;
    // Start is called before the first frame update
    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        double time = Conductor.Instance.songPositionInBeats * (1.0d / TimelineProjectSettings.instance.defaultFrameRate);
        if (loop)
        {
            time -= loopCounter * director.duration;
        }
        director.time = time;
        director.Evaluate();
        if (director.time >= director.duration)
        {
            loopCounter++;
        }
    }
}
