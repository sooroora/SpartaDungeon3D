using System;
using System.Collections;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    [SerializeField] private PlayerCommonStat commonStat;
    public PlayerConditionUI playerConditionUI;

    Condition health ;
    Condition hunger ;
    Condition stamina;

    public float noHungerHealthDecay;
    public event Action onTakeDamage;

    Coroutine healthCoroutine;
    Coroutine hungerCoroutine;
    Coroutine staminaCoroutine;

    public void Init()
    {
        //health.Init();
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

    public void Die()
    {
        
    }

    IEnumerator PassiveConditionRoutine(Condition targetCondition, float _time)
    {
        while (true)
        {
            yield return new WaitForSeconds(_time);
            
        }
    }
}
