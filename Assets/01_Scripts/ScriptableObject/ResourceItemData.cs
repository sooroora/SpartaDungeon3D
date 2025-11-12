using UnityEngine;

[ CreateAssetMenu( fileName = "Resource", menuName = "ItemData/Resource" ) ]
public class ResourceItemData : ItemData
{
    public override Item NewItem()
    {
        ResourceItem newItem = new ResourceItem( this );
        return newItem;
    }
}