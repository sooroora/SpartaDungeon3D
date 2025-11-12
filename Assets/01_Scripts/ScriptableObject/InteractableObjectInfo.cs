using UnityEngine;

[CreateAssetMenu(fileName ="BaseObjectData", menuName = "Game/BaseObjectData")]
public class BaseObjectData : ScriptableObject
{
    [Header("Info")]
    [SerializeField] private string displayName;
    [SerializeField] private string description;
    
    public string DisplayName => displayName;
    public string Description => description;
    
}
