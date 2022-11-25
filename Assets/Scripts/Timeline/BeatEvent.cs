using UnityEngine;

public abstract class BeatEvent : MonoBehaviour
{
    public double beat;
    public abstract void OnActivate(GameObject target);
}