using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Camera Pivot")]
    [SerializeField] private Transform cameraPivotFirstPerson;

    [SerializeField] private Transform cameraPivotThirdPerson;
    public Transform CameraPivotFirstPerson => cameraPivotFirstPerson;
    public Transform CameraPivotThirdPerson => cameraPivotThirdPerson;


    private PlayerController controller;
    private PlayerCondition condition;
    private PlayerMotionController motionController;


    public PlayerController Controller => controller;
    public PlayerCondition Condition => condition;
    public PlayerMotionController MotionController => motionController;

    public Rigidbody Rigidbody => controller.Rigidbody;


    private void Awake()
    {
        //CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        motionController = GetComponent<PlayerMotionController>();
    }

    private void Start()
    {

    }

    public void UpdateMovingForward(Vector3 forward)
    {
        motionController.Rotate(forward);
    }
}
