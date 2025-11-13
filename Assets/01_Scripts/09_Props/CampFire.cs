using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CampFire : InteractableObject
{
    [ Header( "CampFire" ) ]
    [ SerializeField ] DotDamageType dotDamageType;

    [ SerializeField ] private GameObject fire;

    [ SerializeField ] private int burnDamage;

    private void Awake()
    {
        interactionType = InteractionMarkType.Interaction;
        AddOnInteraction( ToggleFire );
    }

    void OnTriggerEnter( Collider other )
    {
        if ( fire.activeInHierarchy == false ) return;

        if ( other.TryGetComponent( out IDotDamage debuffable ) )
        {
            debuffable.ApplyDotDamage( dotDamageType, burnDamage, 5.0f, 1.0f );
        }
    }

    void ToggleFire()
    {
        fire.SetActive( !fire.activeInHierarchy );
    }
}