using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CubeButton : MonoBehaviour
{
    public float timeToActivate;
    public float epsilon = 0.01f;
    public Image progressBar;
    public bool resets = false;
    public Transform[] hands;
    public UnityEvent onClick;

    private float currentActivatedTime;
    private Collider boxCollider;
    private bool activated = false;
    void Start()
    {
        currentActivatedTime = 0;
        boxCollider = GetComponent<Collider>();
    }

    void Update()
    {
        bool incrementTime = false;
        foreach (Transform t in hands)
        {
            if (Vector3.Distance(t.position, boxCollider.ClosestPoint(t.position)) < epsilon)
            {
                incrementTime = true;
                break;
            }
        }

        if (incrementTime)
        {
            currentActivatedTime += Time.deltaTime;
        }
        else
        {
            currentActivatedTime -= Time.deltaTime;
        }
        currentActivatedTime = Mathf.Clamp(currentActivatedTime, 0f, timeToActivate);

        if (currentActivatedTime >= timeToActivate)
        {
            if (!activated)
            {
                onClick.Invoke();
                if (resets)
                {
                    currentActivatedTime = 0;
                }
                else
                {
                    activated = true;
                }
            }
        }
        else
        {
            progressBar.fillAmount = currentActivatedTime / timeToActivate;
            activated = false;
        }
    }
}
