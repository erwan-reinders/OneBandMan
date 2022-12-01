using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertMesh : MonoBehaviour
{
    void Awake()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        Vector3[] normals = meshFilter.mesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }
        meshFilter.mesh.normals = normals;

        int[] triangles = meshFilter.sharedMesh.triangles;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            int t = triangles[i];
            triangles[i] = triangles[i + 2];
            triangles[i + 2] = t;
        }

        meshFilter.mesh.triangles = triangles;
    }
}
