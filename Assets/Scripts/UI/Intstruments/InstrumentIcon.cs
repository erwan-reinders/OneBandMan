using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentIcon : MonoBehaviour
{
    public Vector3 outPosRelative;
    public float beat;

    private Vector3 initialPos;
    private Vector3 target;
    private Vector3 currentSpeed;

    void Start()
    {
        initialPos = transform.position;
        target = initialPos;
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target, ref currentSpeed, beat * (float)Conductor.Instance.secPerBeat);
    }

    public void Hide()
    {
        target = initialPos + outPosRelative;
    }

    public void Show()
    {
        target = initialPos;
    }
}
