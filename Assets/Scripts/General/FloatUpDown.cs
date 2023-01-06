using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUpDown : MonoBehaviour
{
    public float distance;
    public float speed;

    private Transform origin;

    private void Start()
    {
        origin = transform.parent;
    }

    void Update()
    {
        transform.position = origin.position + Vector3.up * Mathf.Sin(Time.time * speed) * distance;
    }
}
