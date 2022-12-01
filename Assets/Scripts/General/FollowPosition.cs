using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    public Transform target;
    public Vector3 worldOffset;

    void Update()
    {
        transform.position = target.position+worldOffset;
    }
}
