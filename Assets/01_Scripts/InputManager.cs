using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance
    {
        get => instance;
        set => instance = value;
    }

    PlayerInput playerInput;
    PlayerController playerController;
    CameraController cameraController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            RegisterActions();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void RegisterActions()
    {
        playerInput = GetComponent<PlayerInput>();
        
        playerInput.actions["Move"].performed += OnMove;
        playerInput.actions["Move"].started += OnMove;
        playerInput.actions["Move"].canceled += OnMove;
        
        playerInput.actions["Look"].performed += OnLook;
        playerInput.actions["Look"].started += OnLook;
        playerInput.actions["Look"].canceled += OnLook;
    }

    public void SetPlayerController(PlayerController _playerController)
    {
        playerController = _playerController;
        
    }

    public void SetCameraController(CameraController _cameraController)
    {
        cameraController = _cameraController;
    }


    ////// Player
    public void OnMove(InputAction.CallbackContext context)
    {


    }


    ////// Camera
    public void OnLook(InputAction.CallbackContext context)
    {
        if (cameraController == null) return;

        Vector2 mouseDelta = context.ReadValue<Vector2>();
        cameraController.RotateCamera(mouseDelta);

    }
}
