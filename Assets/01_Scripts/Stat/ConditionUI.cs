using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : MonoBehaviour
{
    private Condition target;

    public Image uiBar;

    public void Init(Condition _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null) return;
        
        uiBar.fillAmount = target.GetPercentage();
    }

}
