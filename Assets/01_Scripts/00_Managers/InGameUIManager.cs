using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager Instance => instance;
    private static InGameUIManager instance;
    
    [SerializeField] PlayerConditionUI playerConditionUI;
    [SerializeField] InteractionInfoUI interactionInfoUI;
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] GameObject crosshair;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void ToggleCrosshair(bool state)
    {
        crosshair.SetActive(state);
    }

    public void ShowInteractionInfo(BaseObjectData data)
    {
        interactionInfoUI.SetInteractionInfo(data.displayName, data.description);
        interactionInfoUI.gameObject.SetActive(true);
    }

    public void HideInteractionInfo()
    {
        interactionInfoUI.gameObject.SetActive(false);
    }

}
