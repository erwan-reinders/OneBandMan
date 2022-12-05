using UnityEngine;

public class ManiaNoteDisplayer : NoteDisplayer
{
    public Transform timePos;
    public override void UpdateNote(GameObject obj, float interpol)
    {
        obj.transform.localScale = Vector3.LerpUnclamped(spawnPos.lossyScale, timePos.lossyScale, interpol);
        obj.transform.rotation = Quaternion.LerpUnclamped(spawnPos.rotation, timePos.rotation, interpol);
        obj.transform.position = Vector3.LerpUnclamped(spawnPos.position, timePos.position, interpol);
    }
}
