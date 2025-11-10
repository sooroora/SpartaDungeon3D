
using UnityEngine;
using UnityEngine.Events;

public interface IInteractable
{
    public void Interaction();
    public void InteractionRangeEnter();
    public void InteractionRangeExit();
}
