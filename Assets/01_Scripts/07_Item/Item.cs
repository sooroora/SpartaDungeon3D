using System;
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

    public event Action< Item > OnUse;
    public event Action<Item> OnThrow;

    public Item( ItemData itemData, int _count = 1 )
    {
        name = itemData.name;
        displayName = itemData.DisplayName;
        description = itemData.Description;
        canStack = itemData.CanStack;
        maxCount = itemData.MaxCountAmount;
        count = _count;
    }

    public int AddCount( int amount )
    {
        if ( canStack == false )
        {
            return amount > 0 ? 0 : 1;
        }

        count += Mathf.Clamp( amount, 0, maxCount );
        return count;
    }

    public void Use( Player player )
    {
        if ( count > 0 )
        {
            count -= 1;
            UseInternal( player );
            OnUse?.Invoke( this );
        }
        
    }

    protected virtual void UseInternal( Player player )
    {
    }

    public void Throw(Player player)
    {
        if ( count > 0 )
        {
            count -= 1;
            OnThrow?.Invoke(this);
        }
    }
}