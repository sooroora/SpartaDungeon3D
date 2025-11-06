using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get => instance;
        private set => instance = value;
    }
    
    [SerializeField] PlayerController playerController;
    [SerializeField] CameraController cameraController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        InputManager.Instance.SetPlayerController(playerController);
        InputManager.Instance.SetCameraController(cameraController);
    }
}
