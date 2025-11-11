using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] ItemSlotUI itemSlotUIPrefab;
    [SerializeField] Transform itemSlotContainer;
    [SerializeField] private InventoryItemInfoUI itemInfoUI;
        
    
    List<ItemSlotUI> itemSlots;
    ItemSlotUI nowSelectedSlot;
    
    private void Awake()
    {
        itemSlots = new List<ItemSlotUI>();
        for (int i = 0; i < GameDefaultSettings.InventoryMaxSlot; ++i)
        {
            ItemSlotUI spawnedSlot = Instantiate(itemSlotUIPrefab, itemSlotContainer); 
            itemSlots.Add(spawnedSlot);
            spawnedSlot.transform.SetParent(itemSlotContainer);
            
        }
    }

    
    public void SelectItemSlot(ItemSlotUI slot)
    {
        if (nowSelectedSlot != null && nowSelectedSlot != slot)
        {
            nowSelectedSlot.OnDeselect();    
        }
        
        nowSelectedSlot = slot;

        if (slot.ItemData != null)
        {
            
        }
        
    }

}
