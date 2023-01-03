using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    public Effect[] effectPrefabs;

    private Effect[] effects;

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

        effects = new Effect[effectPrefabs.Length];
        for (int i = 0; i < effectPrefabs.Length; i++)
        {
            Effect effectPrefab = effectPrefabs[i];
            effects[i] = Instantiate(effectPrefab, transform.position, transform.rotation, transform);
            effects[i].gameObject.SetActive(false);
        }
    }

    public void PlayEffect(int timingWindow)
    {
        effects[timingWindow+1].Init();
    }
}
