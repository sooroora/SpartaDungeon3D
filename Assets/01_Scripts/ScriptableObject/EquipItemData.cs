using UnityEngine;

[CreateAssetMenu(fileName = "Equip", menuName = "ItemData/Equip")]
public class EquipItemData : ItemData
{
    [Header("Equip Info")]
    public GameObject equipPrefab;
    public EquipmentType equipmentType;
    public int atk;
    public int def;
    public int Attackspeed;
    public int speed;
}
