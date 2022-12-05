using UnityEngine;

public abstract class NoteDisplayer : MonoBehaviour
{
    public Transform spawnPos;

    public abstract void UpdateNote(GameObject obj, float interpol);
}
