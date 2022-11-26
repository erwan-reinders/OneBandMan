using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolonInput : InputManager
{
    public float violinArmLength = 0.3f;
    public float violinMinDistance = 0.05f;
    public float bowMinSpeed = 0.001f;

    public Transform head;
    public Transform leftHand;
    public Transform rightHand;
    public string[] channelNames;

    public Transform cursor;
    public Transform cursorStart;
    public Transform cursorEnd;
    public Transform cursorSeparatorParent;
    public GameObject cursorSeparatorPrefab;


    private Vector3 previousRightHandPos;


    void Start()
    {
        inputChanels = channelNames;
        InitChanels();

        for (int i = 1; i < channelNames.Length; i++)
        {
            Instantiate(cursorSeparatorPrefab, Vector3.Lerp(cursorStart.position, cursorEnd.position, i / (float)channelNames.Length), Quaternion.identity, cursorSeparatorParent);
        }
    }

    private void UpdateInputState(string chanel, bool input, float speed)
    {
        bool lastFrameWasActive = inputs[chanel].Active;
        inputs[chanel].Pressed = !lastFrameWasActive && input;
        inputs[chanel].Active = input;
        inputs[chanel].Released = lastFrameWasActive && !input;
        inputs[chanel].Volume = speed;
    }

    void Update()
    {
        float violonDistance = Vector3.Distance(head.position, leftHand.position) - violinMinDistance;
        violonDistance = violonDistance / violinArmLength;
        int chanelId = (int)Mathf.Clamp(violonDistance * inputChanels.Length, 0, inputChanels.Length - 1);
        string chanel = inputChanels[chanelId];

        Vector3 bowPlayDirection = Vector3.Normalize(head.position - rightHand.position);
        Vector3 bowMovement = rightHand.position - previousRightHandPos;
        previousRightHandPos = rightHand.position;
        float bowSpeed = Mathf.Abs(Vector3.Dot(bowMovement, bowPlayDirection));

        bool input = bowSpeed >= bowMinSpeed;

        foreach (string c in inputChanels)
        {
            if (c != chanel)
            {
                UpdateInputState(c, false, 0);
            }
            else
            {
                UpdateInputState(c, input, bowSpeed);
            }
        }

        cursor.position = Vector3.Lerp(cursorStart.position, cursorEnd.position, violonDistance);
    }
}
