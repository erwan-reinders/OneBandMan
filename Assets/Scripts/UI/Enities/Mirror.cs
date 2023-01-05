using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public Camera mirrorCamera;
    public Transform mainCamera;
    public MeshRenderer mirrorRender;

    void Update()
    {
        Vector3 proj = Vector3.Project(mainCamera.transform.position - mirrorRender.transform.position, mirrorRender.transform.up);
        mirrorCamera.transform.position = mainCamera.transform.position - 2f * proj;
        mirrorCamera.transform.LookAt(mirrorRender.transform.position, Vector3.up);
    }
}
