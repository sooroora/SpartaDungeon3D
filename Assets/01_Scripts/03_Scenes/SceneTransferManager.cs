using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneTransferManager
{
    /// <summary>
    /// Scene 로드해서 이동하기
    /// </summary>
    /// <param name="sceneName">Scene 이름 ESceneName에 넣어놨어요</param>
    public static void LoadScene(ESceneName sceneName)
    {
        PlayerPrefs.SetString("NextSceneName", sceneName.ToString());
        SceneManager.LoadScene( "LoadingScene" );
    }

    /// <summary>
    /// 로딩씬 없이 가기
    /// 타이틀 가는데 로딩까지는 필요 없으니까!
    /// </summary>
    /// <param name="sceneName">Scene 이름 ESceneName에 넣어놨어요</param>
    public static void ImmediagetLoadScene(ESceneName sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }
    
}
