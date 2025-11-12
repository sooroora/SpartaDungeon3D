using System.Collections.Generic;

public static class GameDefaultSettings
{
    public const int InventoryMaxSlot = 30;

    public static Dictionary< ConsumableType, string > ConsumableText = new()
    {
        { ConsumableType.Health, "체력" },
        { ConsumableType.Hunger, "허기" },
        { ConsumableType.Stamina, "스테미나" },
        { ConsumableType.SpeedUp, "스피드업"},
    };
}