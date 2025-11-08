using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition
{
    public float CurrentValue
    {
        get=>curValue;
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

    public float curValue;
    public float maxValue;
    public float startValue;
    public float passiveValue;

    public void Init(float _max, float _start, float _passive)
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
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    public void Substract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }
    
    
    
}
