using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private ItemData[] itemData;
    
    public ItemManager Instance
    {
        get => instance;
        set => instance = value;
    }
    private ItemManager instance;

    private Dictionary<string, ItemData> itemDic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Init();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Init()
    {
        itemDic = new Dictionary<string, ItemData>();

        foreach (ItemData itemData in itemData)
        {
            itemDic.Add(itemData.name, itemData);
        }
    }

    public ItemData GetItemData(string itemName)
    {
        if (itemDic.ContainsKey(itemName))
        {
            return itemDic[itemName];
        }
        return null;
    }

    public void SpawnDropItem(string itemName, Vector3 position)
    {
        ItemData itemData = GetItemData(itemName);
        if(itemData == null) return;
        if(itemData.dropPrefab == null) return;
        
        Instantiate(itemData.dropPrefab, position, Quaternion.identity);
    }
}
