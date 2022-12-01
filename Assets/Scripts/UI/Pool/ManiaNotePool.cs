using UnityEngine;

public class ManiaNotePool : NotePool
{
    public Transform timePos;
    public override void UpdateNote(GameObject obj, float interpol)
    {
        obj.transform.position = Util.LerpUnclamped(spawnPos.position, timePos.position, interpol);
    }
}
