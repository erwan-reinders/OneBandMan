using UnityEngine;

public class OsuNoteDisplayer : NoteDisplayer
{
    public Vector3 startScale;
    public Vector3 timeScale;

    public Material tmpNotMaterial;
    public Material tmpActiveMaterial;

    public override void UpdateNote(GameObject obj, float interpol)
    {
        obj.transform.localScale = Vector3.LerpUnclamped(startScale, timeScale, Mathf.Sqrt(interpol));
        obj.transform.rotation = spawnPos.rotation;
        obj.transform.position = spawnPos.position;
    }
}
