using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    public Transform earlyPos;
    public Transform latePos;

    public Effect[] effectPrefabs;
    public int poolNb;

    //private Effect[] effects;
    private GameObject[] pools;
    private int[] poolIds;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Error : There is more than one EffectManager !");
        }
    }

    void Start()
    {
        if (effectPrefabs.Length != ScoreManager.instance.scoreValuesPerTimingWindow.Length + 1)
        {
            Debug.LogWarning("Effect count (" + effectPrefabs.Length + ") does not match score timing windows (" + (ScoreManager.instance.scoreValuesPerTimingWindow.Length + 1) + ")");
        }

        //effects = new Effect[effectPrefabs.Length];
        pools = new GameObject[effectPrefabs.Length];
        poolIds = new int[effectPrefabs.Length];
        for (int i = 0; i < effectPrefabs.Length; i++)
        {
            poolIds[i] = 0;
            pools[i] = Instantiate(new GameObject(effectPrefabs[i].name + "Pool"), transform.position, transform.rotation, transform);
            for (int j = 0; j < poolNb; j++)
            {
                Effect effectPrefab = effectPrefabs[i];
                Effect instance = Instantiate(effectPrefab, transform.position, transform.rotation, pools[i].transform);
                instance.gameObject.SetActive(false);
            }
        }
    }

    public void PlayEffect(int timingWindow, double timing)
    {
        //effects[timingWindow+1].Init();
        GameObject obj = pools[timingWindow+1].transform.GetChild(poolIds[timingWindow+1]).gameObject;
        if (obj.activeSelf)
        {
            Debug.LogWarning("Warning : not enough effects instanciated (" + effectPrefabs[timingWindow+1].name + ", " + Conductor.Instance.songPositionInBeats + ")");
        }
        float interpol = (timingWindow == -1) ? 0.5f : ((float)timing + 1f) / 2f;
        obj.transform.position = Vector3.Lerp(earlyPos.position, latePos.position, interpol);
        obj.SetActive(true);

        poolIds[timingWindow+1] = (poolIds[timingWindow+1] + 1) % poolNb;
    }
}
