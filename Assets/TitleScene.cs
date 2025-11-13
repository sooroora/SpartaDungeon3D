using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayBgm(EBgmName.Title);
    }

}
