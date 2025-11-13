using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisual : MonoBehaviour
{
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform head;


    private GameObject nowEquipItem;

    public void EquipItem(string itemName, bool isLeft = false)
    {
        ItemData itemData = ItemManager.Instance.GetItemData(itemName);
        if (itemData is EquipItemData equipItemData)
        {
            if (equipItemData.equipPrefab == null) return;

            GameObject equipItem = Instantiate(equipItemData.equipPrefab);


            if (isLeft)
            {
                equipItem.transform.SetParent(leftHand);
            }
            else
            {
                equipItem.transform.SetParent(rightHand);
            }

            equipItem.transform.localPosition = Vector3.zero;
            equipItem.transform.localRotation = Quaternion.identity;
        }

    }

    public void UnEquipItem()
    {
        if (nowEquipItem == null) return;
        Destroy(nowEquipItem);
    }

    public void ShowHead(bool _state)
    {
        head.gameObject.SetActive(_state);
    }
}
