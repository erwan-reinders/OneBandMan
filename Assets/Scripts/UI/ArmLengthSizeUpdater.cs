using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmLengthSizeUpdater : GrabableObject
{
    public TromboneInput tromboneInput;
    public ViolonInput violinInput;

    private void Start()
    {
        OnStart();
        transform.localScale = Vector3.one * (2 * tromboneInput.armLength);
    }

    void Update()
    {
        if (isGrabed)
        {
            float length = Vector3.Distance(transform.position, handHandle.transform.position);
            tromboneInput.armLength = length;
            violinInput.violinArmLength = length;

            transform.localScale = Vector3.one * (2 * tromboneInput.armLength);

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
