public class Item
{
    public string ItemName => itemName;
    public string ItemDescription => itemDescription;
    
    private string itemName;
    private string itemDescription;
    
    private bool canStack;
    private int maxStack;

    public Item(string _itemName, string _itemDescription, bool _canStack, int _maxStack)
    {
        itemName = _itemName;
        itemDescription = _itemDescription;
        canStack = _canStack;
        maxStack = _maxStack;
    }
}
