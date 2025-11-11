using System;
using System.Collections.Generic;
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

    public bool IsUIOpen => isUIOpen;
    bool isUIOpen = false;
    List<InputAction> playerInputList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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

    private void Update()
    {
        //test

        if (Input.GetKeyDown(KeyCode.F1))
        {
            OpenUI();
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            CloseUI();
        }

    }

    void RegisterActions()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInputList = new List<InputAction>();

        playerInput.actions["Move"].started += OnMove;
        playerInput.actions["Move"].performed += OnMove;
        playerInput.actions["Move"].canceled += OnMove;

        playerInput.actions["Look"].started += OnLook;
        playerInput.actions["Look"].performed += OnLook;
        playerInput.actions["Look"].canceled += OnLook;

        playerInput.actions["Jump"].started += OnJump;

        playerInput.actions["Dash"].started += OnDash;
        playerInput.actions["Dash"].canceled += OnDash;

        playerInput.actions["ToggleCameraPerspective"].started += OnToggleCameraPerspective;

        playerInput.actions["Interaction"].started += OnInteraction;
        playerInput.actions["Inventory"].started += OnInventory;

        // playerInputList에 담아두기
        // 많아지면 enum에 넣고 for문으로 바꾸기
        playerInputList.Add(playerInput.actions["Move"]);
        playerInputList.Add(playerInput.actions["Look"]);
        playerInputList.Add(playerInput.actions["Jump"]);
        playerInputList.Add(playerInput.actions["Dash"]);
        playerInputList.Add(playerInput.actions["ToggleCameraPerspective"]);
        playerInputList.Add(playerInput.actions["Interaction"]);
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
        playerController?.OnJump();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            playerController?.OnDash(true);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            playerController?.OnDash(false);
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        //playerController.
    }

    ////// Camera
    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 mouseDelta = context.ReadValue<Vector2>();
        Vector3 forward = cameraController?.UpdateLookInput(mouseDelta) ?? Vector3.forward;

        // Vector3 forward = Quaternion.Euler(0, cameraController.ContainerY.localEulerAngles.y, 0)
        //                   * Vector3.forward;
        playerController?.UpdateForward(forward);
    }

    public void OnToggleCameraPerspective(InputAction.CallbackContext context)
    {
        cameraController?.ToggleCameraPerspective();
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        //if(InGameUIManager.Instance.)
    }
    
    
    
    
    
    public void OpenUI()
    {
        isUIOpen = true;
        Cursor.lockState = CursorLockMode.None;
        foreach (InputAction action in playerInputList)
        {
            action.Disable();
        }
    }

    public void CloseUI()
    {
        isUIOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        foreach (InputAction action in playerInputList)
        {
            action.Enable();
        }
    }


}
