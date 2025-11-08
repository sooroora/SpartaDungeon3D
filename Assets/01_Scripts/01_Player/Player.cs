using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Camera Pivot")]
    [SerializeField] private Transform cameraPivotFirstPerson;
    [SerializeField] private Transform cameraPivotThirdPerson;
    
    private PlayerController controller;
    private PlayerCondition condition;
    
    public PlayerCondition Condition
    {
        get => condition;
        set => condition = value;
    }


    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }

    private void Start()
    {
        CharacterManager.Instance.Player = this;
    }


}
