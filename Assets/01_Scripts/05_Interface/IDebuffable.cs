using UnityEngine;

// 디버프보다는 도트데미지의 하나 같음...
// 나중에 실제 디버프 효과와 분리 필요해 보임.
public interface IDebuffable
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="debuffType"> 디버프 타입</param>
    /// <param name="amount"> 디버프 효과 양 </param>
    /// <param name="debuffTime"> 디버프 지속시간 </param>
    /// <param name="damageInterval"> 디버프 효과 받는 간격 </param>
    void TakeDebuff(DebuffType debuffType, int amount, float debuffTime, float damageInterval = 1.0f);

    void RemoveDebuff(Debuff debuff);
}
