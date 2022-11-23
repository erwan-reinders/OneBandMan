using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePool : MonoBehaviour
{
    public int poolNb;

    public Transform spawnPos;
    public Transform timePos;

    public GameObject pool;
    public GameObject objectPrefab;

    private int poolId;

    private void Start()
    {
        for (int i = 0; i < poolNb; i++)
        {
            GameObject obj = Instantiate(objectPrefab, spawnPos.position, Quaternion.identity, pool.transform);
            obj.SetActive(false);
        }
    }

    public GameObject SpawnNewNote()
    {
        GameObject obj = pool.transform.GetChild(poolId).gameObject;
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
