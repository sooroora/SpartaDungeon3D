using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [ SerializeField ] ItemSlotUI itemSlotUIPrefab;
    [ SerializeField ] Transform itemSlotContainer;
    [ SerializeField ] private InventoryItemInfoUI itemInfoUI;


    List< ItemSlotUI > itemSlots;
    ItemSlotUI nowSelectedSlot;

    private void Awake()
    {
        itemSlots = new List< ItemSlotUI >();
        for ( int i = 0; i < GameDefaultSettings.InventoryMaxSlot; ++i )
        {
            ItemSlotUI spawnedSlot = Instantiate( itemSlotUIPrefab, itemSlotContainer );
            itemSlots.Add( spawnedSlot );
            spawnedSlot.transform.SetParent( itemSlotContainer );
        }
    }

    private void OnEnable()
    {
        OpenInventory();
        itemInfoUI.HideInfo();
    }

    public void OpenInventory()
    {
        Inventory inventory = GameManager.Instance.Player.Inventory;
        
        if ( inventory == null ) return;
        
        
        for ( int i = 0; i < itemSlots.Count; ++i ) // DefaultSettings 에 MaxSlot 
        {
            if ( inventory.Items.Count > i )
            {
                itemSlots[ i ].SetItemData( inventory.Items[ i ] );
            }
            else
            {
                itemSlots[i].SetItemData(null);
            }
            
            itemSlots[i].OnDeselect();
        }
        
        
    }

    public void SelectItemSlot( ItemSlotUI slot )
    {
        if ( nowSelectedSlot != null && nowSelectedSlot != slot )
        {
            nowSelectedSlot.OnDeselect();
        }

        nowSelectedSlot = slot;

        if ( slot.Item != null )
        {
            itemInfoUI.ShowInfo( slot.Item );
        }
    }
}