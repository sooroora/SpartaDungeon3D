using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapScene : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void LoadBootStrapScene()
    {
        Scene scene = SceneManager.GetSceneByName(nameof(ESceneName.BootstrapScene));

        if (scene.isLoaded == false)
        {
            SceneManager.LoadScene(nameof(ESceneName.BootstrapScene),LoadSceneMode.Additive);
        }
    }
}
