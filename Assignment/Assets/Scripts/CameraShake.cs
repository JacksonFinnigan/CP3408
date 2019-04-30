using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeAmount = 0;
    public Camera mainCam;

    private void Awake()
    {
        if (mainCam == null)
            mainCam = Camera.main;
    }

    public void Shake(float amt, float length)
    {
        shakeAmount = amt;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", length);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Shake(shakeAmount, 0.2f);
        }    
    }

    private void BeginShake()
    {
        if (shakeAmount > 0)
        {
            Vector3 camPos = mainCam.transform.position;

            float shakeOffsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float shakeOffsetY = Random.value * shakeAmount * 2 - shakeAmount;
            camPos.x += shakeOffsetX;
            camPos.y += shakeOffsetY;

            mainCam.transform.position = camPos;
        }
    }

    void StopShake()
    {
        CancelInvoke("BeginShake");
        mainCam.transform.localPosition = Vector3.zero;
    }
}
