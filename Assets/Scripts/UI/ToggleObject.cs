using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    public GameObject obj;

    public void Activate()
    {
        obj.SetActive(!obj.activeSelf);
    }
}
