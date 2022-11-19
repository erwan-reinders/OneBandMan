using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeatDurationEvent : BeatEvent
{
    public double duration;
    public abstract void OnUpdate(GameObject target, double time);
    public abstract void OnDeactivate(GameObject target);
}
