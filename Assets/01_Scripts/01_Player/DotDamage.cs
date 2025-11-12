using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDamage
{
    public Coroutine DebuffEffectRoutine => debuffEffectRoutine;
    private Coroutine debuffEffectRoutine;

    public void SetDotDamageRoutine(Coroutine debuffCoroutine)
    {
        debuffEffectRoutine = debuffCoroutine;
    }

    public DotDamageType Type => type;
    public Condition Target => target;
    public int Amount => amount;
    public float Duration => duration;
    public float DamageInterval => damageInterval;

    DotDamageType type;
    private Condition target;
    private int amount;
    private float duration;
    private float damageInterval;

    private Action dotDamageAction;

    public DotDamage(DotDamageType _type, Condition _targetCondition, int _amount, float _duration, float _damageInterval,
        Action _dotDamageAction = null)
    {
        type = _type;
        target = _targetCondition;
        amount = _amount;
        duration = _duration;
        damageInterval = _damageInterval;

        dotDamageAction = _dotDamageAction;
    }

    public void ApplyDebuff()
    {
        target?.Add(-amount);
        dotDamageAction?.Invoke();
    }

}
