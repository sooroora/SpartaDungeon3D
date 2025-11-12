using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [ SerializeField ] protected BaseObjectData objectData;

    [ Header( "Interactable Event" ) ]
    [ SerializeField ] protected UnityEvent OnInteraction;
    [ SerializeField ] protected UnityEvent OnInteractionRangeEnter;
    [ SerializeField ] protected UnityEvent OnInteractionRangeExit;

    protected InteractionMarkType interactionType;


    private void Start()
    {
        AddOnInteractionRangeEnter( () =>
        {
            InGameUIManager.Instance?.ShowInteractionInfo( objectData );
            InGameUIManager.Instance?.ShowInteractionMark( this.transform, interactionType );
        } );
        AddOnInteractionRangeExit( () =>
        {
            InGameUIManager.Instance?.HideInteractionInfo();
            InGameUIManager.Instance?.HideInteractionMark();
        } );
    }

    public virtual void AddOnInteraction( UnityAction action )
    {
        OnInteraction.AddListener( action );
    }

    public virtual void AddOnInteractionRangeEnter( UnityAction action )
    {
        OnInteractionRangeEnter.AddListener( action );
    }

    public virtual void AddOnInteractionRangeExit( UnityAction action )
    {
        OnInteractionRangeExit.AddListener( action );
    }


    public virtual void Interaction()
    {
        OnInteraction?.Invoke();
    }

    public virtual void InteractionRangeEnter()
    {
        OnInteractionRangeEnter?.Invoke();
    }

    public virtual void InteractionRangeExit()
    {
        OnInteractionRangeExit?.Invoke();
    }
}