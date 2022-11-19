using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRotationDuration : BeatDurationEvent
{
    public Quaternion rotation;

    public override void OnActivate(GameObject target)
    {
        target.transform.rotation = rotation;
    }

    public override void OnDeactivate(GameObject target)
    {
        
    }

    public override void OnUpdate(GameObject target, double time)
    {
        
    }
}
