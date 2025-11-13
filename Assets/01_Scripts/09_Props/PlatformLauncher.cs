using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLauncher : MonoBehaviour
{
    [SerializeField] private float readyDuration = 3.0f;
    [SerializeField] private Transform direction;
    [SerializeField] private float jumpPower = 20f;

    private bool isReady = false;
    private float nowDuration = 0.0f;

    private Player player;

    private void Update()
    {
        if (isReady)
        {
            nowDuration += Time.deltaTime;
            InGameUIManager.Instance?.SetGauge(nowDuration/readyDuration);

            if (nowDuration >= readyDuration)
            {
                isReady = false;
                nowDuration = 0.0f;
                Launch(); 
            }
        }
    }

    public void Launch()
    {
        player?.Controller.ForceJump(jumpPower, Vector3.Normalize(direction.forward + Vector3.up));
        InGameUIManager.Instance?.HideGauge();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player _player))
        {
            player = _player;
            isReady = true;
            nowDuration = 0.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player _player))
        {
            isReady = false;
            nowDuration = 0.0f;
            InGameUIManager.Instance?.HideGauge();
        }
    }
}
