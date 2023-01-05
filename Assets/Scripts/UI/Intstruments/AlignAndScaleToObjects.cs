using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignAndScaleToObjects : MonoBehaviour
{
    public Transform baseObject;
    public Transform endObject;

    public Transform objectToMove;
    public Vector2 scaleFactors;

    void Update()
    {
        Vector3 direction = endObject.position - baseObject.position;
        objectToMove.transform.position = baseObject.position;
        float magn = direction.magnitude;
        if (magn > 0)
        {
            objectToMove.transform.forward = direction;
        }

        Vector3 scale = baseObject.transform.localScale;
        objectToMove.transform.localScale = new Vector3(scale.x * scaleFactors.x, scale.y * scaleFactors.y, magn);
    }
}
