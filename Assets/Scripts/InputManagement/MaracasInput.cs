using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaracasInput : InputManager
{
    public string chanelName;
    public float jerkAmount;
    public Transform hand;
    public int nbFrameLetActive;

    private Vector3 previousPos;
    private Vector3 previousSpeed;
    private Vector3 previousAcceleration;
    private int nbFramePassedActive;

    // Start is called before the first frame update
    void Start()
    {
        inputChanels = new string[1];
        inputChanels[0] = chanelName;
        InitChanels();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = hand.position;
        Vector3 speed = pos - previousPos;
        Vector3 acceleration = speed - previousSpeed;
        Vector3 jerk = acceleration - previousAcceleration;
        previousPos = pos;
        previousSpeed = speed;
        previousAcceleration = acceleration;

        float jerkMagnitude = jerk.magnitude;
        bool input = jerkMagnitude > jerkAmount;

        if (input)
        {
            nbFramePassedActive = nbFrameLetActive;
        }
        else
        {
            nbFramePassedActive--;
        }
        input = input || nbFramePassedActive > 0;

        bool lastFrameWasActive = inputs[chanelName].Active;
        inputs[chanelName].Pressed = !lastFrameWasActive && input;
        inputs[chanelName].Active = input;
        inputs[chanelName].Released = lastFrameWasActive && !input;
        inputs[chanelName].Volume = jerkMagnitude;
    }
}
