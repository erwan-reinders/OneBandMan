using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTest : MonoBehaviour
{
    public float angleIncrement = 90f;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        float floor = Mathf.Floor(Conductor.Instance.songPositionInBeats);
        transform.rotation = Quaternion.Euler(0, 0, angleIncrement * floor);
    }
}
