using UnityEngine;

public class Item
{
    /// <summary>
    /// 실제 ItemData 이름 (Item_Carrot)
    /// </summary>
    public string Name => name;

    /// <summary>
    /// display 이름 (당근)
    /// </summary>
    public string DisplayName => displayName;

    public string Description => description;
    public int Count => count;
    public bool CanStack => canStack;
    public int MaxCount => maxCount;

    private string name;
    private string displayName;
    private string description;

    private int count;
    private bool canStack;
    private int maxCount;

    public Item(ItemData itemData)
    {
        name = itemData.name;
        displayName = itemData.DisplayName;
        description = itemData.Description;
        canStack = itemData.CanStack;
        maxCount = itemData.MaxCountAmount;
    }

    public int AddCount(int amount)
    {
        if (canStack == false)
        {
            return amount > 0 ? 0 : 1;
        }

        count += Mathf.Clamp(amount, 0, maxCount);
        return count;
    }
}
