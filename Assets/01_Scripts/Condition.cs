using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition
{
    public float CurrentValue
    {
        get => curValue;
        private set => curValue = value;
    }

    public float StartValue
    {
        get => startValue;
        private set => startValue = value;
    }

    public float MaxValue
    {
        get => maxValue;
        private set => maxValue = value;
    }

    public float PassiveValue
    {
        get => passiveValue;
        private set => passiveValue = value;
    }

    public bool IsUsing => isUsing;

    private float curValue;
    private float maxValue;
    private float startValue;
    private float passiveValue;

    private bool isUsing;

    public Condition(float _max, float _start, float _passive)
    {
        maxValue = _max;
        startValue = _start;
        passiveValue = _passive;
        curValue = startValue;

    }

    public void SetMaxValue(float _max)
    {
        maxValue = _max;
    }

    public void SetStartValue(float _start)
    {
        startValue = _start;
    }


    public void SetPassiveValue(float _passive)
    {
        passiveValue = _passive;
    }

    public void Add(float amount)
    {
        curValue = Mathf.Clamp(curValue + amount, 0f, maxValue);
    }


    public float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void SetUsingCondition(bool _isUsing)
    {
        isUsing = _isUsing;
    }


}
