using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform container;

    [Header("Camera Option")] 
    [SerializeField] private float lookSensitivity = 1.0f;

    private float camCurXRot;
    [SerializeField] private float minXLook;
    [SerializeField] private float maxXLook;
    
    [SerializeField] private float defaultCameraDistance = 5.0f;
    [SerializeField] private float cameraCollisionOffset = 2.0f;


    private Transform target;
    bool isThirdPerson = false;


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

    private void Update()
    {

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

    public void RotateCamera(Vector2 delta)
    {
        camCurXRot += delta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        container.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, delta.x * lookSensitivity, 0);
    }
    
}
