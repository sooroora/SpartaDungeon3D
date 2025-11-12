using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TitleMenu : MonoBehaviour
{
    [ SerializeField ] private Button btnStart;
    [ SerializeField ] private Button btnExit;

    private void Awake()
    {
        btnStart.onClick.AddListener(LoadMainScene);
        btnExit.onClick.AddListener( () =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        });
    }

    void LoadMainScene()
    {
        SceneTransferManager.LoadScene(ESceneName.MainScene);
    }
    
}
