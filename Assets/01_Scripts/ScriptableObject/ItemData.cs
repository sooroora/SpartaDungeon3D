using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class ItemData : BaseObjectData
{
    [ SerializeField ] private Sprite     icon;
    [ SerializeField ] private GameObject dropPrefab;

    [ Header( "Stacking" ) ]
    [ SerializeField ] private bool canStack;

    [ SerializeField ] private int maxCountAmount = 99;

    public Sprite  Icon => icon;
    public GameObject DropPrefab => dropPrefab;
    public bool CanStack => canStack;
    public int MaxCountAmount => maxCountAmount;
    
    
    public virtual Item NewItem()
    {
        Item newItem = new Item( this );
        return newItem;
    }
}