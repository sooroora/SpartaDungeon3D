using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform container;
    [SerializeField] private Transform containerFirstPersonX;

    private Vector3 thirdPersonPosition = new Vector3(0, 3.0f, -6.5f);
    private Vector3 firstPersonPosition = new Vector3(-0.25f, 1.6f, 0.65f);

    public Transform ContainerX
    {
        get => containerFirstPersonX;
        private set => containerFirstPersonX = value;
    }
    public Transform ContainerY
    {
        get => container;
        private set => container = value;
    }


    [Header("Camera Option")]
    [SerializeField] private float lookSensitivity = 0.2f;

    [SerializeField] private float defaultCameraDistance = 5.0f;
    [SerializeField] private float cameraCollisionOffset = 2.0f;

    private float camCurRotX;
    private float camCurRotY;

    [Header("Camera Rotation X Limit")]
    [SerializeField] private float minXLookFirstPerson;

    [SerializeField] private float maxXLookFirstPerson;
    [SerializeField] private float minXLookThirdPerson;
    [SerializeField] private float maxXLookThirdPerson;


    private Transform target;

    public bool IsThirdPerson => isThirdPerson;
    bool isThirdPerson = false;


    public void Awake()
    {
        
    }

    public void Start()
    {
        SetCameraPerspective(true);
    }

    private void LateUpdate()
    {
        MoveCamera();
        RotateCamera();
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    /// <summary>
    /// 3인칭이면 true return
    /// </summary>
    /// <returns></returns>
    public bool ToggleCameraPerspective()
    {
        container.transform.localRotation = Quaternion.identity;
        containerFirstPersonX.transform.localRotation = Quaternion.identity;
        if (isThirdPerson)
        {
            isThirdPerson = false;
            containerFirstPersonX.transform.localPosition = firstPersonPosition;
            InGameUIManager.Instance.ToggleCrosshair( true );
            return true;
        }
        else
        {
            isThirdPerson = true;
            containerFirstPersonX.transform.localPosition = thirdPersonPosition;
            InGameUIManager.Instance.ToggleCrosshair( false );
            return false;
        }
    }

    public void SetCameraPerspective(bool _isThirdPerson)
    {
        isThirdPerson = _isThirdPerson;
        if (isThirdPerson)
        {
            containerFirstPersonX.transform.localPosition = thirdPersonPosition;
            InGameUIManager.Instance.ToggleCrosshair( false );
        }
        else
        {
            containerFirstPersonX.transform.localPosition = firstPersonPosition;
            InGameUIManager.Instance.ToggleCrosshair( true );
        }
    }


    private RaycastHit hit;

    void CheckObstacle()
    {
        if (target == null)
            return;


        Ray ray = new Ray(this.transform.position,
            Vector3.Normalize(target.transform.position - this.transform.forward));

        if (Physics.Raycast(ray, out hit))
        {

        }
    }


    private Vector2 mouseDelta;

    public Vector3 UpdateLookInput(Vector2 delta)
    {
        mouseDelta = delta;

        CalcCameraRotation();
        Vector3 forward = Quaternion.Euler(0, camCurRotY, 0) * Vector3.forward;
        return forward;
    }

    public void CalcCameraRotation()
    {
        camCurRotX += mouseDelta.y * lookSensitivity;

        if (isThirdPerson)
            camCurRotX = Mathf.Clamp(camCurRotX, minXLookThirdPerson, maxXLookThirdPerson);
        else
            camCurRotX = Mathf.Clamp(camCurRotX, minXLookFirstPerson, maxXLookFirstPerson);

        camCurRotY += mouseDelta.x * lookSensitivity;

    }

    public void RotateCamera()
    {
        container.transform.localEulerAngles = new Vector3(0, camCurRotY, 0);

        if (isThirdPerson)
            container.localEulerAngles = new Vector3(-camCurRotX, container.localEulerAngles.y, 0);
        else
            containerFirstPersonX.transform.eulerAngles = new Vector3(-camCurRotX, containerFirstPersonX.transform.eulerAngles.y, containerFirstPersonX.transform.eulerAngles.z);
        // containerY.localEulerAngles = new Vector3(-camCurRotX, containerY.localEulerAngles.y, 0);
        // 
    }

    public void MoveCamera()
    {
        this.transform.position = target.transform.position;
    }

}
