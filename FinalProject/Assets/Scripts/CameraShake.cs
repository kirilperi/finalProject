using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private const float smoothSpeed = 0.2f;
    private float strength;
    private float remainingShakeTime;
    private Vector3 initialCameraPosition;
    private Vector3 newShakePosition;

    public void Shake(float power,float duration)
    {
        initialCameraPosition = transform.localPosition;
        strength = power;
        remainingShakeTime = duration;
    }


    private void LateUpdate()
    {
        if(remainingShakeTime<0)
        { return; }

        newShakePosition = initialCameraPosition + (Vector3)Random.insideUnitCircle * strength;
        transform.localPosition = Vector3.Lerp(transform.localPosition, newShakePosition, smoothSpeed);
        remainingShakeTime -= Time.deltaTime;

    }
}
