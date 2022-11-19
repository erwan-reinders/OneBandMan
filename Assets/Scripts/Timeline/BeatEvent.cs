using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeatEvent : MonoBehaviour
{
    public double beat;
    public abstract void OnActivate(GameObject target);
}