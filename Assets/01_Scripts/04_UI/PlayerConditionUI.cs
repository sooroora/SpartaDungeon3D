using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerConditionUI : MonoBehaviour
{
    public ConditionUI health;
    public ConditionUI hunger;
    public ConditionUI stamina;

    // public void Init(Condition _health, Condition _hunger, Condition _stamina)
    // {
    //     health.Init(_health);
    //     hunger.Init(_hunger);
    //     stamina.Init(_stamina);
    // }
    
    private void Start()
    {
        PlayerCondition playerCondition = GameManager.Instance.Player.Condition;
        
        health.Init(playerCondition.Health);
        hunger.Init(playerCondition.Hunger);
        stamina.Init(playerCondition.Stamina);
    }
}
