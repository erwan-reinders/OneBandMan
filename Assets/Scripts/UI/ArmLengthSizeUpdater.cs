using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmLengthSizeUpdater : GrabableObject
{
    //TODO list d'input ayant une taille de bras...
    public TromboneInput tromboneInput;
    public ViolonInput violinInput;

    private void Start()
    {
        OnStart();
        float length;
        if (tromboneInput != null)
        {
            length = tromboneInput.armLength;
        }
        else
        {
            length = violinInput.violinArmLength;
        }
        transform.localScale = Vector3.one * (2 * length);
    }

    void Update()
    {
        if (isGrabed)
        {
            float length = Vector3.Distance(transform.position, handHandle.transform.position);
            if (tromboneInput != null)
            {
                tromboneInput.armLength = length - tromboneInput.minDistance;
            }
            if (violinInput != null)
            {
                violinInput.violinArmLength = length - violinInput.violinMinDistance;
            }

            float armLength;
            if (tromboneInput != null)
            {
                armLength = tromboneInput.armLength;
            }
            else
            {
                armLength = violinInput.violinArmLength;
            }
            transform.localScale = Vector3.one * (2 * armLength);

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
