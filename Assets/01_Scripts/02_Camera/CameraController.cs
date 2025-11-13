using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform container;
    [SerializeField] private Transform containerX;
    [SerializeField] private Transform wallCheckCotainer;
    [SerializeField] private Transform wallCheckContainerX;
    [SerializeField] private Transform wallCheckTransform;
    private Vector3 thirdPersonPosition = new Vector3(0, 3.0f, -6.5f);
    private Vector3 firstPersonPosition = new Vector3(-0.25f, 1.6f, 0.65f);

    public Transform ContainerX
    {
        get => containerX;
        private set => containerX = value;
    }
    public Transform Container
    {
        get => container;
        private set => container = value;
    }


    [Header("Camera Option")]
    [SerializeField] LayerMask obstacleLayer;

    [SerializeField] private float lookSensitivity = 0.2f;


    private float camCurRotX;
    private float camCurRotY;

    [Header("Camera Rotation X Limit")]
    [SerializeField] private float minXLookFirstPerson;

    [SerializeField] private float maxXLookFirstPerson;
    [SerializeField] private float minXLookThirdPerson;
    [SerializeField] private float maxXLookThirdPerson;


    private Transform target;
    private Transform camTransform;


    public bool IsThirdPerson => isThirdPerson;
    bool isThirdPerson = false;


    public void Start()
    {
        SetCameraPerspective(true);
        camTransform = CameraManager.Instance.Cam.gameObject.transform;


    }

    private void LateUpdate()
    {
        MoveCamera();
        RotateCamera();

        CheckObstacle();
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public void CheckObstacle()
    {
        if (target == null) return;
        float wallCheckDistance = isThirdPerson ? thirdPersonPosition.magnitude : (firstPersonPosition.magnitude * 1.2f);
        RaycastHit[] hits = Physics.RaycastAll(target.position, Vector3.Normalize(wallCheckTransform.position - target.position), wallCheckDistance, obstacleLayer);

        if (hits.Length > 0)
        {
            Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

            if (isThirdPerson)
            {
                camTransform.position = hits[0].point; 
            }
            else
            {
                container.localPosition = -wallCheckCotainer.forward * hits[0].distance;
                camTransform.localPosition = Vector3.zero;
            }
        }
        else
        {
            container.transform.localPosition = Vector3.zero;
            camTransform.localPosition = Vector3.zero;
        }


    }

    /// <summary>
    /// 3인칭이면 true return
    /// </summary>
    /// <returns></returns>
    public bool ToggleCameraPerspective()
    {
        container.transform.localRotation = Quaternion.identity;
        containerX.transform.localRotation = Quaternion.identity;

        wallCheckCotainer.transform.localRotation = Quaternion.identity;
        wallCheckContainerX.transform.localRotation = Quaternion.identity;
        if (isThirdPerson)
        {
            isThirdPerson = false;
            containerX.transform.localPosition = firstPersonPosition;
            wallCheckContainerX.transform.localPosition = firstPersonPosition;
            InGameUIManager.Instance.ToggleCrosshair(true);
            GameManager.Instance.Player.Visual.ShowHead(false);
            return true;
        }
        else
        {
            isThirdPerson = true;
            containerX.transform.localPosition = thirdPersonPosition;
            wallCheckContainerX.transform.localPosition = thirdPersonPosition;
            InGameUIManager.Instance.ToggleCrosshair(false);
            GameManager.Instance.Player.Visual.ShowHead(true);
            return false;
        }
    }

    public void SetCameraPerspective(bool _isThirdPerson)
    {
        isThirdPerson = _isThirdPerson;
        if (isThirdPerson)
        {
            containerX.transform.localPosition = thirdPersonPosition;
            wallCheckContainerX.transform.localPosition = thirdPersonPosition;
            InGameUIManager.Instance.ToggleCrosshair(false);
            GameManager.Instance.Player.Visual.ShowHead(true);
        }
        else
        {
            containerX.transform.localPosition = firstPersonPosition;
            wallCheckContainerX.transform.localPosition = firstPersonPosition;
            InGameUIManager.Instance.ToggleCrosshair(true);
            GameManager.Instance.Player.Visual.ShowHead(false);
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
        wallCheckCotainer.transform.localEulerAngles = new Vector3(0, camCurRotY, 0);

        if (isThirdPerson)
        {
            container.localEulerAngles = new Vector3(-camCurRotX, container.localEulerAngles.y, 0);
            wallCheckCotainer.localEulerAngles = new Vector3(container.localEulerAngles.x, container.localEulerAngles.y, 0);
        }
        else
        {
            containerX.transform.eulerAngles = new Vector3(-camCurRotX, containerX.transform.eulerAngles.y, containerX.transform.eulerAngles.z);
            wallCheckContainerX.transform.eulerAngles = new Vector3(-camCurRotX, containerX.transform.eulerAngles.y, containerX.transform.eulerAngles.z);
        }
        // containerY.localEulerAngles = new Vector3(-camCurRotX, containerY.localEulerAngles.y, 0);
        // 
    }

    public void MoveCamera()
    {
        this.transform.position = target.transform.position;
    }


}
