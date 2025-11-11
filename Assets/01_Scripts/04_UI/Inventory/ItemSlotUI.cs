using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] Color defaultColor;
    [SerializeField] Color selectedColor;

    private Image slotImg;

    public ItemData ItemData => itemData;
    ItemData itemData;

    private InventoryUI inventoryUI;
    private Button itemSlotButton;

    private bool isSelected = false;

    private void Awake()
    {
        slotImg = GetComponent<Image>();
        itemSlotButton = GetComponent<Button>();
        inventoryUI = GetComponentInParent<InventoryUI>();

        itemSlotButton.onClick.AddListener(OnSelect);

        if (itemData == null)
        {
            itemIcon.enabled = false;
            itemName.text = "";
        }

        OnDeselect();
    }

    public void SetItemData(ItemData _itemData)
    {
        itemData = _itemData;
    }

    public void OnSelect()
    {
        isSelected = true;
        slotImg.color = selectedColor;

        inventoryUI.SelectItemSlot(this);
    }

    public void OnDeselect()
    {
        isSelected = false;
        slotImg.color = defaultColor;
    }

}
