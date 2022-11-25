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

    public override void DisplayInput(InputSystem.Inputs input)
    {
        if (input.Active)
        {
            spawnPos.GetChild(0).GetComponent<Renderer>().material = tmpActiveMaterial;
        }
        else
        {
            spawnPos.GetChild(0).GetComponent<Renderer>().material = tmpNotMaterial;
        }
    }
}
