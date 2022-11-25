using UnityEngine;

public class ManiaNotePool : NotePool
{
    public Transform timePos;

    public Material tmpNotMaterial;
    public Material tmpActiveMaterial;

    public override void UpdateNote(GameObject obj, float interpol)
    {
        obj.transform.position = Util.LerpUnclamped(spawnPos.position, timePos.position, interpol);
    }

    public override void DisplayInput(InputSystem.Inputs input)
    {
        if (input.Active)
        {
            timePos.GetChild(0).GetComponent<Renderer>().material = tmpActiveMaterial;
        }
        else
        {
            timePos.GetChild(0).GetComponent<Renderer>().material = tmpNotMaterial;
        }
    }
}
