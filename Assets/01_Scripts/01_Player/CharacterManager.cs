using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager instance;

    public static CharacterManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            }
            return instance;
        }
    }

    private Player player;
    public Player Player => player;

    private void Awake()
    {
        if (instance == null)
        {
            //SceneManager.sceneLoaded += SetPlayer;
            
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(gameObject);
        }
    }

    public void SetPlayer(Player _player)
    {
        player = _player;
    }
    // void SetPlayer(Scene scene, LoadSceneMode mode)
    // {
    //     
    // }


}
