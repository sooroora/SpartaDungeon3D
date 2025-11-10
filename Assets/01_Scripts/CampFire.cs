using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CampFire : InteractableObject
{
    [Header("CampFire")]
    [SerializeField] DebuffType debuffType;
    [SerializeField] private int burnDamage;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDebuffable debuffable))
        {
            debuffable.TakeDebuff(debuffType, burnDamage, 5.0f, 1.0f);
        }
    }

    public override void InteractionRangeEnter()
    {
        base.InteractionRangeEnter();
    }

    public override void InteractionRangeExit()
    {
        base.InteractionRangeExit();
    }


}
