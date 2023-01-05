using UnityEngine;

public class EnableObject : BeatEvent
{
    public GameObject enableObject;
    public bool enable;

    public override void OnActivate(GameObject target)
    {
        enableObject.SetActive(enable);
    }
}
