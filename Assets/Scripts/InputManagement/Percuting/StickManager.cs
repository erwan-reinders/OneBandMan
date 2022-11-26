using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManager : MonoBehaviour
{
    public string tagAgainst;
    public bool InCollision { get; private set; }
    public GameObject CollidingObject { get; private set; }
    public float Speed { get; private set; }

    private Vector3 previousPos;
    private Renderer diplay;

    private void Start()
    {
        diplay = GetComponent<Renderer>();
    }

    private void Update()
    {
        Speed = Vector3.Magnitude(transform.position - previousPos);
        previousPos = transform.position;
    }

    private void OnEnable()
    {
        if (diplay != null)
        {
            diplay.enabled = true;
        }
    }

    private void OnDisable()
    {
        diplay.enabled = false;
        InCollision = false;
        CollidingObject = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagAgainst))
        {
            InCollision = true;
            CollidingObject = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagAgainst))
        {
            InCollision = false;
            CollidingObject = null;
        }
    }
}
