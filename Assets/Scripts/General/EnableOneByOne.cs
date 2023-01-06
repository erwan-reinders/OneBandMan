using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOneByOne : MonoBehaviour
{
    public float time;

    private int curentChild = 0;

    void Start()
    {
        InvokeRepeating("Change", time, time);
    }

    private void Change()
    {
        transform.GetChild(curentChild).gameObject.SetActive(false);
        curentChild = (curentChild + 1) % transform.childCount;
        transform.GetChild(curentChild).gameObject.SetActive(true);
    }
}
