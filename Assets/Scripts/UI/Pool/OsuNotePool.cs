using UnityEngine;

public class OsuNotePool : NotePool
{
    public Vector3 startScale;
    public Vector3 timeScale;

    public Material tmpNotMaterial;
    public Material tmpActiveMaterial;

    public override void UpdateNote(GameObject obj, float interpol)
    {
        obj.transform.localScale = Util.LerpUnclamped(startScale, timeScale, interpol);
    }
}
