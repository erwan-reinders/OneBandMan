using UnityEngine;

public class SetRotation : BeatEvent
{
    public Quaternion rotation;

    public override void OnActivate(GameObject target)
    {
        target.transform.rotation = rotation;
    }
}
