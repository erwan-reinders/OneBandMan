using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePool : MonoBehaviour
{
    public int poolNb;
    public GameObject objectPrefab;

    protected int poolId;

    void Start()
    {
        if (transform.childCount > 0)
        {
            Debug.LogError("Error : a NotePool must not have children !");
        }
        for (int i = 0; i < poolNb; i++)
        {
            GameObject obj = Instantiate(objectPrefab, transform.position, objectPrefab.transform.rotation, transform);
            obj.SetActive(false);
        }
    }

    public GameObject SpawnNewNote()
    {
        GameObject obj = transform.GetChild(poolId).gameObject;
        if (obj.activeSelf)
        {
            Debug.LogWarning("Warning : not enough object instanciated");
        }
        obj.SetActive(true);

        poolId = (poolId + 1) % poolNb;
        return obj;
    }

    public void DeleteNote(GameObject obj)
    {
        obj.SetActive(false);
    }
}
