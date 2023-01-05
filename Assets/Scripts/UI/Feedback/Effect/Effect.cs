using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public float launchSpeedMin;
    public float launchSpeedMax;
    public float launchRotationSpeedMin;
    public float launchRotationSpeedMax;
    public float gravity;
    public float vibratePositionAmount;
    public float vibrateRotationAmount;
    public float activeTime;

    private Vector3 relativePosition;
    private Vector3 velocity;
    private Vector3 relativeRotation;
    private Vector3 rotationVelocity;
    private float curentTime;

    public void Init()
    {
        gameObject.SetActive(true);
    }

    void OnEnable()
    {
        relativePosition = transform.position;
        relativeRotation = Vector3.zero;

        velocity = new Vector3(Random.Range(-launchSpeedMax, launchSpeedMax) * 0.5f, Random.Range(launchSpeedMin, launchSpeedMax), 0f);
        rotationVelocity = Vector3.forward * Random.Range(launchRotationSpeedMin, launchRotationSpeedMax);

        curentTime = activeTime;
    }

    void Update()
    {
        relativePosition = relativePosition + velocity * Time.deltaTime;
        velocity += Physics.gravity * gravity;

        relativeRotation = relativeRotation + rotationVelocity * Time.deltaTime;

        transform.position = relativePosition + new Vector3(Random.Range(-vibratePositionAmount, vibratePositionAmount), Random.Range(-vibratePositionAmount, vibratePositionAmount), Random.Range(-vibratePositionAmount, vibratePositionAmount));
        transform.localEulerAngles = relativeRotation + new Vector3(Random.Range(-vibrateRotationAmount, vibrateRotationAmount), Random.Range(-vibrateRotationAmount, vibrateRotationAmount), Random.Range(-vibrateRotationAmount, vibrateRotationAmount));

        curentTime -= Time.deltaTime;
        if (curentTime < 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
