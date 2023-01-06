using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToward : MonoBehaviour
{
    public Transform objectToMove;
    public Transform target;
    public float beatDuration;
    public bool linear;

    private Vector3 startPos;
    private float startBeat;

    void OnEnable()
    {
        startPos = objectToMove.transform.position;
        startBeat = Conductor.Instance.songPositionInBeats;
    }

    void Update()
    {
        float t = (Conductor.Instance.songPositionInBeats - startBeat) / beatDuration;
        float inter = linear ? t : Mathf.SmoothStep(0f, 1f, t);
        objectToMove.transform.position = Vector3.Lerp(startPos, target.position, inter);
        if (t > 1f)
        {
            gameObject.SetActive(false);
        }
    }
}
