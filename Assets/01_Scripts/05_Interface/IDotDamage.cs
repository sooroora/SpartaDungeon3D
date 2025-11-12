using UnityEngine;

// 디버프보다는 도트데미지의 하나 같음...
// 나중에 실제 디버프 효과와 분리 필요해 보임.
public interface IDotDamage
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dotDamageType"> 디버프 타입</param>
    /// <param name="amount"> 디버프 효과 양 </param>
    /// <param name="durataion"> 디버프 지속시간 </param>
    /// <param name="damageInterval"> 디버프 효과 받는 간격 </param>
    void ApplyDotDamage(DotDamageType dotDamageType, int amount, float durataion, float damageInterval = 1.0f);

    void RemoveDotDamage(DotDamage dotDamage);
}
