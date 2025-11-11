using System.Collections.Generic;

public class Inventory
{
    public List<Item> Items => items;
    private List<Item> items;

    public Inventory()
    {
        items = new List<Item>();
    }

    public bool AddItem(Item item)
    {
        List<Item> findItems = items.FindAll((i) =>
        {
            if (item.Name == i.Name) return true;
            return false;
        });

        if (findItems.Count == 0)
        {
            items.Add(item);
            return true;
        }
        else
        {
            if (item.CanStack)
            {
                for (int i = 0; i < findItems.Count; i++)
                {
                    if (findItems[i].Count < findItems[i].MaxCount)
                    {
                        findItems[i].AddCount(item.Count);
                        return true;
                    }
                }
            }
            else
            {

            }
        }
        return false;
    }

    public void RemoveItem(Item item)
    {

    }

}
