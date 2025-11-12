public class ConsumableItem:Item
{
    public ConsumableEffect[] Consumable => consumable;
    private ConsumableEffect[] consumable;
    
    public ConsumableItem(ItemData itemData) : base(itemData)
    {
        ConsumableItemData consumableItemData = (ConsumableItemData)itemData;
        if(consumableItemData == null) return;

        consumable = consumableItemData.Consumables;
    }
}
