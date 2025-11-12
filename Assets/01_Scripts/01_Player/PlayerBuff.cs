public class PlayerBuff
{
    public BuffType BuffType => buffType;
    private BuffType buffType;
    public float Multiplier=>multiplier;
    private float multiplier = 1;


    public PlayerBuff( BuffType _buffType, float _multiplier )
    {
        buffType = _buffType;
        multiplier = _multiplier;
    }
    
    public void SetMultiplier( float _multiplier )
    {
        multiplier = _multiplier;
    }
}