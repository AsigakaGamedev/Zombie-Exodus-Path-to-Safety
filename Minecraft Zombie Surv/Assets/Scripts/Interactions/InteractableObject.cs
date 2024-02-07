using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(QuickOutline))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField] private bool deactivateOnInteract = true;
    [SerializeField] private UnityEvent interactEvent;

    [Space]
    [SerializeField] private QuickOutline outline;

    public Action<PlayerController> onInteract;

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

    public void Interact(PlayerController player)
    {
        interactEvent.Invoke();
        onInteract?.Invoke(player);

        if (deactivateOnInteract) gameObject.SetActive(false);
    }
}
