using System;

public class ConsumableItem : Item
{
    public ConsumableEffect[] Consumable => consumable;
    private ConsumableEffect[] consumable;

    public ConsumableItem( ItemData itemData ) : base( itemData )
    {
        ConsumableItemData consumableItemData = ( ConsumableItemData )itemData;
        if ( consumableItemData == null ) return;

        consumable = consumableItemData.Consumables;
    }

    protected override void OnUseInternal( Player player )
    {
        for ( int i = 0; i < consumable.Length; i++ )
        {
            switch ( consumable[ i ].consumableType )
            {
                case ConsumableType.Hunger :
                    player.Condition.AddHunger( consumable[ i ].amount );
                    break;
                case ConsumableType.Health :
                    player.Condition.AddHealth( consumable[ i ].amount );
                    break;
                case ConsumableType.Stamina :
                    player.Condition.AddStamina( consumable[ i ].amount );
                    break;
                case ConsumableType.Poison :
                    player.Condition.TakeDebuff( DebuffType.Poison, consumable[ i ].amount, 3.0f, 0.5f );
                    break;
            }
        }
    }
}