public class EquipItem : Item
{
    public bool IsEquip=>isEquip;
    public int Atk => atk;
    public int Def => def;
    public int AttackSpeed=>attackspeed;
    public int Speed => speed;
    public EquipType Type => type;
    
    private EquipType type;
    private int atk;
    private int def;
    private int attackspeed;
    private int speed;
    
    private bool isEquip;
    
    
    public EquipItem(ItemData itemData) : base(itemData)
    {
        EquipItemData equipItem = itemData as EquipItemData;
        
        if(equipItem == null) return;
        
        atk = equipItem.atk;
        def = equipItem.def;
        attackspeed = equipItem.attackspeed;
        speed = equipItem.speed;
        type = equipItem.equipType;
    }

    public void Equip(Player player)
    {
        isEquip = true;
        player.Visual.EquipItem( Name );
    }

    public void Unequip( Player player )
    {
        isEquip = false;
        player.Visual.UnEquipItem();
    }
}
