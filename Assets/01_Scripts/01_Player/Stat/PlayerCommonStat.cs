using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCommonStat", menuName = "Player/CreatePlayerCommonStat")]
public class PlayerCommonStat : ScriptableObject
{
    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    public float MaxHunger
    {
        get =>
            maxHunger;
        set =>
            maxHunger = value;
    }

    public float MaxStamina
    {
        get =>
            maxStamina;
        set =>
            maxStamina = value;
    }
    
    public float InvincibleTime => invincibleTime;
    
    [SerializeField] private float maxHealth;
    [SerializeField] private float maxHunger;
    [SerializeField] private float maxStamina;
    
    [SerializeField] private float invincibleTime = 2.0f;
}
