using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Equip", menuName = "ItemData/Equip")]
public class EquipItemData : ItemData
{
    [Header("Equip Info")]
    public GameObject equipPrefab;
    [FormerlySerializedAs("equipmentType")] public EquipType equipType;
    public int atk;
    public int def;
    public int attackspeed;
    public int speed;

    public override Item NewItem()
    {
        EquipItem newItem = new EquipItem(this);
        return newItem;
    }
}
