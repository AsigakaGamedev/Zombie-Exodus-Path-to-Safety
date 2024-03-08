using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(QuickOutline))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField] private bool deactivateOnInteract = true;
    [SerializeField] private UnityEvent successInteractEvent;
    [SerializeField] private UnityEvent failedInteractEvent;

    [Space]
    [SerializeField] private QuickOutline outline;

    [Space]
    [SerializeField] private AInteractValidator[] validators; 

    public Action<PlayerController> onSuccessInteract;
    public Action<PlayerController> onFailedInteract;

    private void OnValidate()
    {
        if (!outline) outline = GetComponent<QuickOutline>();
    }

    private void Start()
    {
        HideOutline();
    }

    public void ShowOutline()
    {
        outline.enabled = true;
    }

    public void HideOutline()
    {
        outline.enabled = false;
    }

    public void TryInteract(PlayerController player)
    {
        if (CanInteract(player))
        {
            onSuccessInteract?.Invoke(player);
            successInteractEvent.Invoke();

            if (deactivateOnInteract) gameObject.SetActive(false);
        }
        else
        {
            onFailedInteract?.Invoke(player);
            failedInteractEvent?.Invoke();
        }
    }

    public bool CanInteract(PlayerController player)
    {
        foreach (AInteractValidator validator in validators)
        {
            if (!validator.OnValidateInteract(player)) return false;
        }

        return true;
    }
}
