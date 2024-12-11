using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_shake_controller : MonoBehaviour
{
    private CinemachineBasicMultiChannelPerlin noise;
    private float timeend;
    private bool shake;
    public void shakeCam(float t)
    {
        timeend = Time.realtimeSinceStartup + t;
        shake = true;
        noise.enabled = true;
    }

    private void Start()
    {
        timeend = 0;
        shake = false;
        noise = gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shake)
        {
            if (Time.realtimeSinceStartup > timeend)
            {
                shake = false;
                noise.enabled = false;
            }
        }
    }
}
