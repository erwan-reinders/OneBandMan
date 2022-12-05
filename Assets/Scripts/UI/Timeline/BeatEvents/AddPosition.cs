using UnityEngine;

public class AddPosition : BeatEvent
{
    public bool factorSize = false;
    public Vector3 position;

    public override void OnActivate(GameObject target)
    {
        Vector3 posFactor = Vector3.one;
        if (factorSize)
        {
            posFactor = target.transform.localScale;
        }
        Vector3 posToAdd = new Vector3(position.x * posFactor.x, position.y * posFactor.y, position.z * posFactor.z);
        target.transform.position += posToAdd;
    }
}
