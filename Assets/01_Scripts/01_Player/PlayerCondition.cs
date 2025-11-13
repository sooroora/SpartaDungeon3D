using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour, IDamagable, IDotDamage
{
    [SerializeField] private PlayerCommonStat commonStat;
    PlayerConditionUI playerConditionUI;

    public Condition Health => health;
    public Condition Hunger => hunger;
    public Condition Stamina => stamina;

    public bool IsInvincible => isInvincible;
    bool isInvincible = false;
    Condition health;
    Condition hunger;
    Condition stamina;


    private WaitForSeconds invincibleWait;
    Coroutine invincibleRoutine;
    Coroutine healthCoroutine;
    Coroutine hungerPassiveCoroutine;
    Coroutine staminaPassiveCoroutine;


    List<PlayerBuff> playerBuffs = new List<PlayerBuff>();
    List<DotDamage> dotDamages = new List<DotDamage>();

    private void Awake()
    {
        health = new Condition(commonStat.MaxHealth, commonStat.MaxHealth, 0);
        stamina = new Condition(commonStat.MaxStamina, commonStat.MaxStamina, 1.0f);
        hunger = new Condition(commonStat.MaxHunger, commonStat.MaxHunger, -0.2f);

        invincibleWait = new WaitForSeconds(commonStat.InvincibleTime);
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

    public void AddHealth(float amount)
    {
        health.Add(amount);
    }

    public void AddHunger(float amount)
    {
        hunger.Add(amount);
    }

    public void AddStamina(float amount)
    {
        stamina.Add(amount);
    }

    public void Dash(float amount)
    {
        stamina.SetUsingCondition(true);
        stamina.Add(-amount);
    }

    public void Die()
    {
        // 죽으면 없당
    }


    public void TakeDamage(int damage)
    {
        if (isInvincible) return;
        CameraManager.Instance.CameraEffectController.ShowHitIndicator();
        SoundManager.Instance?.PlaySfxOnce(ESfxName.Damaged);
        health.Add(-damage);
        SetInvincible();
    }

    public void ApplyDotDamage(DotDamageType dotDamageType, int amount, float durataion, float damageInterval = 1)
    {
        // target 이 2개 이상일 때도 있으면 좋겠다
        Condition targetCondition = null;
        Action debuffEffectAction = null;

        switch (dotDamageType)
        {
            case DotDamageType.Burn:
                targetCondition = health;
                debuffEffectAction += () =>
                {
                    CameraManager.Instance.CameraEffectController.ShowHitIndicator();
                    SoundManager.Instance?.PlaySfxOnce(ESfxName.Damaged);
                };
                break;
            
            // 없지만 이런게 있을거다...
            case DotDamageType.Poison:
                targetCondition = health;
                debuffEffectAction += () =>
                {
                    // CameraManager.Instance.CameraEffectController.ShowHitIndicator();
                    // SoundManager.Instance?.PlaySfxOnce(ESfxName.Damaged);
                };

                break;
        }

        DotDamage newDotDamage = new DotDamage(dotDamageType, targetCondition, amount, durataion, damageInterval, debuffEffectAction);
        dotDamages.Add(newDotDamage);
        newDotDamage.SetDotDamageRoutine(StartCoroutine(DotDamageRoutine(newDotDamage)));
    }

    public void TakeBuff(BuffType buffType, int amount, float duration)
    {
        PlayerBuff newBuff = new PlayerBuff(buffType, amount);
        StartCoroutine(PlayerBuffRoutine(newBuff, duration));
    }

    public float GetSpeedBuffValue()
    {
        float totalMultiplier = 1f;

        for (int i = 0; i < playerBuffs.Count; i++)
        {
            if (playerBuffs[i].BuffType == BuffType.SpeedUp)
                totalMultiplier *= playerBuffs[i].Multiplier;
        }

        return totalMultiplier;
    }

    public void RemoveDotDamage(DotDamage dotDamage)
    {
        if (dotDamage.DebuffEffectRoutine != null)
            StopCoroutine(dotDamage.DebuffEffectRoutine);
        dotDamages.Remove(dotDamage);
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

    IEnumerator PlayerBuffRoutine(PlayerBuff buff, float duration)
    {
        playerBuffs.Add(buff);
        yield return new WaitForSeconds(duration);
        if (playerBuffs.Contains(buff))
        {
            playerBuffs.Remove(buff);
        }
    }

    IEnumerator DotDamageRoutine(DotDamage dotDamage)
    {
        float duration = dotDamage.Duration;
        WaitForSeconds wait = new WaitForSeconds(dotDamage.DamageInterval);

        while (duration > 0)
        {
            duration -= dotDamage.DamageInterval;
            dotDamage.ApplyDebuff();
            yield return wait;
        }

        dotDamages.Remove(dotDamage);
    }


    void SetInvincible()
    {
        if (invincibleRoutine != null)
            return;
        invincibleRoutine = StartCoroutine(InvincibleRoutine());
    }

    IEnumerator InvincibleRoutine()
    {
        isInvincible = true;
        yield return invincibleWait;
        isInvincible = false;
        invincibleRoutine = null;
    }
}
