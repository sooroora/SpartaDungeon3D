using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CampFire : InteractableObject
{
    [Header("CampFire")]
    [SerializeField] DotDamageType dotDamageType;
    [SerializeField] private int burnDamage;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDotDamage debuffable))
        {
            debuffable.ApplyDotDamage(dotDamageType, burnDamage, 5.0f, 1.0f);
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
