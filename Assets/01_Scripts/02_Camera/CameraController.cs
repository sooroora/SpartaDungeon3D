using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
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
    [SerializeField] private Transform containerX;
    [SerializeField] private Transform containerY;

    [Header("Camera Option")]
    [SerializeField] private float lookSensitivity = 0.2f;

    private float camCurXRot;
    [SerializeField] private float minXLook;
    [SerializeField] private float maxXLook;

    [SerializeField] private float defaultCameraDistance = 5.0f;
    [SerializeField] private float cameraCollisionOffset = 2.0f;

    private Transform target;
    bool isThirdPerson = false;
    
    
    private void LateUpdate()
    {
        MoveCamera();
        RotateCamera();
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public void ToggleThirdPerson(bool _isThirdPerson)
    {
        isThirdPerson = _isThirdPerson;

        if (isThirdPerson)
        {
        }
        else
        {
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

    public void UpdateLookInput(Vector2 delta)
    {
        mouseDelta = delta;
    }

    public void RotateCamera()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        containerX.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        containerY.transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void MoveCamera()
    {
        this.transform.position = target.transform.position;
    }

}
