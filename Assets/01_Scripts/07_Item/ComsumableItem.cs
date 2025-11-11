public class ComsumableItem:Item
{
    public ConsumableEffect[] ComsumableEffect => comsumableEffects;
    private ConsumableEffect[] comsumableEffects;
    
    public ComsumableItem(ItemData itemData) : base(itemData)
    {
        
    }
}
