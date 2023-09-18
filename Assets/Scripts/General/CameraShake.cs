using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cmvc;
    public float shakeI = 2f;

    bool isShaking;

    private float timer;
    private CinemachineBasicMultiChannelPerlin perlinNoise;

    private void Awake()
    {
        cmvc = GetComponent<CinemachineVirtualCamera>();
    }

    public void Start()
    {
        StopShake();
    }

    public void ShakeCamera()
    {
        perlinNoise = cmvc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlinNoise.m_AmplitudeGain = shakeI;

        timer = 0.2f;
        isShaking = true;
    }

    void StopShake()
    {
        perlinNoise = cmvc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlinNoise.m_AmplitudeGain = 0;
        isShaking = false;

    }

    private void Update()
    {
        if (timer > 0 && isShaking)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                if (isShaking)
                {
                    StopShake();
                }
            }
        }
    }

}
