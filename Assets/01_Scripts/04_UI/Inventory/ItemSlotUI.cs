using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [ SerializeField ] private Image           itemIcon;
    [ SerializeField ] private TextMeshProUGUI textItemCount;
    [ SerializeField ]         Color           defaultColor;
    [ SerializeField ]         Color           selectedColor;

    private Image slotImg;

    public Item Item => item;
    Item        item;

    private InventoryUI inventoryUI;
    private Button      itemSlotButton;

    private bool isSelected = false;

    private void Awake()
    {
        slotImg = GetComponent< Image >();
        itemSlotButton = GetComponent< Button >();
        inventoryUI = GetComponentInParent< InventoryUI >();

        itemSlotButton.onClick.AddListener( OnSelect );

        if ( item == null )
        {
            itemIcon.enabled = false;
            textItemCount.text = "";
        }

        OnDeselect();
    }

    public void SetItemData( Item _item )
    {
        if(_item == null) return;
        
        item = _item;
        ItemData data = ItemManager.Instance.GetItemData( item.Name );

        if ( data == null )
        {
            itemIcon.enabled = false;
            textItemCount.text = "";
            return;
        }
        
        itemIcon.enabled = true;
        itemIcon.sprite = data.Icon;

        if ( data.CanStack )
        {
            textItemCount.text = "";
        }
        else
        {
            textItemCount.text = item.Count.ToString();
        }
    }

    public void OnSelect()
    {
        isSelected = true;
        slotImg.color = selectedColor;

        inventoryUI.SelectItemSlot( this );
    }

    public void OnDeselect()
    {
        isSelected = false;
        slotImg.color = defaultColor;
    }
}