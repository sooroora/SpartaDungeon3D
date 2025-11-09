using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff
{
    public Coroutine DebuffEffectRoutine => debuffEffectRoutine;
    private Coroutine debuffEffectRoutine;

    public void SetDebuffRoutine(Coroutine debuffCoroutine)
    {
        debuffEffectRoutine = debuffCoroutine;
    }

    public DebuffType Type => type;
    public Condition Target => target;
    public int Amount => amount;
    public float Duration => debuffTime;
    public float DamageInterval => damageInterval;

    DebuffType type;
    private Condition target;
    private int amount;
    private float debuffTime;
    private float damageInterval;

    private Action debuffAction;

    public Debuff(DebuffType _type, Condition _targetCondition, int _amount, float _debuffTime, float _damageInterval,
        Action _debuffAction = null)
    {
        type = _type;
        target = _targetCondition;
        amount = _amount;
        debuffTime = _debuffTime;
        damageInterval = _damageInterval;

        debuffAction = _debuffAction;
    }

    public void ApplyDebuff()
    {
        target?.Add(-amount);
        debuffAction?.Invoke();
    }

}
