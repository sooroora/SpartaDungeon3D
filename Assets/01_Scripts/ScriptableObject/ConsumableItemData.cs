using UnityEngine;

[CreateAssetMenu(fileName = "Comsumable", menuName = "ItemData/Comsumable")]
public class ConsumableItemData : ItemData
{

    [Header("Consumerble")]
    public ConsumableEffect[] consumables;
}
