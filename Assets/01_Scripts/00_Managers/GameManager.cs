using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get => instance;
        private set => instance = value;
    }
    
    [SerializeField] Player player;
    [SerializeField] PlayerController playerController;
    [SerializeField] CameraController cameraController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        
    }

    private void Init()
    {
        //InputManager 세팅
        InputManager.Instance.SetPlayerController(playerController);
        InputManager.Instance.SetCameraController(cameraController);
        
        // 카메라 타겟 세팅
        cameraController.SetTarget(player.transform);
        
    }
    
    
    private void Start()
    {
        Init();

    }
}
