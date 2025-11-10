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

    public Player Player => player;
    [SerializeField] Player player;
    [SerializeField] PlayerController playerController;
    //[SerializeField] CameraController cameraController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }


    private void Start()
    {
        Init();

    }

    private void Init()
    {
        //InputManager 세팅
        InputManager.Instance.SetPlayerController(playerController);
        InputManager.Instance.SetCameraController(CameraManager.Instance.CameraController);

        // 카메라 타겟 세팅
        CameraManager.Instance.CameraController.SetTarget(player.transform);
        CameraManager.Instance.CameraController.SetCameraPerspective(true);

    }

}
