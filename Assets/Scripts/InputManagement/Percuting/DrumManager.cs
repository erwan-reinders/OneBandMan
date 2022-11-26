using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumManager : MonoBehaviour
{
    public string tagAgainst = "Stick";
    public bool InCollision { get; private set; }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagAgainst))
        {
            InCollision = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagAgainst))
        {
            InCollision = false;
        }
    }
}
