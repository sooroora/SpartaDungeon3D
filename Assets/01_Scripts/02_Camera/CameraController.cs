using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform containerX;
    [SerializeField] private Transform containerY;

    private Vector3 thirdPersonPosition = new Vector3(0, 3.0f, -6.5f);
    private Vector3 firstPersonPosition = new Vector3(-0.25f, 1.6f, 0.65f);

    public Transform ContainerX
    {
        get => containerX;
        private set => containerX = value;
    }
    public Transform ContainerY
    {
        get => containerY;
        private set => containerY = value;
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
    bool isThirdPerson = false;


    public void Awake()
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

    public bool ToggleCameraPerspective()
    {
        if (isThirdPerson)
        {
            isThirdPerson = false;
            containerX.transform.localPosition = firstPersonPosition;
            return true;
        }
        else
        {
            isThirdPerson = true;
            containerX.transform.localPosition = thirdPersonPosition;
            return false;
        }
    }

    public void SetCameraPerspective(bool _isThirdPerson)
    {
        isThirdPerson = _isThirdPerson;
        if (isThirdPerson)
        {
            containerX.transform.localPosition = thirdPersonPosition;
        }

        else
        {
            containerX.transform.localPosition = firstPersonPosition;
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
        containerY.localEulerAngles = new Vector3(-camCurRotX, containerY.localEulerAngles.y, 0);
        containerY.transform.eulerAngles = new Vector3(containerY.transform.eulerAngles.x, camCurRotY, 0);
    }

    public void MoveCamera()
    {
        this.transform.position = target.transform.position;
    }

}
