using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercutingInput : InputManager
{
    public string chanelName;
    public float minHitSpeed;
    public int nbFrameLetActive;
    public GameObject drum;
    public StickManager[] sticks;

    private int nbFramePassedActive;

    // Start is called before the first frame update
    void Start()
    {
        inputChanels = new string[1];
        inputChanels[0] = chanelName;
        InitChanels();
    }

    private void OnEnable()
    {
        foreach (StickManager stick in sticks)
        {
            stick.enabled = true;
        }
    }

    private void OnDisable()
    {
        foreach (StickManager stick in sticks)
        {
            if (stick != null)
            {
                stick.enabled = false;
            }
        }
    }

    void Update()
    {
        StickManager collidingStick = null;
        float maxSpeed = 0;
        bool justCollided = false;
        foreach (StickManager stick in sticks)
        {
            if (stick.InCollision && stick.CollidingObject == drum)
            {
                collidingStick = stick;
                maxSpeed = Mathf.Max(maxSpeed, stick.Speed);
                if (stick.JustCollided)
                {
                    justCollided = true;
                }
            }
        }


        bool input = collidingStick != null && collidingStick.Speed > minHitSpeed;

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
        inputs[chanelName].Pressed = justCollided;
        inputs[chanelName].Active = input;
        inputs[chanelName].Released = lastFrameWasActive && !input;
        inputs[chanelName].Volume = collidingStick != null ? maxSpeed : 0;
    }
}
