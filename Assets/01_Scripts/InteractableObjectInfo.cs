using UnityEngine;

[CreateAssetMenu(fileName ="BaseObject", menuName = "Game/BaseObjectInfo")]
public class BaseObjectInfo : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    
    // 아이템인지 주울 수 있는지
    // 타입같은거 설정 필요
}
