using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour, IDamagable, IDebuffable
{
    [SerializeField] private PlayerCommonStat commonStat;
    PlayerConditionUI playerConditionUI;

    public Condition Health => health;
    public Condition Hunger => hunger;
    public Condition Stamina => stamina;

    Condition health;
    Condition hunger;
    Condition stamina;

    // 뭐하는애지
    public float noHungerHealthDecay;
    public event Action onTakeDamage;

    Coroutine healthCoroutine;
    Coroutine hungerPassiveCoroutine;
    Coroutine staminaPassiveCoroutine;


    List<Debuff> debuffs = new List<Debuff>();

    private void Awake()
    {
        health = new Condition(commonStat.MaxHealth, commonStat.MaxHealth, 0);
        stamina = new Condition(commonStat.MaxStamina, commonStat.MaxStamina, 1.0f);
        hunger = new Condition(commonStat.MaxHunger, commonStat.MaxHunger, -0.2f);


    }

    public void Start()
    {
        staminaPassiveCoroutine = StartCoroutine(PassiveConditionRoutine(stamina, 1.0f));
    }

    private void Update()
    {
        // hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        // stamina.Add(stamina.passiveValue * Time.deltaTime);
        //
        // if(hunger.curValue < 0f)
        // {
        //     health.Subtract(noHungerHealthDecay * Time.deltaTime);
        // }
        //
        // if(health.curValue < 0f)
        // {
        //     Die();
        // }
    }

    public void Heal(float amount)
    {
        //health.Add(amount);
    }

    public void Eat(float amount)
    {
        //hunger.Add(amount);
    }

    public void Dash(float amount)
    {
        stamina.SetUsingCondition(true);
        stamina.Add(-amount);
    }

    public void Die()
    {

    }


    public void TakeDamage(int damage)
    {
        CameraManager.Instance.CameraEffectController.ShowHitIndicator();
        health.Add(-damage);
    }

    public void TakeDebuff(DebuffType debuffType, int amount, float debuffTime, float damageInterval = 1)
    {
        Condition targetCondition = null;
        Action debuffEffectAction = null;

        switch (debuffType)
        {
            case DebuffType.Burn:
                targetCondition = health;
                debuffEffectAction += () => { CameraManager.Instance.CameraEffectController.ShowHitIndicator(); };
                break;
            case DebuffType.Poison:
                targetCondition = health;
                break;
        }

        Debuff newDebuff = new Debuff(debuffType, targetCondition, amount, debuffTime, damageInterval, debuffEffectAction);
        debuffs.Add(newDebuff);
        newDebuff.SetDebuffRoutine(StartCoroutine(DebuffRoutine(newDebuff)));
    }

    public void RemoveDebuff(Debuff debuff)
    {
        if (debuff.DebuffEffectRoutine != null)
            StopCoroutine(debuff.DebuffEffectRoutine);
        debuffs.Remove(debuff);
    }

    IEnumerator PassiveConditionRoutine(Condition targetCondition, float _time)
    {
        while (true)
        {
            yield return new WaitForSeconds(_time);

            if (targetCondition.IsUsing == false)
            {
                targetCondition.Add(targetCondition.PassiveValue);
            }
            else
            {
                targetCondition.SetUsingCondition(false);
                // 회복 시작까지 걸리는 시간을 추가로 기다리게 하는 애도 만들면 좋을 듯
            }

        }
    }

    IEnumerator DebuffRoutine(Debuff debuff)
    {
        float duration = debuff.Duration;
        WaitForSeconds wait = new WaitForSeconds(debuff.DamageInterval);

        while (duration > 0)
        {
            duration -= debuff.DamageInterval;
            debuff.ApplyDebuff();
            yield return wait;
        }

        debuffs.Remove(debuff);
    }


}
