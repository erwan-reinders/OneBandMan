using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolFeedback : MonoBehaviour
{
    public string chanel;
    public Material tmpNotMaterial;
    public Material tmpActiveMaterial;
    public Renderer rendererObject;

    void Update()
    {
        InputSystem.Inputs input = InputSystem.GetInput(chanel);
        if (input.Active)
        {
            rendererObject.material = tmpActiveMaterial;
        }
        else
        {
            rendererObject.material = tmpNotMaterial;
        }
    }
}
