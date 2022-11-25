using System.Collections.Generic;
using UnityEngine;

public abstract class InputManager : MonoBehaviour
{
    protected string[] inputChanels;

    protected Dictionary<string, InputSystem.Inputs> inputs;

    protected void InitChanels()
    {
        inputs = new Dictionary<string, InputSystem.Inputs>();
        foreach (string input in inputChanels)
        {
            if (!InputSystem.inputs.ContainsKey(input))
            {
                InputSystem.inputs.Add(input, new InputSystem.Inputs());
            }
            inputs.Add(input, InputSystem.inputs[input]);
        }
    }
}
