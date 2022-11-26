using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TromboneInput : InputManager
{
    public XRNode controller;
    public float armLength = 0.3f;
    public float minDistance = 0.05f;
    public Transform head;
    public Transform hand;
    public string[] channelNames;

    public Transform cursor;
    public Transform cursorStart;
    public Transform cursorEnd;
    public Transform cursorSeparatorParent;
    public GameObject cursorSeparatorPrefab;

    private List<InputDevice> inputControllers;
    private List<InputFeatureUsage<bool>> inputFeatures;

    void Start()
    {
        inputChanels = channelNames;
        InitChanels();

        inputFeatures = new List<InputFeatureUsage<bool>>();
        inputFeatures.Add(CommonUsages.gripButton);
        inputFeatures.Add(CommonUsages.triggerButton);
        inputFeatures.Add(CommonUsages.primaryButton);
        inputFeatures.Add(CommonUsages.secondaryButton);

        inputControllers = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(controller, inputControllers);
        InputDevices.deviceConnected += InputDevices_deviceConnected;
        InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;

        for (int i = 1; i < channelNames.Length; i++)
        {
            Instantiate(cursorSeparatorPrefab, Vector3.Lerp(cursorStart.position, cursorEnd.position, i /(float) channelNames.Length), Quaternion.identity, cursorSeparatorParent);
        }
    }

    private void InputDevices_deviceConnected(InputDevice device)
    {
        inputControllers.Clear();
        InputDevices.GetDevicesAtXRNode(controller, inputControllers);
    }

    private void InputDevices_deviceDisconnected(InputDevice device)
    {
        if (inputControllers.Contains(device))
            inputControllers.Remove(device);
    }

    private bool CheckDeviceInput()
    {
        foreach (InputDevice inputController in inputControllers)
        {
            foreach (InputFeatureUsage<bool> feature in inputFeatures)
            {
                bool featureState;
                if (inputController.TryGetFeatureValue(feature, out featureState)
                    && featureState)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void UpdateInputState(string chanel, bool input)
    {
        bool lastFrameWasActive = inputs[chanel].Active;
        inputs[chanel].Pressed = !lastFrameWasActive && input;
        inputs[chanel].Active = input;
        inputs[chanel].Released = lastFrameWasActive && !input;
    }

    void Update()
    {
        float length = Vector3.Distance(head.position, hand.position) - minDistance;
        length = length / armLength;
        int chanelId = (int)Mathf.Clamp(length * inputChanels.Length, 0, inputChanels.Length - 1);
        string chanel = inputChanels[chanelId];

        bool input = CheckDeviceInput();

        foreach (string c in inputChanels)
        {
            if (c != chanel)
            {
                UpdateInputState(c, false);
            }
            else
            {
                UpdateInputState(c, input);
            }
        }

        cursor.position = Vector3.Lerp(cursorStart.position, cursorEnd.position, length);
    }
}
