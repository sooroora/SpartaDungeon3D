using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerController controller;
    private PlayerCondition condition;
    private PlayerMotionController motionController;


    public PlayerController Controller => controller;
    public PlayerCondition Condition => condition;
    public PlayerMotionController MotionController => motionController;

    public Rigidbody Rigidbody => controller.Rigidbody;

    public  Inventory Inventory => inventory;
    private Inventory inventory;

    private void Awake()
    {
        inventory = new Inventory();
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
