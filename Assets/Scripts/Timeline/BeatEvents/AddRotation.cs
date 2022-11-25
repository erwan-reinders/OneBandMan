using UnityEngine;

public class AddRotation : BeatEvent
{
    public Quaternion rotation;

    public override void OnActivate(GameObject target)
    {
        target.transform.rotation *= rotation;
    }
}
