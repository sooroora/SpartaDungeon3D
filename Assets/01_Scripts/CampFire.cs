using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum DebuffType
{
    Burn,
    Poison,
}

public class CampFire : MonoBehaviour
{
    [SerializeField] DebuffType debuffType;
    [SerializeField] private int burnDamage;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDebuffable debuffable))
        {
            debuffable.TakeDebuff(debuffType, burnDamage, 5.0f, 1.0f);
            //damagable.TakeDamage(1);       
        }
    }
}
