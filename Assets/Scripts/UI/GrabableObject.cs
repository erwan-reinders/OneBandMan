using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GrabableObject : MonoBehaviour
{
    public Transform objectToMove;
    public Transform handHandle;
    public XRNode controller;

    protected bool isGrabed = false;
    private InputDevice inputController;
    private List<InputFeatureUsage<bool>> inputFeatures;

    void Start()
    {
        OnStart();
    }

    protected void OnStart()
    {
        inputFeatures = new List<InputFeatureUsage<bool>>();
        inputFeatures.Add(CommonUsages.gripButton);
        inputFeatures.Add(CommonUsages.triggerButton);
        inputFeatures.Add(CommonUsages.primaryButton);
        inputFeatures.Add(CommonUsages.secondaryButton);

        List<InputDevice> inputControllers = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(controller, inputControllers);
        if (inputControllers.Count > 0)
        {
            inputController = inputControllers[0];
        }

        InputDevices.deviceConnected += InputDevices_deviceConnected;
        InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;
    }

    private void InputDevices_deviceConnected(InputDevice device)
    {
        List<InputDevice> inputControllers = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(controller, inputControllers);
        if (inputControllers.Count > 0)
        {
            inputController = inputControllers[0];
        }
    }

    private void InputDevices_deviceDisconnected(InputDevice device)
    {
        if (inputController == device)
            Drop();
    }

    protected bool CheckDeviceInput()
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
        return false;
    }

    void Update()
    {
        if (isGrabed)
        {
            objectToMove.position = handHandle.position;
            if (CheckDeviceInput())
            {
                Drop();
            }
        }
    }

    public virtual void Grab()
    {
        isGrabed = true;
    }

    public void Drop()
    {
        isGrabed = false;
    }
}
