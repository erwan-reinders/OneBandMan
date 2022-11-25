using UnityEngine;

public abstract class NotePool : MonoBehaviour
{
    public int poolNb;

    public Transform spawnPos;

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

    public abstract void UpdateNote(GameObject obj, float interpol);
    public virtual void DisplayInput(InputSystem.Inputs input)
    {

    }
}
