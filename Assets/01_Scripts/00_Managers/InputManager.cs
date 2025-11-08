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

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void RegisterActions()
    {
        playerInput = GetComponent<PlayerInput>();

        playerInput.actions["Move"].started += OnMove;
        playerInput.actions["Move"].performed += OnMove;
        playerInput.actions["Move"].canceled += OnMove;

        playerInput.actions["Look"].started += OnLook;
        playerInput.actions["Look"].performed += OnLook;
        playerInput.actions["Look"].canceled += OnLook;
        
        playerInput.actions["Jump"].started += OnJump;
        playerInput.actions["Jump"].performed += OnJump;
        playerInput.actions["Jump"].canceled += OnJump;
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
        playerController?.UpdateMoveInput(context.ReadValue<Vector2>());
        
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("ASDASD");
        if (context.phase == InputActionPhase.Started)
        {
            playerController?.OnJump();
        }
        
    }

    ////// Camera
    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 mouseDelta = context.ReadValue<Vector2>();
        cameraController?.UpdateLookInput(mouseDelta);
        
        
        // lateUpdate 때문에 forward가 늦게 구해질 수 있어서
        // UpdateLookInput에서 반환하는 방식으로 바꿔바야할 것 같음
        Vector3 forward = Quaternion.Euler(0, cameraController.ContainerY.localEulerAngles.y, 0)
                          * Vector3.forward;
        playerController?.UpdateForward(forward);
    }


}
