using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;

public class XRManager : MonoBehaviour
{
    private static XRManager instance;

    public float cameraMoveIncrements;
    public float cameraAngleIncrements;

    public CameraOffset XRRig;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Error : There are more than one XRManager");
        }
    }

    public static void MoveCameraUp()
    {
        //instance.XRRig.cameraYOffset += instance.cameraMoveIncrements;
        instance.XRRig.transform.position += Vector3.up * instance.cameraMoveIncrements;
    }

    public static void MoveCameraDown()
    {
        //instance.XRRig.cameraYOffset -= instance.cameraMoveIncrements;
        instance.XRRig.transform.position += Vector3.down * instance.cameraMoveIncrements;
    }

    public static void MoveCameraLeft()
    {
        instance.XRRig.transform.position += Vector3.left * instance.cameraMoveIncrements;
    }

    public static void MoveCameraRight()
    {
        instance.XRRig.transform.position += Vector3.right * instance.cameraMoveIncrements;
    }

    public static void MoveCameraForward()
    {
        instance.XRRig.transform.position += Vector3.forward * instance.cameraMoveIncrements;
    }

    public static void MoveCameraBackward()
    {
        instance.XRRig.transform.position += Vector3.back * instance.cameraMoveIncrements;
    }

    public static void RotateCameraRight()
    {
        instance.XRRig.transform.Rotate(Vector3.down, instance.cameraAngleIncrements);
    }

    public static void RotateCameraLeft()
    {
        instance.XRRig.transform.Rotate(Vector3.down, instance.cameraAngleIncrements);
    }
}
