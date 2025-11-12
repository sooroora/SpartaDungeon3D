using UnityEngine;

[ CreateAssetMenu( fileName = "Comsumable", menuName = "ItemData/Comsumable" ) ]
public class ConsumableItemData : ItemData
{
    [ Header( "Consumerble" ) ]
    [ SerializeField ] private ConsumableEffect[] consumables;

    public ConsumableEffect[] Consumables => consumables;

    public override Item NewItem()
    {
        ConsumableItem newItem = new ConsumableItem( this );
        return newItem;
    }
}