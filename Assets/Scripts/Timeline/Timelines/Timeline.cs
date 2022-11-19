using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline : MonoBehaviour
{
    public bool loop;
    public float loopDuration;
    public GameObject timelineObject;

    public int currentEventId;
    public int loopCounter;
    private int maxEventAtOnce = 10;
    private BeatEvent currentEvent;

    // Start is called before the first frame update
    void Start()
    {
        currentEventId = 0;
        loopCounter = 0;
        currentEvent = timelineObject.transform.GetChild(currentEventId).GetComponent<BeatEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        double eps = 1.0d / AudioSettings.outputSampleRate;
        int iter = 0;
        bool condition;
        do
        {
            double loopPosition = Conductor.Instance.songPositionInBeats - loopCounter * loopDuration;
            condition = currentEvent.beat - loopPosition < eps;

            if (condition)
            {
                currentEvent.OnActivate(gameObject);
                currentEventId++;
                if (currentEventId >= timelineObject.transform.childCount)
                {
                    currentEventId = 0;
                    loopCounter++;
                    if (!loop)
                    {
                        enabled = false;
                    }
                }
                currentEvent = timelineObject.transform.GetChild(currentEventId).GetComponent<BeatEvent>();
            }
            iter++;
        } while (condition && iter < maxEventAtOnce);
        
    }
}
