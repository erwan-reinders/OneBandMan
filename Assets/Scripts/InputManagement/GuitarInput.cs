using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GuitarInput : InputManager
{
    public const string STRUMMING_TYPE = "strumming";
    public const string PICKING_TYPE = "picking";

    public float armLength = 0.3f;
    public float minDistance = 0.05f;
    public float minSpeed = 0.004f;
    public Collider stringCollider;

    public XRNode controller;

    public Transform leftHand;
    public Transform rightHand;
    public string[] channelNames;

    public Transform cursor;
    public Transform cursorStart;
    public Transform cursorEnd;
    public Transform cursorSeparatorParent;
    public GameObject cursorSeparatorPrefab;

    private List<InputDevice> inputControllers;
    private List<InputFeatureUsage<bool>> inputFeatures;

    private Vector3 previousRightHandPos;


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
            GameObject obj = Instantiate(cursorSeparatorPrefab, cursorSeparatorParent);
            obj.transform.position = Vector3.Lerp(cursorStart.position, cursorEnd.position, i / (float)channelNames.Length);
            obj.transform.localEulerAngles = Vector3.zero;
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

    private void UpdateInputState(string chanel, bool input, float speed, string type)
    {
        bool lastFrameWasActive = inputs[chanel].Active;
        inputs[chanel].Pressed = !lastFrameWasActive && input;
        inputs[chanel].Active = input;
        inputs[chanel].Released = lastFrameWasActive && !input;
        inputs[chanel].Volume = speed;
        inputs[chanel].Type = type;
    }

    void Update()
    {
        float leftHandDistance = Vector3.Distance(stringCollider.transform.position, leftHand.position) - minDistance;
        leftHandDistance = leftHandDistance / armLength;
        int chanelId = (int)Mathf.Clamp(leftHandDistance * inputChanels.Length, 0, inputChanels.Length - 1);
        string chanel = inputChanels[chanelId];

        bool inCollision = Vector3.Distance(rightHand.position, stringCollider.ClosestPoint(rightHand.position)) < 0.01f;

        bool picking = inCollision && CheckDeviceInput();

        bool strumming = inCollision;
        float playSpeed = 0f;
        if (strumming)
        {
            playSpeed = Vector3.Distance(rightHand.position, previousRightHandPos);
            strumming = playSpeed > minSpeed;
        }

        bool input = picking || strumming;
        string type = "";
        if (strumming)
        {
            type = STRUMMING_TYPE;
        }
        else if (picking)
        {
            type = PICKING_TYPE;
        }

        foreach (string c in inputChanels)
        {
            if (c != chanel)
            {
                UpdateInputState(c, false, 0, type);
            }
            else
            {
                UpdateInputState(c, input, playSpeed, type);
            }
        }

        previousRightHandPos = rightHand.position;

        cursor.position = Vector3.Lerp(cursorStart.position, cursorEnd.position, leftHandDistance);
    }
}
