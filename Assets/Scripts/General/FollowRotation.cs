using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRotation: MonoBehaviour
{
    public Transform target;
    public Vector3 eulerOffset;

    void Update()

    {
        transform.rotation = target.rotation * Quaternion.Euler(eulerOffset);
    }
}
