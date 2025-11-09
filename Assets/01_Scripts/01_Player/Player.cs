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

    public PlayerCondition Condition => condition;


    private void Awake()
    {
        //CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }

    private void Start()
    {

    }

    public void UpdateForward(Vector3 forward)
    {
        
    }
}
