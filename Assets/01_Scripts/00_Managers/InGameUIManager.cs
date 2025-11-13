using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    public static  InGameUIManager Instance => instance;
    private static InGameUIManager instance;

    [ SerializeField ] PlayerConditionUI playerConditionUI;
    [ SerializeField ] InteractionInfoUI interactionInfoUI;
    [ SerializeField ] InventoryUI       inventoryUI;
    [ SerializeField ] GameObject        crosshair;
    [ SerializeField ] InteractionMark   interactionMark;
    [ SerializeField ] private GameObject controlManualUI;

    private void Awake()
    {
        if ( instance == null )
            instance = this;
    }

    public void ToggleCrosshair( bool state )
    {
        crosshair.SetActive( state );
    }

    public void ShowInteractionInfo( BaseObjectData data )
    {
        interactionInfoUI.SetInteractionInfo( data.DisplayName, data.Description );
        interactionInfoUI.gameObject.SetActive( true );
    }

    public void HideInteractionInfo()
    {
        interactionInfoUI.gameObject.SetActive( false );
    }

    public bool ToggleInventoryUI()
    {
        if ( inventoryUI.gameObject.activeInHierarchy )
        {
            inventoryUI.gameObject.SetActive( false );
            return false;
        }
        else
        {
            inventoryUI.gameObject.SetActive( true ); 
            return true;
        }
    }


    public void ShowInteractionMark( Transform target, InteractionMarkType markType )
    {
        interactionMark?.Show( target, markType );
    }

    public void HideInteractionMark()
    {
        interactionMark?.Hide();
    }

    public void ShowControlManualUI(bool _state)
    {
        controlManualUI.SetActive(_state);
    }
}