using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List< Item > Items => items;
    private List< Item > items;


    public Inventory()
    {
        items = new List< Item >();
    }

    public bool AddItem( Item item, out int remainCount )
    {
        List< Item > findItems = items.FindAll( ( i ) =>
        {
            if ( item.Name == i.Name ) return true;
            return false;
        } );

        item.OnUse += OnItemUse;
        remainCount = item.Count;

        if ( findItems.Count > 0 )
        {
            if ( item.CanStack )
            {
                for ( int i = 0; i < findItems.Count; i++ )
                {
                    if ( findItems[ i ].Count < findItems[ i ].MaxCount )
                    {
                        int originCount = findItems[ i ].Count;
                        int nowStackCount = findItems[ i ].AddCount( remainCount );
                        remainCount = remainCount - ( nowStackCount - originCount );

                        if ( remainCount == 0 )
                        {
                            break;
                        }
                    }
                }

                if ( remainCount == 0 )
                    return true;
            }
        }

        if ( items.Count >= GameDefaultSettings.InventoryMaxSlot ) return false;

        items.Add( item );
        remainCount = 0;
        return true;
    }

    public void RemoveItem( Item item )
    {
        if ( items.Contains( item ) )
        {
            item.OnUse -= OnItemUse;
            items.Remove( item );
        }
    }

    void OnItemUse( Item item )
    {
        if ( item is ConsumableItem consumableItem )
        {
            if ( consumableItem.Count <= 0 )
                RemoveItem( consumableItem );
        }
    }
}