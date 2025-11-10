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

        playerInput.actions["Dash"].started += OnDash;
        playerInput.actions["Dash"].canceled += OnDash;

        playerInput.actions["ToggleCameraPerspective"].started += OnToggleCameraPerspective;
        
        playerInput.actions["Interaction"].started += OnInteraction;
    
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
            playerController.OnDash(true);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            playerController.OnDash(false);
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
        cameraController.ToggleCameraPerspective();
    }


}
