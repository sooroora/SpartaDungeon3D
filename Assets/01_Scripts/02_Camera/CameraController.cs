using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{  
    
    [SerializeField] private Transform containerX;
    [SerializeField] private Transform containerY;

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

    private float camCurXRot;
    [Header("Camera Rotation X Limit")]
    [SerializeField] private float minXLookFirstPerson;
    [SerializeField] private float maxXLookFirstPerson;
    [SerializeField] private float minXLookThirdPerson;
    [SerializeField] private float maxXLookThirdPerson;

    
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
            containerX.transform.localPosition = new Vector3(0, 3.0f, -6.5f);
        }
        else
        {
            containerX.transform.localPosition = new Vector3(0, 0.0f, 0.0f);
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
        
        if(isThirdPerson)
            camCurXRot = Mathf.Clamp(camCurXRot, minXLookThirdPerson, maxXLookThirdPerson);
        else
            camCurXRot = Mathf.Clamp(camCurXRot, minXLookFirstPerson, maxXLookFirstPerson);
        
        containerY.localEulerAngles = new Vector3(-camCurXRot, containerY.localEulerAngles.y, 0);

        containerY.transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void MoveCamera()
    {
        this.transform.position = target.transform.position;
    }

}
