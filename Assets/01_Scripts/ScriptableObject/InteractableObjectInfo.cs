using UnityEngine;

[CreateAssetMenu(fileName ="BaseObjectData", menuName = "Game/BaseObjectData")]
public class BaseObjectData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    
    // 아이템인지 주울 수 있는지
    // 타입같은거 설정 필요
}
