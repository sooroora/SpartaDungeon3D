using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = " ItemData", menuName = "Game/ItemData")]
public class ItemData : BaseObjectData
{
    [Header("Item Info")]
    public ItemType itemType;

    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;

    public int maxStackAmount = 99;
    
    [Header("Consumerble")]
    public ItemDataConsumable[] consumables;
}
