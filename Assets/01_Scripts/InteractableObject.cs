using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private BaseObjectInfo objectInfo;
    [SerializeField] private GameObject interactionMark;
 
    [Header("Interactable Event")]
    [SerializeField] private UnityEvent OnInteraction;
    [SerializeField] private UnityEvent OnInteractionRangeEnter;
    [SerializeField] private UnityEvent OnInteractionRangeExit;

    private void Awake()
    {
        if (interactionMark != null)
        {
            interactionMark.SetActive(false);
            AddOnInteractionRangeEnter(() =>
            {
                interactionMark.SetActive(true);
            });
            
            AddOnInteractionRangeExit(() =>
            {
                interactionMark.SetActive(false);
            });
        }
    }

    private void Start()
    {
        AddOnInteractionRangeEnter(() =>
        {
            InGameUIManager.Instance?.ShowInteractionInfo(objectInfo);
        });
        AddOnInteractionRangeExit(()=>
        {
            InGameUIManager.Instance?.HideInteractionInfo();
        });
    }

    public void AddOnInteraction(UnityAction action)
    {
        OnInteraction.AddListener(action);
    }

    public void AddOnInteractionRangeEnter(UnityAction action)
    {
        OnInteractionRangeEnter.AddListener(action);
    }

    public void AddOnInteractionRangeExit(UnityAction action)
    {
        OnInteractionRangeExit.AddListener(action);
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
