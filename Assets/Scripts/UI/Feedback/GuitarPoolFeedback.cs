using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarPoolFeedback : MonoBehaviour
{
    public string chanel;
    public Material tmpNotMaterial;
    public Material tmpActiveStrummingMaterial;
    public Material tmpActivePickingMaterial;
    public Renderer rendererObject;
    public int nbFramesDisplayStruming = 3;

    private int currentNbFramesDisplayStruming;

    void Update()
    {
        InputSystem.Inputs input = InputSystem.GetInput(chanel);
        if (currentNbFramesDisplayStruming > 0)
        {
            rendererObject.material = tmpActiveStrummingMaterial;
            currentNbFramesDisplayStruming--;
        }
        else if (input.Active)
        {
            if (input.Type == GuitarInput.STRUMMING_TYPE)
            {
                rendererObject.material = tmpActiveStrummingMaterial;
                currentNbFramesDisplayStruming = nbFramesDisplayStruming;
            }
            else
            {
                rendererObject.material = tmpActivePickingMaterial;
            }
        }
        else
        {
            rendererObject.material = tmpNotMaterial;
        }
    }
}
