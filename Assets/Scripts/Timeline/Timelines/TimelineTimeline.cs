using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineTimeline : MonoBehaviour
{
    public bool loop;
	public double multiplcator = 1.0d;
	
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
        double time = Conductor.Instance.songPositionInBeats * (multiplcator / TimelineProjectSettings.instance.defaultFrameRate);
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
		else if (director.time < 0.0f) {
			loopCounter--;
		}
    }
}
