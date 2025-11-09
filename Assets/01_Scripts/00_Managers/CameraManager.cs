using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance;
    public static CameraManager Instance
    {
        get => instance;
        private set => instance = value;
    }

    public CameraController CameraController => cameraController;
    public CameraEffectController CameraEffectController => cameraEffectController;
    private CameraController cameraController;
    CameraEffectController cameraEffectController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            cameraController = GetComponent<CameraController>();
            cameraEffectController = GetComponent<CameraEffectController>();
        }
        else if (instance != this)
            Destroy(gameObject);

    }

}
