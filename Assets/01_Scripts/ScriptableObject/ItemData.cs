using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemData : BaseObjectData
{
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount = 99;
    
}
