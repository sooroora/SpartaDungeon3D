using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerCommonStat", menuName = "Player/CreatePlayerMovingStat")]
public class PlayerMovingStat :ScriptableObject
{
    public float Speed
    {
        get => speed;
        private set => speed = value;
    }
    public float JumpForce
    {
        get => jumpForce;
        private set => jumpForce = value;
    }
    
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 10;
   
}
