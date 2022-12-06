using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllignToObjects : MonoBehaviour
{
    public Transform baseObj;
    public Transform towardObj;

    void Update()
    {
        Vector3 direction = towardObj.position - baseObj.position;
        transform.position = baseObj.position;
        transform.forward = direction;
    }
}
