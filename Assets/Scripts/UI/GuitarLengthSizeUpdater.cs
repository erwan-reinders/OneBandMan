using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarLengthSizeUpdater : GrabableObject
{
    //TODO list d'input ayant une taille de bras...
    public GuitarInput guitarInput;

    private void Start()
    {
        OnStart();
        transform.localScale = Vector3.one * (2 * guitarInput.armLength);
    }

    void Update()
    {
        if (isGrabed)
        {
            float length = Vector3.Distance(transform.position, handHandle.transform.position);
            guitarInput.armLength = length;

            transform.localScale = Vector3.one * (2 * guitarInput.armLength);

            if (CheckDeviceInput())
            {
                Drop();
            }
        }
    }

    public override void Grab()
    {
        isGrabed = true;
        gameObject.SetActive(true);
    }
}
