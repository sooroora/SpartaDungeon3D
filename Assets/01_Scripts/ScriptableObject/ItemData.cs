using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class ItemData : BaseObjectData
{
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    [FormerlySerializedAs("maxStackAmount")] public int maxCountAmount = 99;

    public virtual Item NewItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
    
    
}
